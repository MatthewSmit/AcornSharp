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
    internal static partial class Program
    {
        private static void TestsHarmony()
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

            Test("\"\\u{714E}\\u{8336}\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "煎茶",
                            raw = "\"\\u{714E}\\u{8336}\"",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("\"\\u{20BB7}\\u{91CE}\\u{5BB6}\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "𠮷野家",
                            raw = "\"\\u{20BB7}\\u{91CE}\\u{5BB6}\"",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Numeric Literal

            Test("00", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 0,
                            raw = "00",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 2, 2))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0o0", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 0,
                            raw = "0o0",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function test() {'use strict'; 0o0; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "test"),
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = "use strict",
                                        raw = "'use strict'",
                                        loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 29, 29))
                                    },
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 30, 30))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 0,
                                        raw = "0o0",
                                        loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 34, 34))
                                    },
                                    loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 35, 35))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 37, 37))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0o2", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 2,
                            raw = "0o2",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0o12", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 10,
                            raw = "0o12",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0O0", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 0,
                            raw = "0O0",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function test() {'use strict'; 0O0; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "test"),
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = "use strict",
                                        raw = "'use strict'",
                                        loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 29, 29))
                                    },
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 30, 30))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 0,
                                        raw = "0O0",
                                        loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 34, 34))
                                    },
                                    loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 35, 35))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 37, 37))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0O2", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 2,
                            raw = "0O2",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0O12", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 10,
                            raw = "0O12",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0b0", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 0,
                            raw = "0b0",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0b1", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 1,
                            raw = "0b1",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0b10", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 2,
                            raw = "0b10",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0B0", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 0,
                            raw = "0B0",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0B1", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 1,
                            raw = "0B1",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0B10", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 2,
                            raw = "0B10",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6 Template Strings

            Test("`42`", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression =  new BaseNode(default)
                        {
                            type = NodeType.TemplateLiteral,
                            quasis = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("42", "42"),
                                    tail = true,
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 3, 3))
                                }
                            },
                            expressions = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("raw`42`", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.TaggedTemplateExpression,
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "raw"),
                            quasi = new BaseNode(default)
                            {
                                type = NodeType.TemplateLiteral,
                                quasis = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.TemplateElement,
                                        value = new TemplateNode("42", "42"),
                                        tail = true,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6))
                                    }
                                },
                                expressions = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 7, 7))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("raw`hello ${name}`", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.TaggedTemplateExpression,
                            tag = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "raw"),
                            quasi = new BaseNode(default)
                            {
                                type = NodeType.TemplateLiteral,
                                quasis = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.TemplateElement,
                                        value = new TemplateNode("hello ", "hello "),
                                        tail = false,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.TemplateElement,
                                        value = new TemplateNode("", ""),
                                        tail = true,
                                        loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 17, 17))
                                    }
                                },
                                expressions = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 16, 16)), "name")
                                },
                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 18, 18))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("`$`", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.TemplateLiteral,
                            quasis = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("$", "$"),
                                    tail = true,
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2))
                                }
                            },
                            expressions = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("`\\n\\r\\b\\v\\t\\f\\\n\\\r\n`", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.TemplateLiteral,
                            quasis = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("\\n\\r\\b\\v\\t\\f\\\n\\\n", "\n\r\b\u000b\t\f"),
                                    tail = true,
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(3, 0, 18))
                                }
                            },
                            expressions = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 19))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 19))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("`\n\r\n\r`", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.TemplateLiteral,
                            quasis = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("\n\n\n", "\n\n\n"),
                                    tail = true,
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(4, 0, 5))
                                }
                            },
                            expressions = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(4, 1, 6))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(4, 1, 6))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(4, 1, 6))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("`\\u{000042}\\u0042\\x42u0\\A`", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.TemplateLiteral,
                            quasis = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("\\u{000042}\\u0042\\x42u0\\A", "BBBu0A"),
                                    tail = true,
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 25, 25))
                                }
                            },
                            expressions = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("new raw`42`", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.NewExpression,
                            callee = new BaseNode(default)
                            {
                                type = NodeType.TaggedTemplateExpression,
                                tag = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), "raw"),
                                quasi = new BaseNode(default)
                                {
                                    type = NodeType.TemplateLiteral,
                                    quasis = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode("42", "42"),
                                            tail = true,
                                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                        }
                                    },
                                    expressions = new List<BaseNode>(),
                                    loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 11, 11))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11))
                            },
                            arguments = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("`outer${{x: {y: 10}}}bar${`nested${function(){return 1;}}endnest`}end`", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.TemplateLiteral,
                            expressions = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new IdentifierNode(default, "x"),
                                            value = new BaseNode(default)
                                            {
                                                type = NodeType.ObjectExpression,
                                                properties = new List<BaseNode>
                                                {
                                                    new BaseNode(default)
                                                    {
                                                        type = NodeType.Property,
                                                        method = false,
                                                        shorthand = false,
                                                        computed = false,
                                                        key = new IdentifierNode(default, "y"),
                                                        value = new BaseNode(default)
                                                        {
                                                            type = NodeType.Literal,
                                                            value = 10,
                                                            raw = "10"
                                                        },
                                                        kind = "init"
                                                    }
                                                }
                                            },
                                            kind = "init"
                                        }
                                    }
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.TemplateLiteral,
                                    expressions = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<BaseNode>(),
                                            generator = false,
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>
                                                {
                                                    new BaseNode(default)
                                                    {
                                                        type = NodeType.ReturnStatement,
                                                        argument = new BaseNode(default)
                                                        {
                                                            type = NodeType.Literal,
                                                            value = 1,
                                                            raw = "1"
                                                        }
                                                    }
                                                }
                                            },
                                            bexpression = false
                                        }
                                    },
                                    quasis = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode("nested", "nested"),
                                            tail = false
                                        },
                                        new BaseNode(default)
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode("endnest", "endnest"),
                                            tail = true
                                        }
                                    }
                                }
                            },
                            quasis = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("outer", "outer"),
                                    tail = false
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("bar", "bar"),
                                    tail = false
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.TemplateElement,
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

            Test("switch (answer) { case 42: let t = 42; break; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.SwitchStatement,
                        discriminant = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 14, 14)), "answer"),
                        cases = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.SwitchCase,
                                test = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    raw = "42",
                                    loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                },
                                sconsequent = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.VariableDeclaration,
                                        declarations = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.VariableDeclarator,
                                                id = new IdentifierNode(new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)), "t"),
                                                init = new BaseNode(default)
                                                {
                                                    type = NodeType.Literal,
                                                    value = 42,
                                                    raw = "42",
                                                    loc = new SourceLocation(new Position(1, 35, 35), new Position(1, 37, 37))
                                                },
                                                loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 37, 37))
                                            }
                                        },
                                        kind = "let",
                                        loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 38, 38))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.BreakStatement,
                                        label = null,
                                        loc = new SourceLocation(new Position(1, 39, 39), new Position(1, 45, 45))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 45, 45))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 47, 47))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 47, 47))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Arrow Function

            Test("() => \"test\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = "test",
                                raw = "\"test\"",
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("e => \"test\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "e")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = "test",
                                raw = "\"test\"",
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 11, 11))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(e) => \"test\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "e")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = "test",
                                raw = "\"test\"",
                                loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(a, b) => \"test\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "a"),
                                new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "b")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = "test",
                                raw = "\"test\"",
                                loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 16, 16))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("e => { 42; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "e")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 42,
                                            raw = "42",
                                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                                        },
                                        loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 10, 10))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 12, 12))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("e => ({ property: 42 })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "e")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16)), "property"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 42,
                                            raw = "42",
                                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20))
                                        },
                                        kind = "init",
                                        method = false,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 20, 20))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 22, 22))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("e => { label: 42 }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "e")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.LabeledStatement,
                                        label = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 12, 12)), "label"),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.ExpressionStatement,
                                            expression = new BaseNode(default)
                                            {
                                                type = NodeType.Literal,
                                                value = 42,
                                                raw = "42",
                                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                            },
                                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                        },
                                        loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 16, 16))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 18, 18))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(a, b) => { 42; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "a"),
                                new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "b")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 42,
                                            raw = "42",
                                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                        },
                                        loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 15, 15))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 17, 17))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("([a, , b]) => 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<BaseNode>
                                    {
                                        new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), "a"),
                                        null,
                                        new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "b")
                                    },
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 9, 9))
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                raw = "42",
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            }, new Options
            {
                ecmaVersion = 6
            });

            testFail("([a.a]) => 42", "Assigning to rvalue (1:2)", new Options {ecmaVersion = 6});
            testFail("() => {}()", "Unexpected token (1:8)", new Options {ecmaVersion = 6});
            testFail("(a) => {}()", "Unexpected token (1:9)", new Options {ecmaVersion = 6});
            testFail("a => {}()", "Unexpected token (1:7)", new Options {ecmaVersion = 6});
            testFail("console.log(typeof () => {});", "Unexpected token (1:20)", new Options {ecmaVersion = 6});

            Test("(() => {})()", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12)),
                            callee = new BaseNode(default)
                            {
                                type = NodeType.ArrowFunctionExpression,
                                id = null,
                                @params = new List<BaseNode>(),
                                fbody = new BaseNode(default)
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<BaseNode>(),
                                    loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                                },
                                generator = false,
                                bexpression = false,
                                loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 9, 9))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("((() => {}))()", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                            callee = new BaseNode(default)
                            {
                                type = NodeType.ArrowFunctionExpression,
                                id = null,
                                @params = new List<BaseNode>(),
                                fbody = new BaseNode(default)
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<BaseNode>(),
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                },
                                generator = false,
                                bexpression = false,
                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 10, 10))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });


            Test("(x=1) => x * x", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.AssignmentPattern,
                                    left = new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "x"),
                                    right = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 1,
                                        raw = "1",
                                        loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4))
                                    },
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 4, 4))
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                @operator = "*",
                                left = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "x"),
                                right = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "x"),
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("eval => 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), "eval")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                raw = "42",
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("arguments => 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9)), "arguments")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                raw = "42",
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(a) => 00", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "a")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 0,
                                raw = "00",
                                loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(eval, a) => 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 5, 5)), "eval"),
                                new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                raw = "42",
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(eval = 10) => 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.AssignmentPattern,
                                    left = new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 5, 5)), "eval"),
                                    right = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 10,
                                        raw = "10",
                                        loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                    },
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 10, 10))
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                raw = "42",
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(eval, a = 10) => 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 5, 5)), "eval"),
                                new BaseNode(default)
                                {
                                    type = NodeType.AssignmentPattern,
                                    left = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a"),
                                    right = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 10,
                                        raw = "10",
                                        loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13))
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 42,
                                raw = "42",
                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(x => x)", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "x")
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "x"),
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 7, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x => y => 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.ArrowFunctionExpression,
                                id = null,
                                @params = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "y")
                                },
                                fbody = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    raw = "42",
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                generator = false,
                                bexpression = true,
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 12, 12))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(x) => ((y, z) => (x, y, z))", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "x")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.ArrowFunctionExpression,
                                id = null,
                                @params = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "y"),
                                    new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "z")
                                },
                                fbody = new BaseNode(default)
                                {
                                    type = NodeType.SequenceExpression,
                                    expressions = new List<BaseNode>
                                    {
                                        new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "x"),
                                        new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "y"),
                                        new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)), "z")
                                    },
                                    loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 26, 26))
                                },
                                generator = false,
                                bexpression = true,
                                loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 27, 27))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("foo(() => {})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            arguments = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ArrowFunctionExpression,
                                    id = null,
                                    @params = new List<BaseNode>(),
                                    fbody = new BaseNode(default)
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<BaseNode>(),
                                        loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                    },
                                    generator = false,
                                    bexpression = false,
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("foo((x, y) => {})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "foo"),
                            arguments = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ArrowFunctionExpression,
                                    id = null,
                                    @params = new List<BaseNode>
                                    {
                                        new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "x"),
                                        new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "y")
                                    },
                                    fbody = new BaseNode(default)
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<BaseNode>(),
                                        loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                    },
                                    generator = false,
                                    bexpression = false,
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Method Definition

            Test("x = { method() { } }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12)), "method"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<BaseNode>(),
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 18, 18))
                                        },
                                        kind = "init",
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 18, 18))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 20, 20))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x = { method(test) { } }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12)), "method"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<BaseNode>
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 17, 17)), "test")
                                            },
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 22, 22))
                                        },
                                        kind = "init",
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 22, 22))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 24, 24))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x = { 'method'() { } }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = "method",
                                            raw = "'method'",
                                            loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                                        },
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<BaseNode>(),
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 20, 20))
                                        },
                                        kind = "init",
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 20, 20))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 22, 22))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x = { get() { } }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9)), "get"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<BaseNode>(),
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 15, 15))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 15, 15))
                                        },
                                        kind = "init",
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x = { set() { } }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9)), "set"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<BaseNode>(),
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 15, 15))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 15, 15))
                                        },
                                        kind = "init",
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            // Harmony: Object Literal Property Value Shorthand

            Test("x = { y, z }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "y"),
                                        value = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "y"),
                                        kind = "init",
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "z"),
                                        value = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "z"),
                                        kind = "init",
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            // Harmony: Destructuring

            Test("[a, b] = [b, a]", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new BaseNode(default)
                            {
                                type = NodeType.ArrayPattern,
                                elements = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "a"),
                                    new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "b")
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            right = new BaseNode(default)
                            {
                                type = NodeType.ArrayExpression,
                                elements = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "b"),
                                    new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "a")
                                },
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 15, 15))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({ responseText: text } = res)", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new BaseNode(default)
                            {
                                type = NodeType.ObjectPattern,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 15, 15)), "responseText"),
                                        value = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 21, 21)), "text"),
                                        kind = "init",
                                        method = false,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 21, 21))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 23, 23))
                            },
                            right = new IdentifierNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 29, 29)), "res"),
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 29, 29))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("const {a} = {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a"),
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9))
                                },
                                init = new BaseNode(default)
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<BaseNode>(),
                                    loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                },
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                            }
                        },
                        kind = "const",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("const [a] = []", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<BaseNode>
                                    {
                                        new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a")
                                    },
                                    loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 9, 9))
                                },
                                init = new BaseNode(default)
                                {
                                    type = NodeType.ArrayExpression,
                                    elements = new List<BaseNode> { },
                                    loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                },
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 14, 14))
                            }
                        },
                        kind = "const",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("let {a} = {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "a"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "a"),
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                                },
                                init = new BaseNode(default)
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<BaseNode>(),
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("let [a] = []", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<BaseNode>
                                    {
                                        new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "a")
                                    },
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                                },
                                init = new BaseNode(default)
                                {
                                    type = NodeType.ArrayExpression,
                                    elements = new List<BaseNode> { },
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var {a} = {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "a"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "a"),
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                                },
                                init = new BaseNode(default)
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<BaseNode>(),
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var [a] = []", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<BaseNode>
                                    {
                                        new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "a")
                                    },
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                                },
                                init = new BaseNode(default)
                                {
                                    type = NodeType.ArrayExpression,
                                    elements = new List<BaseNode> { },
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("const {a:b} = {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "b"),
                                            kind = "init",
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 10, 10))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11))
                                },
                                init = new BaseNode(default)
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<BaseNode>(),
                                    loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                },
                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 16, 16))
                            }
                        },
                        kind = "const",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("let {a:b} = {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "a"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "b"),
                                            kind = "init",
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                                },
                                init = new BaseNode(default)
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<BaseNode>(),
                                    loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var {a:b} = {}", new BaseNode(default)
            {
                type = NodeType.Program,
                sourceType = "script",
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "a"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "b"),
                                            kind = "init",
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9))
                                },
                                init = new BaseNode(default)
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<BaseNode>(),
                                    loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            // Harmony: Modules

            Test("export var document", new BaseNode(default)
            {
                type = NodeType.Program,
                sourceType = "module",
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19)), "document"),
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 19, 19))
                        },
                        specifiers = new List<BaseNode>(),
                        source = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export var document = { }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19)), "document"),
                                    init = new BaseNode(default)
                                    {
                                        type = NodeType.ObjectExpression,
                                        properties = new List<BaseNode>(),
                                        loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25))
                                    },
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 25, 25))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 25, 25))
                        },
                        specifiers = new List<BaseNode>(),
                        source = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            testFail("export var await", "The keyword 'await' is reserved (1:11)", new Options {ecmaVersion = 6, sourceType = "module"});

            Test("export let document", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19)), "document"),
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19))
                                }
                            },
                            kind = "let",
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 19, 19))
                        },
                        specifiers = new List<BaseNode>(),
                        source = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export let document = { }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19)), "document"),
                                    init = new BaseNode(default)
                                    {
                                        type = NodeType.ObjectExpression,
                                        properties = new List<BaseNode>(),
                                        loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25))
                                    },
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 25, 25))
                                }
                            },
                            kind = "let",
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 25, 25))
                        },
                        specifiers = new List<BaseNode>(),
                        source = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export const document = { }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 21, 21)), "document"),
                                    init = new BaseNode(default)
                                    {
                                        type = NodeType.ObjectExpression,
                                        properties = new List<BaseNode>(),
                                        loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 27, 27))
                                    },
                                    loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 27, 27))
                                }
                            },
                            kind = "const",
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 27, 27))
                        },
                        specifiers = new List<BaseNode>(),
                        source = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export function parse() { }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new BaseNode(default)
                        {
                            type = NodeType.FunctionDeclaration,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21)), "parse"),
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 27, 27))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 27, 27))
                        },
                        specifiers = new List<BaseNode>(),
                        source = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export class Class {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new BaseNode(default)
                        {
                            type = NodeType.ClassDeclaration,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 18, 18)), "Class"),
                            superClass = null,
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.ClassBody,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 21, 21))
                            },
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 21, 21))
                        },
                        specifiers = new List<BaseNode>(),
                        source = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            testFail("export new Foo();", "Unexpected token (1:7)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("export typeof foo;", "Unexpected token (1:7)", new Options {ecmaVersion = 6, sourceType = "module"});

            Test("export default 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        declaration = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = 42,
                            raw = "42",
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export default function () {}", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                        declaration = new BaseNode(default)
                        {
                            type = NodeType.FunctionDeclaration,
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 29, 29)),
                            id = null,
                            generator = false,
                            bexpression = false,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29)),
                                body = new List<BaseNode>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            Test("export default function f() {}", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30)),
                        declaration = new BaseNode(default)
                        {
                            type = NodeType.FunctionDeclaration,
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 30, 30)),
                            id = new IdentifierNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25)), "f"),
                            generator = false,
                            bexpression = false,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 30, 30)),
                                body = new List<BaseNode>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            Test("export default class {}", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                        declaration = new BaseNode(default)
                        {
                            type = NodeType.ClassDeclaration,
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23)),
                            id = null,
                            superClass = null,
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.ClassBody,
                                loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23)),
                                body = new List<BaseNode>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            Test("export default class A {}", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25)),
                        declaration = new BaseNode(default)
                        {
                            type = NodeType.ClassDeclaration,
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 25, 25)),
                            id = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), "A"),
                            superClass = null,
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.ClassBody,
                                loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25)),
                                body = new List<BaseNode>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            Test("export default (class{});", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        declaration = new BaseNode(default)
                        {
                            type = NodeType.ClassExpression,
                            id = null,
                            superClass = null,
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.ClassBody,
                                body = new List<BaseNode>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            testFail("export *", "Unexpected token (1:8)", new Options {ecmaVersion = 6, sourceType = "module"});

            Test("export * from \"crypto\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportAllDeclaration,
                        source = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "crypto",
                            raw = "\"crypto\"",
                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export { encrypt }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = null,
                        specifiers = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ExportSpecifier,
                                exported = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), "encrypt"),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), "encrypt"),
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16))
                            }
                        },
                        source = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export { encrypt, decrypt }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = null,
                        specifiers = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ExportSpecifier,
                                exported = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), "encrypt"),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), "encrypt"),
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.ExportSpecifier,
                                exported = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25)), "decrypt"),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25)), "decrypt"),
                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25))
                            }
                        },
                        source = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export { encrypt as default }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = null,
                        specifiers = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ExportSpecifier,
                                exported = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27)), "default"),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), "encrypt"),
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 27, 27))
                            }
                        },
                        source = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export { encrypt, decrypt as dec }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = null,
                        specifiers = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ExportSpecifier,
                                exported = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), "encrypt"),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), "encrypt"),
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.ExportSpecifier,
                                exported = new IdentifierNode(new SourceLocation(new Position(1, 29, 29), new Position(1, 32, 32)), "dec"),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25)), "decrypt"),
                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 32, 32))
                            }
                        },
                        source = null,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export { default } from \"other\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = null,
                        specifiers = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ExportSpecifier,
                                exported = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), "default"),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), "default"),
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16))
                            }
                        },
                        source = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 31, 31)),
                            value = "other",
                            raw = "\"other\""
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            testFail("export { default }", "Unexpected keyword 'default' (1:9)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("export { if }", "Unexpected keyword 'if' (1:9)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("export { default as foo }", "Unexpected keyword 'default' (1:9)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("export { if as foo }", "Unexpected keyword 'if' (1:9)", new Options {ecmaVersion = 6, sourceType = "module"});

            Test("import \"jquery\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ImportDeclaration,
                        specifiers = new List<BaseNode>(),
                        source = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "jquery",
                            raw = "\"jquery\"",
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 15, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("import $ from \"jquery\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ImportDeclaration,
                        specifiers = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ImportDefaultSpecifier,
                                local = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "$"),
                                loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                            }
                        },
                        source = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "jquery",
                            raw = "\"jquery\"",
                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("import { encrypt, decrypt } from \"crypto\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ImportDeclaration,
                        specifiers = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ImportSpecifier,
                                imported = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), "encrypt"),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), "encrypt"),
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16))
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.ImportSpecifier,
                                imported = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25)), "decrypt"),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25)), "decrypt"),
                                loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 25, 25))
                            }
                        },
                        source = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "crypto",
                            raw = "\"crypto\"",
                            loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 41, 41))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 41, 41))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("import { encrypt as enc } from \"crypto\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ImportDeclaration,
                        specifiers = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ImportSpecifier,
                                imported = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 16, 16)), "encrypt"),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 23, 23)), "enc"),
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 23, 23))
                            }
                        },
                        source = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "crypto",
                            raw = "\"crypto\"",
                            loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 39, 39))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("import crypto, { decrypt, encrypt as enc } from \"crypto\"", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 56, 56)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ImportDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 56, 56)),
                        specifiers = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ImportDefaultSpecifier,
                                loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13)),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13)), "crypto")
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.ImportSpecifier,
                                loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)),
                                imported = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)), "decrypt"),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24)), "decrypt")
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.ImportSpecifier,
                                loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 40, 40)),
                                imported = new IdentifierNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 33, 33)), "encrypt"),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 37, 37), new Position(1, 40, 40)), "enc")
                            }
                        },
                        source = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            loc = new SourceLocation(new Position(1, 48, 48), new Position(1, 56, 56)),
                            value = "crypto",
                            raw = "\"crypto\""
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            testFail("import default from \"foo\"", "Unexpected token (1:7)", new Options {ecmaVersion = 6, sourceType = "module"});

            Test("import { null as nil } from \"bar\"", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ImportDeclaration,
                        specifiers = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ImportSpecifier,
                                imported = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "null"),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20)), "nil"),
                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 20, 20))
                            }
                        },
                        source = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "bar",
                            raw = "\"bar\"",
                            loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 33, 33))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("import * as crypto from \"crypto\"", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ImportDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32)),
                        specifiers = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ImportNamespaceSpecifier,
                                loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 18, 18)),
                                local = new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 18, 18)), "crypto")
                            }
                        },
                        source = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 32, 32)),
                            value = "crypto",
                            raw = "\"crypto\""
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            testFail("import { class } from 'foo'", "Unexpected keyword 'class' (1:9)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("import { class, var } from 'foo'", "Unexpected keyword 'class' (1:9)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("import { a as class } from 'foo'", "Unexpected keyword 'class' (1:14)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("import * as class from 'foo'", "Unexpected keyword 'class' (1:12)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("import { enum } from 'foo'", "The keyword 'enum' is reserved (1:9)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("import { a as enum } from 'foo'", "The keyword 'enum' is reserved (1:14)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("import * as enum from 'foo'", "The keyword 'enum' is reserved (1:12)", new Options {ecmaVersion = 6, sourceType = "module"});

            // Harmony: Yield Expression

            Test("(function* () { yield v })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(default)
                                        {
                                            type = NodeType.YieldExpression,
                                            argument = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "v"),
                                            @delegate = false,
                                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 23, 23))
                                        },
                                        loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 23, 23))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 25, 25))
                            },
                            generator = true,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 25, 25))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("(function* () { yield\nv })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(default)
                                        {
                                            type = NodeType.YieldExpression,
                                            argument = null,
                                            @delegate = false,
                                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21))
                                        },
                                        loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new IdentifierNode(new SourceLocation(new Position(2, 0, 22), new Position(2, 1, 23)), "v"),
                                        loc = new SourceLocation(new Position(2, 0, 22), new Position(2, 1, 23))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(2, 3, 25))
                            },
                            generator = true,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(2, 3, 25))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 4, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(2, 4, 26))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("(function* () { yield *v })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(default)
                                        {
                                            type = NodeType.YieldExpression,
                                            argument = new IdentifierNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24)), "v"),
                                            @delegate = true,
                                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 24, 24))
                                        },
                                        loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 24, 24))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 26, 26))
                            },
                            generator = true,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 26, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function* test () { yield *v }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "test"),
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.YieldExpression,
                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 28, 28)), "v"),
                                        @delegate = true,
                                        loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 28, 28))
                                    },
                                    loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 28, 28))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 30, 30))
                        },
                        generator = true,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var x = { *test () { yield *v } };", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 15, 15)), "test"),
                                            value = new BaseNode(default)
                                            {
                                                type = NodeType.FunctionExpression,
                                                id = null,
                                                @params = new List<BaseNode>(),
                                                fbody = new BaseNode(default)
                                                {
                                                    type = NodeType.BlockStatement,
                                                    body = new List<BaseNode>
                                                    {
                                                        new BaseNode(default)
                                                        {
                                                            type = NodeType.ExpressionStatement,
                                                            expression = new BaseNode(default)
                                                            {
                                                                type = NodeType.YieldExpression,
                                                                argument = new IdentifierNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), "v"),
                                                                @delegate = true,
                                                                loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 29, 29))
                                                            },
                                                            loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 29, 29))
                                                        }
                                                    },
                                                    loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31))
                                                },
                                                generator = true,
                                                bexpression = false,
                                                loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 31, 31))
                                            },
                                            kind = "init",
                                            method = true,
                                            shorthand = false,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 31, 31))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 33, 33))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 33, 33))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function* foo() { console.log(yield); }", new BaseNode(default)
            {
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        id = new IdentifierNode(default, "foo"),
                        generator = true,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(default)
                        {
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    expression = new BaseNode(default)
                                    {
                                        callee = new MemberExpressionNode(default,
                                            new IdentifierNode(default, "console"),
                                            new IdentifierNode(default, "log"),
                                            false),
                                        arguments = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                @delegate = false,
                                                argument = null,
                                                type = NodeType.YieldExpression,
                                            }
                                        },
                                        type = NodeType.CallExpression,
                                    },
                                    type = NodeType.ExpressionStatement,
                                }
                            },
                            type = NodeType.BlockStatement,
                        },
                        type = NodeType.FunctionDeclaration,
                    }
                },
                sourceType = "script",
                type = NodeType.Program
            }, new Options {ecmaVersion = 6});

            Test("function* t() {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "t"),
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                        },
                        generator = true,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(function* () { yield yield 10 })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(default)
                                        {
                                            type = NodeType.YieldExpression,
                                            argument = new BaseNode(default)
                                            {
                                                type = NodeType.YieldExpression,
                                                argument = new BaseNode(default)
                                                {
                                                    type = NodeType.Literal,
                                                    value = 10,
                                                    raw = "10",
                                                    loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 30, 30))
                                                },
                                                @delegate = false,
                                                loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 30, 30))
                                            },
                                            @delegate = false,
                                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 30, 30))
                                        },
                                        loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 30, 30))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 32, 32))
                            },
                            generator = true,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 32, 32))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            }, new Options
            {
                ecmaVersion = 6
            });

            testFail("function *g() { (x = yield) => {} }", "Yield expression cannot be a default value (1:21)", new Options {ecmaVersion = 6});
            testFail("function *g() { ({x = yield}) => {} }", "Yield expression cannot be a default value (1:22)", new Options {ecmaVersion = 6});

            // Harmony: Iterators

            Test("for(x of list) process(x);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForOfStatement,
                        left = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                        right = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 13, 13)), "list"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 22, 22)), "process"),
                                arguments = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24)), "x")
                                },
                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 25, 25))
                            },
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 26, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("for (var x of list) process(x);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForOfStatement,
                        left = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "x"),
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                        },
                        right = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18)), "list"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27)), "process"),
                                arguments = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), "x")
                                },
                                loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                            },
                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 31, 31))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("for (var x = 42 of list) process(x);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForOfStatement,
                        left = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "x"),
                                    init = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 42,
                                        raw = "42",
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                                    },
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 15, 15))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 15, 15))
                        },
                        right = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 23, 23)), "list"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 32, 32)), "process"),
                                arguments = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 33, 33), new Position(1, 34, 34)), "x")
                                },
                                loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 35, 35))
                            },
                            loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 36, 36))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 36, 36))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("for (let x of list) process(x);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForOfStatement,
                        left = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "x"),
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                }
                            },
                            kind = "let",
                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                        },
                        right = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 18, 18)), "list"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27)), "process"),
                                arguments = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), "x")
                                },
                                loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 30, 30))
                            },
                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 31, 31))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            }, new Options
            {
                ecmaVersion = 6
            });

            // Harmony: Class (strawman)

            Test("var A = class extends B {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "A"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.ClassExpression,
                                    superClass = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "B"),
                                    fbody = new BaseNode(default)
                                    {
                                        type = NodeType.ClassBody,
                                        body = new List<BaseNode>(),
                                        loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 26, 26))
                                    },
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 26, 26))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 26, 26))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A extends class B extends C {} {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = new BaseNode(default)
                        {
                            type = NodeType.ClassExpression,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "B"),
                            superClass = new IdentifierNode(new SourceLocation(new Position(1, 32, 32), new Position(1, 33, 33)), "C"),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.ClassBody,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 34, 34), new Position(1, 36, 36))
                            },
                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 36, 36))
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 37, 37), new Position(1, 39, 39))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 39, 39))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A {get() {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "get"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17))
                                    },
                                    kind = "method",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 18, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { static get() {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20)), "get"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 25, 25))
                                    },
                                    kind = "method",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 25, 25))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 26, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A extends B {get foo() {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), "B"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 26, 26)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 29, 29), new Position(1, 31, 31))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 31, 31))
                                    },
                                    kind = "get",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 32, 32))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A extends B { static get foo() {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), "B"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 31, 31), new Position(1, 34, 34)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 37, 37), new Position(1, 39, 39))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 34, 34), new Position(1, 39, 39))
                                    },
                                    kind = "get",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 39, 39))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 40, 40))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 40, 40))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A {set a(v) {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "a"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "v")
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 20, 20))
                                    },
                                    kind = "set",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 20, 20))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 21, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { static set a(v) {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), "a"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24)), "v")
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 28, 28))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 28, 28))
                                    },
                                    kind = "set",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 28, 28))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 29, 29))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A {set(v) {};}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "set"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "v")
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 18, 18))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 18, 18))
                                    },
                                    kind = "method",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 18, 18))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 20, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { static set(v) {};}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20)), "set"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), "v")
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 26, 26))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26))
                                    },
                                    kind = "method",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 26, 26))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 28, 28))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A {*gen(v) { yield v; }}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), "gen"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "v")
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>
                                            {
                                                new BaseNode(default)
                                                {
                                                    type = NodeType.ExpressionStatement,
                                                    expression = new BaseNode(default)
                                                    {
                                                        type = NodeType.YieldExpression,
                                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)), "v"),
                                                        @delegate = false,
                                                        loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 26, 26))
                                                    },
                                                    loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 27, 27))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 29, 29))
                                        },
                                        generator = true,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 29, 29))
                                    },
                                    kind = "method",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 29, 29))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 30, 30))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { static *gen(v) { yield v; }}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21)), "gen"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 23, 23)), "v")
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>
                                            {
                                                new BaseNode(default)
                                                {
                                                    type = NodeType.ExpressionStatement,
                                                    expression = new BaseNode(default)
                                                    {
                                                        type = NodeType.YieldExpression,
                                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 33, 33), new Position(1, 34, 34)), "v"),
                                                        @delegate = false,
                                                        loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 34, 34))
                                                    },
                                                    loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 35, 35))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 37, 37))
                                        },
                                        generator = true,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 37, 37))
                                    },
                                    kind = "method",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 37, 37))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 38, 38))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("\"use strict\"; (class A {constructor() { super() }})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "use strict",
                            raw = "\"use strict\"",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    },
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ClassExpression,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), "A"),
                            superClass = null,
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.ClassBody,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.MethodDefinition,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 35, 35)), "constructor"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<BaseNode>(),
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>
                                                {
                                                    new BaseNode(default)
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new BaseNode(default)
                                                        {
                                                            type = NodeType.CallExpression,
                                                            callee = new BaseNode(default)
                                                            {
                                                                type = NodeType.Super,
                                                                loc = new SourceLocation(new Position(1, 40, 40), new Position(1, 45, 45))
                                                            },
                                                            arguments = new List<BaseNode>(),
                                                            loc = new SourceLocation(new Position(1, 40, 40), new Position(1, 47, 47))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 40, 40), new Position(1, 47, 47))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 38, 38), new Position(1, 49, 49))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 35, 35), new Position(1, 49, 49))
                                        },
                                        kind = "constructor",
                                        @static = false,
                                        loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 49, 49))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 50, 50))
                            },
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 50, 50))
                        },
                        loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 51, 51))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 51, 51))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A {'constructor'() {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id =  new IdentifierNode(default, "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key =  new BaseNode(default){type = NodeType.Literal, value = "constructor"},
                                    @static = false,
                                    kind = "constructor",
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
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
            }, new Options {ecmaVersion = 6});

            testFail("class A { constructor() {} 'constructor'() }", "Duplicate constructor in the same class (1:27)", new Options {ecmaVersion = 6});

            testFail("class A { get constructor() {} }", "Constructor can't have get/set modifier (1:14)", new Options {ecmaVersion = 6});

            testFail("class A { *constructor() {} }", "Constructor can't be a generator (1:11)", new Options {ecmaVersion = 6});

            Test("class A {static foo() {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 24, 24))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 24, 24))
                                    },
                                    kind = "method",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 24, 24))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 25, 25))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 25, 25))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A {foo() {} static bar() {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17))
                                    },
                                    kind = "method",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 28, 28)), "bar"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 33, 33))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 33, 33))
                                    },
                                    kind = "method",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 33, 33))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 34, 34))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 34, 34))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("\"use strict\"; (class A { static constructor() { super() }})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "use strict",
                            raw = "\"use strict\"",
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    },
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ClassExpression,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), "A"),
                            superClass = null,
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.ClassBody,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.MethodDefinition,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 32, 32), new Position(1, 43, 43)), "constructor"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<BaseNode>(),
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>
                                                {
                                                    new BaseNode(default)
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new BaseNode(default)
                                                        {
                                                            type = NodeType.CallExpression,
                                                            callee = new BaseNode(default)
                                                            {
                                                                type = NodeType.Super,
                                                                loc = new SourceLocation(new Position(1, 48, 48), new Position(1, 53, 53))
                                                            },
                                                            arguments = new List<BaseNode>(),
                                                            loc = new SourceLocation(new Position(1, 48, 48), new Position(1, 55, 55))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 48, 48), new Position(1, 55, 55))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 46, 46), new Position(1, 57, 57))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 43, 43), new Position(1, 57, 57))
                                        },
                                        kind = "method",
                                        @static = true,
                                        loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 57, 57))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 58, 58))
                            },
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 58, 58))
                        },
                        loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 59, 59))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 59, 59))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { foo() {} bar() {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 18, 18))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 18, 18))
                                    },
                                    kind = "method",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 18, 18))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 22, 22)), "bar"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 27, 27))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 27, 27))
                                    },
                                    kind = "method",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 27, 27))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 28, 28))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { get foo() {} set foo(v) {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22))
                                    },
                                    kind = "get",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 22, 22))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 30, 30)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 31, 31), new Position(1, 32, 32)), "v")
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 34, 34), new Position(1, 36, 36))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 30, 30), new Position(1, 36, 36))
                                    },
                                    kind = "set",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 36, 36))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 37, 37))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { static get foo() {} get foo() {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 29, 29))
                                    },
                                    kind = "get",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 29, 29))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 34, 34), new Position(1, 37, 37)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 40, 40), new Position(1, 42, 42))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 37, 37), new Position(1, 42, 42))
                                    },
                                    kind = "get",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 30, 30), new Position(1, 42, 42))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 43, 43))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 43, 43))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 43, 43))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { static get foo() {} static get bar() {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 29, 29))
                                    },
                                    kind = "get",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 29, 29))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 41, 41), new Position(1, 44, 44)), "bar"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 47, 47), new Position(1, 49, 49))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 44, 44), new Position(1, 49, 49))
                                    },
                                    kind = "get",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 30, 30), new Position(1, 49, 49))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 51, 51))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 51, 51))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 51, 51))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { static get foo() {} static set foo(v) {} get foo() {} set foo(v) {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 24, 24)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 29, 29))
                                    },
                                    kind = "get",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 29, 29))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 41, 41), new Position(1, 44, 44)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 45, 45), new Position(1, 46, 46)), "v")
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 48, 48), new Position(1, 50, 50))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 44, 44), new Position(1, 50, 50))
                                    },
                                    kind = "set",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 30, 30), new Position(1, 50, 50))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 55, 55), new Position(1, 58, 58)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 61, 61), new Position(1, 63, 63))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 58, 58), new Position(1, 63, 63))
                                    },
                                    kind = "get",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 51, 51), new Position(1, 63, 63))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 68, 68), new Position(1, 71, 71)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 72, 72), new Position(1, 73, 73)), "v")
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 75, 75), new Position(1, 77, 77))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 71, 71), new Position(1, 77, 77))
                                    },
                                    kind = "set",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 64, 64), new Position(1, 77, 77))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 78, 78))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 78, 78))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 78, 78))
            }, new Options
            {
                ecmaVersion = 6
            });


            Test("class A { static [foo]() {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 29, 29)),
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 27, 27)),
                                    @static = true,
                                    computed = true,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21)), "foo"),
                                    kind = "method",
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 27, 27)),
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        generator = false,
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 27, 27)),
                                            body = new List<BaseNode>()
                                        },
                                        bexpression = false
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

            Test("class A { static get [foo]() {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 33, 33)),
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 31, 31)),
                                    @static = true,
                                    computed = true,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 25, 25)), "foo"),
                                    kind = "get",
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 31, 31)),
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        generator = false,
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            loc = new SourceLocation(new Position(1, 29, 29), new Position(1, 31, 31)),
                                            body = new List<BaseNode>()
                                        },
                                        bexpression = false
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

            Test("class A { set foo(v) {} get foo() {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), "v")
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 23, 23))
                                    },
                                    kind = "set",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 23, 23))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 31, 31)), "foo"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 34, 34), new Position(1, 36, 36))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 36, 36))
                                    },
                                    kind = "get",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 36, 36))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 38, 38))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { foo() {} get foo() {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 33, 33)),
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 18, 18)),
                                    @static = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), "foo"),
                                    kind = "method",
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 18, 18)),
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        generator = false,
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 18, 18)),
                                            body = new List<BaseNode>()
                                        },
                                        bexpression = false
                                    }
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 31, 31)),
                                    @static = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 26, 26)), "foo"),
                                    kind = "get",
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 31, 31)),
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        generator = false,
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            loc = new SourceLocation(new Position(1, 29, 29), new Position(1, 31, 31)),
                                            body = new List<BaseNode>()
                                        },
                                        bexpression = false
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

            Test("class Semicolon { ; }", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15)), "Semicolon"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21)),
                            body = new List<BaseNode>()
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Computed Properties

            Test("({[x]: 10})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "x"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 10,
                                        raw = "10",
                                        loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                                    },
                                    kind = "init",
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 9, 9))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 10, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({[\"x\" + \"y\"]: 10})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new BaseNode(default)
                                    {
                                        type = NodeType.BinaryExpression,
                                        @operator = "+",
                                        left = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = "x",
                                            raw = "\"x\"",
                                            loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 6, 6))
                                        },
                                        right = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = "y",
                                            raw = "\"y\"",
                                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12))
                                        },
                                        loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 12, 12))
                                    },
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 10,
                                        raw = "10",
                                        loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17))
                                    },
                                    kind = "init",
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 17, 17))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({[x]: function() {}})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "x"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 20, 20))
                                    },
                                    kind = "init",
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 20, 20))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 21, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({[x]: 10, y: 20})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "x"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 10,
                                        raw = "10",
                                        loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                                    },
                                    kind = "init",
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 9, 9))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "y"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        value = 20,
                                        raw = "20",
                                        loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                    },
                                    kind = "init",
                                    method = false,
                                    shorthand = false,
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 16, 16))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({get [x]() {}, set [x](v) {}})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "x"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14))
                                    },
                                    kind = "get",
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 14, 14))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), "x"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new IdentifierNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25)), "v")
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 27, 27), new Position(1, 29, 29))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 29, 29))
                                    },
                                    kind = "set",
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 29, 29))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 30, 30))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 31, 31))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({[x]() {}})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "x"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 10, 10))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10))
                                    },
                                    kind = "init",
                                    method = true,
                                    shorthand = false,
                                    computed = true,
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 10, 10))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 11, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var {[x]: y} = {y}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "x"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "y"),
                                            kind = "init",
                                            method = false,
                                            shorthand = false,
                                            computed = true,
                                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 11, 11))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12))
                                },
                                init = new BaseNode(default)
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), "y"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), "y"),
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 18, 18))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function f({[x]: y}) {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "f"),
                        @params = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ObjectPattern,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "x"),
                                        value = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), "y"),
                                        kind = "init",
                                        method = false,
                                        shorthand = false,
                                        computed = true,
                                        loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 18, 18))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19))
                            }
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var x = {*[test]() { yield *v; }}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "x"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 15, 15)), "test"),
                                            value = new BaseNode(default)
                                            {
                                                type = NodeType.FunctionExpression,
                                                id = null,
                                                @params = new List<BaseNode>(),
                                                fbody = new BaseNode(default)
                                                {
                                                    type = NodeType.BlockStatement,
                                                    body = new List<BaseNode>
                                                    {
                                                        new BaseNode(default)
                                                        {
                                                            type = NodeType.ExpressionStatement,
                                                            expression = new BaseNode(default)
                                                            {
                                                                type = NodeType.YieldExpression,
                                                                argument = new IdentifierNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), "v"),
                                                                @delegate = true,
                                                                loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 29, 29))
                                                            },
                                                            loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 30, 30))
                                                        }
                                                    },
                                                    loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 32, 32))
                                                },
                                                generator = true,
                                                bexpression = false,
                                                loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 32, 32))
                                            },
                                            kind = "init",
                                            method = true,
                                            shorthand = false,
                                            computed = true,
                                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 32, 32))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 33, 33))
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 33, 33))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A {[x]() {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 18, 18)),
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 17, 17)),
                                    @static = false,
                                    computed = true,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "x"),
                                    kind = "method",
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 17, 17)),
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        generator = false,
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 17, 17)),
                                            body = new List<BaseNode>()
                                        },
                                        bexpression = false
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

            testFail("({[x]})", "Unexpected token (1:5)", new Options {ecmaVersion = 6});

            // ES6: Default parameters

            Test("function f([x] = [1]) {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "f"),
                        @params = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.AssignmentPattern,
                                left = new BaseNode(default)
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<BaseNode>
                                    {
                                        new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "x")
                                    },
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14))
                                },
                                right = new BaseNode(default)
                                {
                                    type = NodeType.ArrayExpression,
                                    elements = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 1,
                                            raw = "1",
                                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20))
                                },
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 20, 20))
                            }
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 24, 24))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function f([x] = [1]) { 'use strict' }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "f"),
                        @params = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.AssignmentPattern,
                                left = new BaseNode(default)
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<BaseNode>
                                    {
                                        new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "x")
                                    },
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14))
                                },
                                right = new BaseNode(default)
                                {
                                    type = NodeType.ArrayExpression,
                                    elements = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 1,
                                            raw = "1",
                                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 20, 20))
                                },
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 20, 20))
                            }
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 36, 36)),
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 36, 36)),
                                        value = "use strict",
                                        raw = "'use strict'"
                                    }
                                }
                            },
                            loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 38, 38))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 38, 38))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function f({x} = {x: 10}) {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "f"),
                        @params = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.AssignmentPattern,
                                left = new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "x"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "x"),
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14))
                                },
                                right = new BaseNode(default)
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), "x"),
                                            value = new BaseNode(default)
                                            {
                                                type = NodeType.Literal,
                                                value = 10,
                                                raw = "10",
                                                loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 23, 23))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 24, 24))
                                },
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 24, 24))
                            }
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 28, 28))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 28, 28))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("f = function({x} = {x: 10}) {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "f"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.FunctionExpression,
                                id = null,
                                @params = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.AssignmentPattern,
                                        left = new BaseNode(default)
                                        {
                                            type = NodeType.ObjectPattern,
                                            properties = new List<BaseNode>
                                            {
                                                new BaseNode(default)
                                                {
                                                    type = NodeType.Property,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "x"),
                                                    value = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "x"),
                                                    kind = "init",
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 16, 16))
                                        },
                                        right = new BaseNode(default)
                                        {
                                            type = NodeType.ObjectExpression,
                                            properties = new List<BaseNode>
                                            {
                                                new BaseNode(default)
                                                {
                                                    type = NodeType.Property,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), "x"),
                                                    value = new BaseNode(default)
                                                    {
                                                        type = NodeType.Literal,
                                                        value = 10,
                                                        raw = "10",
                                                        loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                                    },
                                                    kind = "init",
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 25, 25))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 26, 26))
                                        },
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                                    }
                                },
                                fbody = new BaseNode(default)
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<BaseNode>(),
                                    loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 30, 30))
                                },
                                generator = false,
                                bexpression = false,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 30, 30))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({f: function({x} = {x: 10}) {}})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), "f"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.AssignmentPattern,
                                                left = new BaseNode(default)
                                                {
                                                    type = NodeType.ObjectPattern,
                                                    properties = new List<BaseNode>
                                                    {
                                                        new BaseNode(default)
                                                        {
                                                            type = NodeType.Property,
                                                            key = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "x"),
                                                            value = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "x"),
                                                            kind = "init",
                                                            method = false,
                                                            shorthand = true,
                                                            computed = false,
                                                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                                                        }
                                                    },
                                                    loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 17, 17))
                                                },
                                                right = new BaseNode(default)
                                                {
                                                    type = NodeType.ObjectExpression,
                                                    properties = new List<BaseNode>
                                                    {
                                                        new BaseNode(default)
                                                        {
                                                            type = NodeType.Property,
                                                            key = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), "x"),
                                                            value = new BaseNode(default)
                                                            {
                                                                type = NodeType.Literal,
                                                                value = 10,
                                                                raw = "10",
                                                                loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 26, 26))
                                                            },
                                                            kind = "init",
                                                            method = false,
                                                            shorthand = false,
                                                            computed = false,
                                                            loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 26, 26))
                                                        }
                                                    },
                                                    loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 27, 27))
                                                },
                                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 27, 27))
                                            }
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 29, 29), new Position(1, 31, 31))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 31, 31))
                                    },
                                    kind = "init",
                                    method = false,
                                    shorthand = false,
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 31, 31))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 32, 32))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({f({x} = {x: 10}) {}})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), "f"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.AssignmentPattern,
                                                left = new BaseNode(default)
                                                {
                                                    type = NodeType.ObjectPattern,
                                                    properties = new List<BaseNode>
                                                    {
                                                        new BaseNode(default)
                                                        {
                                                            type = NodeType.Property,
                                                            key = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "x"),
                                                            value = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "x"),
                                                            kind = "init",
                                                            method = false,
                                                            shorthand = true,
                                                            computed = false,
                                                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6))
                                                        }
                                                    },
                                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7))
                                                },
                                                right = new BaseNode(default)
                                                {
                                                    type = NodeType.ObjectExpression,
                                                    properties = new List<BaseNode>
                                                    {
                                                        new BaseNode(default)
                                                        {
                                                            type = NodeType.Property,
                                                            key = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "x"),
                                                            value = new BaseNode(default)
                                                            {
                                                                type = NodeType.Literal,
                                                                value = 10,
                                                                raw = "10",
                                                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                                            },
                                                            kind = "init",
                                                            method = false,
                                                            shorthand = false,
                                                            computed = false,
                                                            loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 16, 16))
                                                        }
                                                    },
                                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 17, 17))
                                                },
                                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                                            }
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 21, 21))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 21, 21))
                                    },
                                    kind = "init",
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 21, 21))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(class {f({x} = {x: 10}) {}})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ClassExpression,
                            superClass = null,
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.ClassBody,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.MethodDefinition,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "f"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<BaseNode>
                                            {
                                                new BaseNode(default)
                                                {
                                                    type = NodeType.AssignmentPattern,
                                                    left = new BaseNode(default)
                                                    {
                                                        type = NodeType.ObjectPattern,
                                                        properties = new List<BaseNode>
                                                        {
                                                            new BaseNode(default)
                                                            {
                                                                type = NodeType.Property,
                                                                key = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "x"),
                                                                value = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "x"),
                                                                kind = "init",
                                                                method = false,
                                                                shorthand = true,
                                                                computed = false,
                                                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12))
                                                            }
                                                        },
                                                        loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13))
                                                    },
                                                    right = new BaseNode(default)
                                                    {
                                                        type = NodeType.ObjectExpression,
                                                        properties = new List<BaseNode>
                                                        {
                                                            new BaseNode(default)
                                                            {
                                                                type = NodeType.Property,
                                                                key = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), "x"),
                                                                value = new BaseNode(default)
                                                                {
                                                                    type = NodeType.Literal,
                                                                    value = 10,
                                                                    raw = "10",
                                                                    loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                                                                },
                                                                kind = "init",
                                                                method = false,
                                                                shorthand = false,
                                                                computed = false,
                                                                loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22))
                                                            }
                                                        },
                                                        loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 23, 23))
                                                    },
                                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 23, 23))
                                                }
                                            },
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 27, 27))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 27, 27))
                                        },
                                        kind = "method",
                                        @static = false,
                                        loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 27, 27))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 28, 28))
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 28, 28))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 29, 29))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(({x} = {x: 10}) => {})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.AssignmentPattern,
                                    left = new BaseNode(default)
                                    {
                                        type = NodeType.ObjectPattern,
                                        properties = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.Property,
                                                key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "x"),
                                                value = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "x"),
                                                kind = "init",
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 5, 5))
                                    },
                                    right = new BaseNode(default)
                                    {
                                        type = NodeType.ObjectExpression,
                                        properties = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.Property,
                                                key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "x"),
                                                value = new BaseNode(default)
                                                {
                                                    type = NodeType.Literal,
                                                    value = 10,
                                                    raw = "10",
                                                    loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                                },
                                                kind = "init",
                                                method = false,
                                                shorthand = false,
                                                computed = false,
                                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 15, 15))
                                    },
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 15, 15))
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x = function(y = 1) {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.FunctionExpression,
                                id = null,
                                @params = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.AssignmentPattern,
                                        left = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "y"),
                                        right = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            value = 1,
                                            raw = "1",
                                            loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18))
                                        },
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 18, 18))
                                    }
                                },
                                fbody = new BaseNode(default)
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<BaseNode>(),
                                    loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                                },
                                generator = false,
                                bexpression = false,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 22, 22))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function f(a = 1) {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "f"),
                        @params = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.AssignmentPattern,
                                left = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "a"),
                                right = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 1,
                                    raw = "1",
                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                                },
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 16, 16))
                            }
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x = { f: function(a=1) {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "f"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<BaseNode>
                                            {
                                                new BaseNode(default)
                                                {
                                                    type = NodeType.AssignmentPattern,
                                                    left = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), "a"),
                                                    right = new BaseNode(default)
                                                    {
                                                        type = NodeType.Literal,
                                                        value = 1,
                                                        raw = "1",
                                                        loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21))
                                                    },
                                                    loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 21, 21))
                                                }
                                            },
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 25, 25))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 25, 25))
                                        },
                                        kind = "init",
                                        method = false,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 25, 25))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 27, 27))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x = { f(a=1) {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "x"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "f"),
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<BaseNode>
                                            {
                                                new BaseNode(default)
                                                {
                                                    type = NodeType.AssignmentPattern,
                                                    left = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a"),
                                                    right = new BaseNode(default)
                                                    {
                                                        type = NodeType.Literal,
                                                        value = 1,
                                                        raw = "1",
                                                        loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11))
                                                    },
                                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 11, 11))
                                                }
                                            },
                                            fbody = new BaseNode(default)
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<BaseNode>(),
                                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 15, 15))
                                        },
                                        kind = "init",
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 15, 15))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Rest parameters

            Test("function f(a, ...b) {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "f"),
                        @params = new List<BaseNode>
                        {
                            new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "a"),
                            new BaseNode(default)
                            {
                                type = NodeType.RestElement,
                                argument = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), "b")
                            }
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Destructured Parameters

            Test("function x([ a, b ]){}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "x"),
                        @params = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ArrayPattern,
                                elements = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "a"),
                                    new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), "b")
                                },
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19))
                            }
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function x({ a, b }){}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "x"),
                        @params = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ObjectPattern,
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "a"),
                                        value = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "a"),
                                        kind = "init",
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), "b"),
                                        value = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), "b"),
                                        kind = "init",
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 19, 19))
                            }
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>(),
                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            testFail("function x(...[ a, b ]){}", "Unexpected token (1:14)", new Options {ecmaVersion = 6});

            testFail("function x({ a: { w, x }, b: [y, z] }, ...[a, b, c]){}", "Unexpected token (1:42)", new Options {ecmaVersion = 6});

            Test("(function x([ a, b ]){})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "x"),
                            @params = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<BaseNode>
                                    {
                                        new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "a"),
                                        new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), "b")
                                    },
                                    loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 20, 20))
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 23, 23))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(function x({ a, b }){})", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "x"),
                            @params = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "a"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "a"),
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15))
                                        },
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), "b"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), "b"),
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 20, 20))
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 23, 23))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 23, 23))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            testFail("(function x(...[ a, b ]){})", "Unexpected token (1:15)", new Options {ecmaVersion = 6});
            testFail("var a = { set foo(...v) {} };", "Setter cannot use rest params (1:18)", new Options {ecmaVersion = 6});
            testFail("class a { set foo(...v) {} };", "Setter cannot use rest params (1:18)", new Options {ecmaVersion = 6});

            testFail("(function x({ a: { w, x }, b: [y, z] }, ...[a, b, c]){})", "Unexpected token (1:43)", new Options {ecmaVersion = 6});

            Test("({ x([ a, b ]){} })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "x"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.ArrayPattern,
                                                elements = new List<BaseNode>
                                                {
                                                    new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a"),
                                                    new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "b")
                                                },
                                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 13, 13))
                                            }
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 16, 16))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16))
                                    },
                                    kind = "init",
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 16, 16))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 18, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({ x(...[ a, b ]){} })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "x"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.RestElement,
                                                argument = new BaseNode(default)
                                                {
                                                    type = NodeType.ArrayPattern,
                                                    elements = new List<BaseNode>
                                                    {
                                                        new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "a"),
                                                        new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "b")
                                                    },
                                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 16, 16))
                                                }
                                            }
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 19, 19))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 19, 19))
                                    },
                                    kind = "init",
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 19, 19))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 21, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 7
            });

            Test("({ x({ a: { w, x }, b: [y, z] }, ...[a, b, c]){} })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "x"),
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.ObjectPattern,
                                                properties = new List<BaseNode>
                                                {
                                                    new BaseNode(default)
                                                    {
                                                        type = NodeType.Property,
                                                        key = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a"),
                                                        value = new BaseNode(default)
                                                        {
                                                            type = NodeType.ObjectPattern,
                                                            properties = new List<BaseNode>
                                                            {
                                                                new BaseNode(default)
                                                                {
                                                                    type = NodeType.Property,
                                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "w"),
                                                                    value = new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "w"),
                                                                    kind = "init",
                                                                    method = false,
                                                                    shorthand = true,
                                                                    computed = false,
                                                                    loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13))
                                                                },
                                                                new BaseNode(default)
                                                                {
                                                                    type = NodeType.Property,
                                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "x"),
                                                                    value = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "x"),
                                                                    kind = "init",
                                                                    method = false,
                                                                    shorthand = true,
                                                                    computed = false,
                                                                    loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16))
                                                                }
                                                            },
                                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 18, 18))
                                                        },
                                                        kind = "init",
                                                        method = false,
                                                        shorthand = false,
                                                        computed = false,
                                                        loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 18, 18))
                                                    },
                                                    new BaseNode(default)
                                                    {
                                                        type = NodeType.Property,
                                                        key = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21)), "b"),
                                                        value = new BaseNode(default)
                                                        {
                                                            type = NodeType.ArrayPattern,
                                                            elements = new List<BaseNode>
                                                            {
                                                                new IdentifierNode(new SourceLocation(new Position(1, 24, 24), new Position(1, 25, 25)), "y"),
                                                                new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 28, 28)), "z")
                                                            },
                                                            loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 29, 29))
                                                        },
                                                        kind = "init",
                                                        method = false,
                                                        shorthand = false,
                                                        computed = false,
                                                        loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 29, 29))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 31, 31))
                                            },
                                            new BaseNode(default)
                                            {
                                                type = NodeType.RestElement,
                                                argument = new BaseNode(default)
                                                {
                                                    type = NodeType.ArrayPattern,
                                                    elements = new List<BaseNode>
                                                    {
                                                        new IdentifierNode(new SourceLocation(new Position(1, 37, 37), new Position(1, 38, 38)), "a"),
                                                        new IdentifierNode(new SourceLocation(new Position(1, 40, 40), new Position(1, 41, 41)), "b"),
                                                        new IdentifierNode(new SourceLocation(new Position(1, 43, 43), new Position(1, 44, 44)), "c")
                                                    },
                                                    loc = new SourceLocation(new Position(1, 36, 36), new Position(1, 45, 45))
                                                }
                                            }
                                        },
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<BaseNode>(),
                                            loc = new SourceLocation(new Position(1, 46, 46), new Position(1, 48, 48))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 48, 48))
                                    },
                                    kind = "init",
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 48, 48))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 50, 50))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 51, 51))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 51, 51))
            }, new Options
            {
                ecmaVersion = 7
            });

            Test("(...a) => {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.RestElement,
                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "a")
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 12, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(a, ...b) => {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "a"),
                                new BaseNode(default)
                                {
                                    type = NodeType.RestElement,
                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "b")
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 15, 15))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({ a }) => {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "a"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "a"),
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 6, 6))
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({ a }, ...b) => {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "a"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "a"),
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 6, 6))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.RestElement,
                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "b")
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 19, 19))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19))
            }, new Options
            {
                ecmaVersion = 6
            });

            testFail("(...[a, b]) => {}", "Unexpected token (1:4)", new Options {ecmaVersion = 6});

            testFail("(a, ...[b]) => {}", "Unexpected token (1:7)", new Options {ecmaVersion = 6});

            Test("({ a: [a, b] }, ...c) => {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "a"),
                                            value = new BaseNode(default)
                                            {
                                                type = NodeType.ArrayPattern,
                                                elements = new List<BaseNode>
                                                {
                                                    new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a"),
                                                    new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "b")
                                                },
                                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 12, 12))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 12, 12))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 14, 14))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.RestElement,
                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "c")
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 27, 27))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({ a: b, c }, [d, e], ...f) => {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "a"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "b"),
                                            kind = "init",
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7))
                                        },
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "c"),
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "c"),
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 12, 12))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<BaseNode>
                                    {
                                        new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "d"),
                                        new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), "e")
                                    },
                                    loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 20, 20))
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.RestElement,
                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 26, 26)), "f")
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>(),
                                loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 33, 33))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: SpreadElement

            Test("[...a] = b", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new BaseNode(default)
                            {
                                type = NodeType.ArrayPattern,
                                elements = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.RestElement,
                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 5, 5)), "a"),
                                        loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 5, 5))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 6, 6))
                            },
                            right = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "b"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("[a, ...b] = c", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new BaseNode(default)
                            {
                                type = NodeType.ArrayPattern,
                                elements = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "a"),
                                    new BaseNode(default)
                                    {
                                        type = NodeType.RestElement,
                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "b"),
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 8, 8))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                            },
                            right = new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "c"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("[{ a, b }, ...c] = d", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new BaseNode(default)
                            {
                                type = NodeType.ArrayPattern,
                                elements = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ObjectPattern,
                                        properties = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.Property,
                                                key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "a"),
                                                value = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4)), "a"),
                                                kind = "init",
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 4, 4))
                                            },
                                            new BaseNode(default)
                                            {
                                                type = NodeType.Property,
                                                key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "b"),
                                                value = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "b"),
                                                kind = "init",
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 9, 9))
                                    },
                                    new BaseNode(default)
                                    {
                                        type = NodeType.RestElement,
                                        argument = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "c"),
                                        loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 15, 15))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 16, 16))
                            },
                            right = new IdentifierNode(new SourceLocation(new Position(1, 19, 19), new Position(1, 20, 20)), "d"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("[a, ...[b, c]] = d", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new BaseNode(default)
                            {
                                type = NodeType.ArrayPattern,
                                elements = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "a"),
                                    new BaseNode(default)
                                    {
                                        type = NodeType.RestElement,
                                        argument = new BaseNode(default)
                                        {
                                            type = NodeType.ArrayPattern,
                                            elements = new List<BaseNode>
                                            {
                                                new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "b"),
                                                new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "c")
                                            },
                                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 13, 13))
                                        },
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                            },
                            right = new IdentifierNode(new SourceLocation(new Position(1, 17, 17), new Position(1, 18, 18)), "d"),
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var [...a] = b", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.RestElement,
                                            argument = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a"),
                                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 9, 9))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 10, 10))
                                },
                                init = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "b"),
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var [a, ...b] = c", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<BaseNode>
                                    {
                                        new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "a"),
                                        new BaseNode(default)
                                        {
                                            type = NodeType.RestElement,
                                            argument = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "b"),
                                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 12, 12))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13))
                                },
                                init = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 17, 17)), "c"),
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 17, 17))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var [{ a, b }, ...c] = d", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.ObjectPattern,
                                            properties = new List<BaseNode>
                                            {
                                                new BaseNode(default)
                                                {
                                                    type = NodeType.Property,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a"),
                                                    value = new IdentifierNode(new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8)), "a"),
                                                    kind = "init",
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 8, 8))
                                                },
                                                new BaseNode(default)
                                                {
                                                    type = NodeType.Property,
                                                    key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "b"),
                                                    value = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "b"),
                                                    kind = "init",
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 13, 13))
                                        },
                                        new BaseNode(default)
                                        {
                                            type = NodeType.RestElement,
                                            argument = new IdentifierNode(new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)), "c"),
                                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 19, 19))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 20, 20))
                                },
                                init = new IdentifierNode(new SourceLocation(new Position(1, 23, 23), new Position(1, 24, 24)), "d"),
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 24, 24))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var [a, ...[b, c]] = d", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<BaseNode>
                                    {
                                        new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "a"),
                                        new BaseNode(default)
                                        {
                                            type = NodeType.RestElement,
                                            argument = new BaseNode(default)
                                            {
                                                type = NodeType.ArrayPattern,
                                                elements = new List<BaseNode>
                                                {
                                                    new IdentifierNode(new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)), "b"),
                                                    new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 16, 16)), "c")
                                                },
                                                loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17))
                                            },
                                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 17, 17))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 18, 18))
                                },
                                init = new IdentifierNode(new SourceLocation(new Position(1, 21, 21), new Position(1, 22, 22)), "d"),
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 22, 22))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22))
            }, new Options
            {
                ecmaVersion = 7
            });

            Test("func(...a)", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), "func"),
                            arguments = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.SpreadElement,
                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a"),
                                    loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 9, 9))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 10, 10))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("func(a, ...b)", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), "func"),
                            arguments = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "a"),
                                new BaseNode(default)
                                {
                                    type = NodeType.SpreadElement,
                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "b"),
                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 12, 12))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("func(...a, b)", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 4, 4)), "func"),
                            arguments = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.SpreadElement,
                                    loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 9, 9)),
                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 9, 9)), "a")
                                },
                                new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "b")
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("/[a-z]/u", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            regex = new RegexNode
                            {
                                pattern = "[a-z]",
                                flags = "u"
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 8, 8))
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("/[\\uD834\\uDF06-\\uD834\\uDF08a-z]/u", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            regex = new RegexNode
                            {
                                pattern = "[\\uD834\\uDF06-\\uD834\\uDF08a-z]",
                                flags = "u"
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("do {} while (false) foo();", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 26, 26)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.DoWhileStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 19, 19)),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 5, 5)),
                            body = new List<BaseNode>()
                        },
                        test = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 18, 18)),
                            value = false,
                            raw = "false"
                        }
                    },
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 26, 26)),
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 25, 25)),
                            callee = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 23, 23)), "foo"),
                            arguments = new List<BaseNode>()
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            // Harmony Invalid syntax

            testFail("0o", "Expected number in radix 8 (1:2)", new Options {ecmaVersion = 6});

            testFail("0o1a", "Identifier directly after number (1:3)", new Options {ecmaVersion = 6});

            testFail("0o9", "Expected number in radix 8 (1:2)", new Options {ecmaVersion = 6});

            testFail("0o18", "Unexpected token (1:3)", new Options {ecmaVersion = 6});

            testFail("0O", "Expected number in radix 8 (1:2)", new Options {ecmaVersion = 6});

            testFail("0O1a", "Identifier directly after number (1:3)", new Options {ecmaVersion = 6});

            testFail("0O9", "Expected number in radix 8 (1:2)", new Options {ecmaVersion = 6});

            testFail("0O18", "Unexpected token (1:3)", new Options {ecmaVersion = 6});

            testFail("0b", "Expected number in radix 2 (1:2)", new Options {ecmaVersion = 6});

            testFail("0b1a", "Identifier directly after number (1:3)", new Options {ecmaVersion = 6});

            testFail("0b9", "Expected number in radix 2 (1:2)", new Options {ecmaVersion = 6});

            testFail("0b18", "Unexpected token (1:3)", new Options {ecmaVersion = 6});

            testFail("0b12", "Unexpected token (1:3)", new Options {ecmaVersion = 6});

            testFail("0B", "Expected number in radix 2 (1:2)", new Options {ecmaVersion = 6});

            testFail("0B1a", "Identifier directly after number (1:3)", new Options {ecmaVersion = 6});

            testFail("0B9", "Expected number in radix 2 (1:2)", new Options {ecmaVersion = 6});

            testFail("0B18", "Unexpected token (1:3)", new Options {ecmaVersion = 6});

            testFail("0B12", "Unexpected token (1:3)", new Options {ecmaVersion = 6});

            testFail("\"\\u{110000}\"", "Code point out of bounds (1:4)", new Options {ecmaVersion = 6});

            testFail("\"\\u{}\"", "Bad character escape sequence (1:4)", new Options {ecmaVersion = 6});

            testFail("\"\\u{FFFF\"", "Bad character escape sequence (1:4)", new Options {ecmaVersion = 6});

            testFail("\"\\u{FFZ}\"", "Bad character escape sequence (1:4)", new Options {ecmaVersion = 6});

            testFail("[v] += ary", "Assigning to rvalue (1:0)", new Options {ecmaVersion = 6});

            testFail("[2] = 42", "Assigning to rvalue (1:1)", new Options {ecmaVersion = 6});

            testFail("({ obj:20 }) = 42", "Parenthesized pattern (1:0)", new Options {ecmaVersion = 6});

            testFail("( { get x() {} } = 0)", "Object pattern can't contain getter or setter (1:8)", new Options {ecmaVersion = 6});

            testFail("x \n is y", "Unexpected token (2:4)", new Options {ecmaVersion = 6});

            testFail("x \n isnt y", "Unexpected token (2:6)", new Options {ecmaVersion = 6});

            testFail("function default() {}", "Unexpected keyword 'default' (1:9)", new Options {ecmaVersion = 6});

            testFail("function hello() {'use strict'; ({ i: 10, s(eval) { } }); }", "Binding eval in strict mode (1:44)", new Options {ecmaVersion = 6});

            testFail("function a() { \"use strict\"; ({ b(t, t) { } }); }", "Argument name clash (1:37)", new Options {ecmaVersion = 6});

            testFail("var super", "Unexpected keyword 'super' (1:4)", new Options {ecmaVersion = 6});

            testFail("var default", "Unexpected keyword 'default' (1:4)", new Options {ecmaVersion = 6});

            testFail("let default", "Unexpected token (1:4)", new Options {ecmaVersion = 6});

            testFail("const default", "Unexpected keyword 'default' (1:6)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; ({ v: eval } = obj)", "Assigning to eval in strict mode (1:20)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; ({ v: arguments } = obj)", "Assigning to arguments in strict mode (1:20)", new Options {ecmaVersion = 6});

            testFail("for (let x = 42 in list) process(x);", "Unexpected token (1:16)", new Options {ecmaVersion = 6});

            testFail("for (let x = 42 of list) process(x);", "Unexpected token (1:16)", new Options {ecmaVersion = 6});

            testFail("import foo", "Unexpected token (1:10)", new Options {ecmaVersion = 6, sourceType = "module"});

            testFail("import { foo, bar }", "Unexpected token (1:19)", new Options {ecmaVersion = 6, sourceType = "module"});

            testFail("import foo from bar", "Unexpected token (1:16)", new Options {ecmaVersion = 6, sourceType = "module"});

            testFail("((a)) => 42", "Parenthesized pattern (1:1)", new Options {ecmaVersion = 6});

            testFail("(a, (b)) => 42", "Parenthesized pattern (1:4)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; (eval = 10) => 42", "Assigning to eval in strict mode (1:15)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; eval => 42", "Binding eval in strict mode (1:14)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; arguments => 42", "Binding arguments in strict mode (1:14)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; (eval, a) => 42", "Binding eval in strict mode (1:15)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; (arguments, a) => 42", "Binding arguments in strict mode (1:15)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; (eval, a = 10) => 42", "Binding eval in strict mode (1:15)", new Options {ecmaVersion = 6});

            testFail("(a, a) => 42", "Argument name clash (1:4)", new Options {ecmaVersion = 6});

            testFail("function foo(a, a = 2) {}", "Argument name clash (1:16)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; (a, a) => 42", "Argument name clash (1:18)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; (a) => 00", "Invalid number (1:21)", new Options {ecmaVersion = 6});

            testFail("() <= 42", "Unexpected token (1:1)", new Options {ecmaVersion = 6});

            testFail("(10) => 00", "Assigning to rvalue (1:1)", new Options {ecmaVersion = 6});

            testFail("(10, 20) => 00", "Assigning to rvalue (1:1)", new Options {ecmaVersion = 6});

            testFail("yield v", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

            testFail("yield 10", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

            testFail("void { [1, 2]: 3 };", "Unexpected token (1:9)", new Options {ecmaVersion = 6});

            testFail("let [this] = [10]", "Unexpected keyword 'this' (1:5)", new Options {ecmaVersion = 6});
            testFail("let {this} = x", "Unexpected keyword 'this' (1:5)", new Options {ecmaVersion = 6});
            testFail("let [function] = [10]", "Unexpected keyword 'function' (1:5)", new Options {ecmaVersion = 6});
            testFail("let [function] = x", "Unexpected keyword 'function' (1:5)", new Options {ecmaVersion = 6});
            testFail("([function] = [10])", "Unexpected token (1:10)", new Options {ecmaVersion = 6});
            testFail("([this] = [10])", "Assigning to rvalue (1:2)", new Options {ecmaVersion = 6});
            testFail("({this} = x)", "Unexpected keyword 'this' (1:2)", new Options {ecmaVersion = 6});
            testFail("var x = {this}", "Unexpected keyword 'this' (1:9)", new Options {ecmaVersion = 6});

            Test("yield* 10", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            @operator = "*",
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 5, 5)), "yield"),
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 10,
                                raw = "10",
                                loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 9, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 9, 9))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("e => yield* 10", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<BaseNode>
                            {
                                new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 1, 1)), "e")
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BinaryExpression,
                                @operator = "*",
                                left = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 10, 10)), "yield"),
                                right = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 10,
                                    raw = "10",
                                    loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 14, 14))
                                },
                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 14, 14))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            testFail("(function () { yield 10 })", "Unexpected token (1:21)", new Options {ecmaVersion = 6});

            Test("(function () { yield* 10 })", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                body = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new BaseNode(default)
                                        {
                                            type = NodeType.BinaryExpression,
                                            @operator = "*",
                                            left = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20)), "yield"),
                                            right = new BaseNode(default)
                                            {
                                                type = NodeType.Literal,
                                                value = 10,
                                                raw = "10",
                                                loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 24, 24))
                                            },
                                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24))
                                        },
                                        loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 24, 24))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 26, 26))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 26, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("let + 1", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(default, "let"),
                            @operator = "+",
                            right = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 1,
                                raw = "1"
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("var let = 1", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(default, "let"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 1,
                                    raw = "1"
                                }
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options {ecmaVersion = 6});

            testFail("'use strict'; let + 1", "The keyword 'let' is reserved (1:14)", new Options {ecmaVersion = 6});

            Test("var yield = 2", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new IdentifierNode(default, "yield"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 2,
                                    raw = "2"
                                }
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options {ecmaVersion = 6});

            testFail("(function() { \"use strict\"; f(yield v) })", "The keyword 'yield' is reserved (1:30)", new Options {ecmaVersion = 6});

            testFail("var obj = { *test** }", "Unexpected token (1:17)", new Options {ecmaVersion = 6});

            testFail("class A extends yield B { }", "Unexpected token (1:22)", new Options {ecmaVersion = 6});

            testFail("class default", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

            testFail("`test", "Unterminated template (1:1)", new Options {ecmaVersion = 6});

            testFail("switch `test`", "Unexpected token (1:7)", new Options {ecmaVersion = 6});

            testFail("`hello ${10 `test`", "Unexpected token (1:18)", new Options {ecmaVersion = 6});

            testFail("`hello ${10;test`", "Unexpected token (1:11)", new Options {ecmaVersion = 6});

            testFail("function a() 1 // expression closure is not supported", "Unexpected token (1:13)", new Options {ecmaVersion = 6});

            testFail("({ \"chance\" }) = obj", "Unexpected token (1:12)", new Options {ecmaVersion = 6});

            testFail("({ 42 }) = obj", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

            testFail("function f(a, ...b, c)", "Comma is not permitted after the rest element (1:18)", new Options {ecmaVersion = 6});

            testFail("function f(a, ...b = 0)", "Unexpected token (1:19)", new Options {ecmaVersion = 6});

            testFail("function x(...{ a }){}", "Unexpected token (1:14)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; function x(a, { a }){}", "Argument name clash (1:30)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; function x({ b: { a } }, [{ b: { a } }]){}", "Argument name clash (1:47)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; function x(a, ...[a]){}", "Unexpected token (1:31)", new Options {ecmaVersion = 6});

            testFail("(...a, b) => {}", "Comma is not permitted after the rest element (1:5)", new Options {ecmaVersion = 6});

            testFail("([ 5 ]) => {}", "Assigning to rvalue (1:3)", new Options {ecmaVersion = 6});

            testFail("({ 5 }) => {}", "Unexpected token (1:5)", new Options {ecmaVersion = 6});

            testFail("(...[ 5 ]) => {}", "Unexpected token (1:6)", new Options {ecmaVersion = 7});

            Test("[...{ a }] = b", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            testFail("[...a, b] = c", "Comma is not permitted after the rest element (1:5)", new Options {ecmaVersion = 6});

            testFail("({ t(eval) { \"use strict\"; } });", "Binding eval in strict mode (1:5)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; `${test}\\02`;", "Octal literal in strict mode (1:22)", new Options {ecmaVersion = 6});

            testFail("if (1) import \"acorn\";", "'import' and 'export' may only appear at the top level (1:7)", new Options {ecmaVersion = 6});

            testFail("[...a, ] = b", "Comma is not permitted after the rest element (1:5)", new Options {ecmaVersion = 6});

            testFail("if (b,...a, );", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

            testFail("(b, ...a)", "Unexpected token (1:4)", new Options {ecmaVersion = 6});

            testFail("switch (cond) { case 10: let a = 20; ", "Unexpected token (1:37)", new Options {ecmaVersion = 6});

            testFail("\"use strict\"; (eval) => 42", "Binding eval in strict mode (1:15)", new Options {ecmaVersion = 6});

            testFail("(eval) => { \"use strict\"; 42 }", "Binding eval in strict mode (1:1)", new Options {ecmaVersion = 6});

            testFail("({ get test() { } }) => 42", "Object pattern can't contain getter or setter (1:7)", new Options {ecmaVersion = 6});

            /* Regression tests */

            // # https://github.com/ternjs/acorn/issues/127
            Test("doSmth(`${x} + ${y} = ${x + y}`)", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.CallExpression,
                            callee = new IdentifierNode(default, "doSmth"),
                            arguments = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.TemplateLiteral,
                                    expressions = new List<BaseNode>
                                    {
                                        new IdentifierNode(default, "x"),
                                        new IdentifierNode(default, "y"),
                                        new BaseNode(default)
                                        {
                                            type = NodeType.BinaryExpression,
                                            left = new IdentifierNode(default, "x"),
                                            @operator = "+",
                                            right = new IdentifierNode(default, "y")
                                        }
                                    },
                                    quasis = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode("", ""),
                                            tail = false
                                        },
                                        new BaseNode(default)
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode(" + ", " + "),
                                            tail = false
                                        },
                                        new BaseNode(default)
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode(" = ", " = "),
                                            tail = false
                                        },
                                        new BaseNode(default)
                                        {
                                            type = NodeType.TemplateElement,
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
            Test("function normal(x, y = 10) {}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(default, "normal"),
                        @params = new List<BaseNode>
                        {
                            new IdentifierNode(default, "x"),
                            new BaseNode(default)
                            {
                                type = NodeType.AssignmentPattern,
                                left = new IdentifierNode(default, "y"),
                                right = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    value = 10,
                                    raw = "10"
                                }
                            }
                        },
                        generator = false,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>()
                        },
                        bexpression = false
                    }
                }
            }, new Options {ecmaVersion = 6});

            // test preserveParens option with arrow functions
            Test("() => 42", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            bexpression = true
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, preserveParens = true});

            // https://github.com/ternjs/acorn/issues/161
            Test("import foo, * as bar from 'baz';", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ImportDeclaration,
                        specifiers = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.ImportDefaultSpecifier,
                                local = new IdentifierNode(default, "foo")
                            },
                            new BaseNode(default)
                            {
                                type = NodeType.ImportNamespaceSpecifier,
                                local = new IdentifierNode(default, "bar")
                            }
                        },
                        source = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            value = "baz",
                            raw = "'baz'"
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            // https://github.com/ternjs/acorn/issues/173
            Test("`{${x}}`, `}`", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.SequenceExpression,
                            expressions = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.TemplateLiteral,
                                    expressions = new List<BaseNode>
                                    {
                                        new IdentifierNode(default, "x")
                                    },
                                    quasis = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode("{", "{"),
                                            tail = false
                                        },
                                        new BaseNode(default)
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode("}", "}"),
                                            tail = true
                                        }
                                    }
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.TemplateLiteral,
                                    expressions = new List<BaseNode>(),
                                    quasis = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.TemplateElement,
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
            Test("var {get} = obj;", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new IdentifierNode(default, "get"),
                                            kind = "init",
                                            value = new IdentifierNode(default, "get")
                                        }
                                    }
                                },
                                init = new IdentifierNode(default, "obj")
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options {ecmaVersion = 6});

            // Destructuring defaults (https://github.com/ternjs/acorn/issues/181)

            Test("var {propName: localVar = defaultValue} = obj", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 45, 45)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 45, 45)),
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 45, 45)),
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 39, 39)),
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 38, 38)),
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 13, 13)), "propName"),
                                            value = new BaseNode(default)
                                            {
                                                type = NodeType.AssignmentPattern,
                                                loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 38, 38)),
                                                left = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 23, 23)), "localVar"),
                                                right = new IdentifierNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 38, 38)), "defaultValue")
                                            },
                                            kind = "init"
                                        }
                                    }
                                },
                                init = new IdentifierNode(new SourceLocation(new Position(1, 42, 42), new Position(1, 45, 45)), "obj")
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var {propName = defaultValue} = obj", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 35, 35)),
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 29, 29)),
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 28, 28)),
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 13, 13)), "propName"),
                                            kind = "init",
                                            value = new BaseNode(default)
                                            {
                                                type = NodeType.AssignmentPattern,
                                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 28, 28)),
                                                left = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 13, 13)), "propName"),
                                                right = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 28, 28)), "defaultValue")
                                            }
                                        }
                                    }
                                },
                                init = new IdentifierNode(new SourceLocation(new Position(1, 32, 32), new Position(1, 35, 35)), "obj")
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var {get = defaultValue} = obj", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 30, 30)),
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 30, 30)),
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 24, 24)),
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 23, 23)),
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8)), "get"),
                                            kind = "init",
                                            value = new BaseNode(default)
                                            {
                                                type = NodeType.AssignmentPattern,
                                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 23, 23)),
                                                left = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 8, 8)), "get"),
                                                right = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 23, 23)), "defaultValue")
                                            }
                                        }
                                    }
                                },
                                init = new IdentifierNode(new SourceLocation(new Position(1, 27, 27), new Position(1, 30, 30)), "obj")
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var [localVar = defaultValue] = obj", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 35, 35)),
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 35, 35)),
                                id = new BaseNode(default)
                                {
                                    type = NodeType.ArrayPattern,
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 29, 29)),
                                    elements = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.AssignmentPattern,
                                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 28, 28)),
                                            left = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 13, 13)), "localVar"),
                                            right = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 28, 28)), "defaultValue")
                                        }
                                    }
                                },
                                init = new IdentifierNode(new SourceLocation(new Position(1, 32, 32), new Position(1, 35, 35)), "obj")
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({x = 0} = obj)", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 14, 14)),
                            @operator = "=",
                            left = new BaseNode(default)
                            {
                                type = NodeType.ObjectPattern,
                                loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 8, 8)),
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)),
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), "x"),
                                        kind = "init",
                                        value = new BaseNode(default)
                                        {
                                            type = NodeType.AssignmentPattern,
                                            loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)),
                                            left = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), "x"),
                                            right = new BaseNode(default)
                                            {
                                                type = NodeType.Literal,
                                                loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)),
                                                value = 0
                                            }
                                        }
                                    }
                                }
                            },
                            right = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14)), "obj")
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({x = 0}) => x", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                            id = null,
                            generator = false,
                            bexpression = true,
                            @params = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ObjectPattern,
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 8, 8)),
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)),
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), "x"),
                                            kind = "init",
                                            value = new BaseNode(default)
                                            {
                                                type = NodeType.AssignmentPattern,
                                                loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 7, 7)),
                                                left = new IdentifierNode(new SourceLocation(new Position(1, 2, 2), new Position(1, 3, 3)), "x"),
                                                right = new BaseNode(default)
                                                {
                                                    type = NodeType.Literal,
                                                    loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)),
                                                    value = 0
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            fbody = new IdentifierNode(new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)), "x")
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("[a, {b: {c = 1}}] = arr", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                        expression = new BaseNode(default)
                        {
                            type = NodeType.AssignmentExpression,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                            @operator = "=",
                            left = new BaseNode(default)
                            {
                                type = NodeType.ArrayPattern,
                                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 17, 17)),
                                elements = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 1, 1), new Position(1, 2, 2)), "a"),
                                    new BaseNode(default)
                                    {
                                        type = NodeType.ObjectPattern,
                                        loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 16, 16)),
                                        properties = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.Property,
                                                loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 15, 15)),
                                                method = false,
                                                shorthand = false,
                                                computed = false,
                                                key = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "b"),
                                                value = new BaseNode(default)
                                                {
                                                    type = NodeType.ObjectPattern,
                                                    loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 15, 15)),
                                                    properties = new List<BaseNode>
                                                    {
                                                        new BaseNode(default)
                                                        {
                                                            type = NodeType.Property,
                                                            loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)),
                                                            method = false,
                                                            shorthand = true,
                                                            computed = false,
                                                            key = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "c"),
                                                            kind = "init",
                                                            value = new BaseNode(default)
                                                            {
                                                                type = NodeType.AssignmentPattern,
                                                                loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 14, 14)),
                                                                left = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 10, 10)), "c"),
                                                                right = new BaseNode(default)
                                                                {
                                                                    type = NodeType.Literal,
                                                                    loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 14, 14)),
                                                                    value = 1
                                                                }
                                                            }
                                                        }
                                                    }
                                                },
                                                kind = "init"
                                            }
                                        }
                                    }
                                }
                            },
                            right = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 23, 23)), "arr")
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("for ({x = 0} in arr);", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForInStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                        left = new BaseNode(default)
                        {
                            type = NodeType.ObjectPattern,
                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 12, 12)),
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11)),
                                    method = false,
                                    shorthand = true,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "x"),
                                    kind = "init",
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.AssignmentPattern,
                                        loc = new SourceLocation(new Position(1, 6, 6), new Position(1, 11, 11)),
                                        left = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "x"),
                                        right = new BaseNode(default)
                                        {
                                            type = NodeType.Literal,
                                            loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)),
                                            value = 0
                                        }
                                    }
                                }
                            }
                        },
                        right = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 19, 19)), "arr"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.EmptyStatement,
                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 21, 21))
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            testFail("obj = {x = 0}", "Shorthand property assignments are valid only in destructuring patterns (1:9)", new Options {ecmaVersion = 6});

            testFail("f({x = 0})", "Shorthand property assignments are valid only in destructuring patterns (1:5)", new Options {ecmaVersion = 6});

            // https://github.com/ternjs/acorn/issues/191

            Test("try {} catch ({message}) {}", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                body = new List<BaseNode>
                {
                    new TryStatementNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 27, 27)),
                        new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 6, 6)),
                            body = new List<BaseNode>()
                        },
                        new BaseNode(default)
                        {
                            type = NodeType.CatchClause,
                            loc = new SourceLocation(new Position(1, 7, 7), new Position(1, 27, 27)),
                            param = new BaseNode(default)
                            {
                                type = NodeType.ObjectPattern,
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 23, 23)),
                                properties = new List<BaseNode>
                                {
                                    new BaseNode(default)
                                    {
                                        type = NodeType.Property,
                                        loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 22, 22)),
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 22, 22)), "message"),
                                        kind = "init",
                                        value = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 22, 22)), "message")
                                    }
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.BlockStatement,
                                loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 27, 27)),
                                body = new List<BaseNode>()
                            }
                        },
                        null)
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            // https://github.com/ternjs/acorn/issues/192

            Test("class A { static() {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 23, 23)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 23, 23)),
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 21, 21)),
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 16, 16)), "static"),
                                    @static = false,
                                    kind = "method",
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        loc = new SourceLocation(new Position(1, 16, 16), new Position(1, 21, 21)),
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        generator = false,
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 21, 21)),
                                            body = new List<BaseNode>()
                                        },
                                        bexpression = false
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

            Test("for (const x of list) process(x);", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ForOfStatement,
                        left = new BaseNode(default)
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12)), "x"),
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 12, 12))
                                }
                            },
                            kind = "const",
                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 12, 12))
                        },
                        right = new IdentifierNode(new SourceLocation(new Position(1, 16, 16), new Position(1, 20, 20)), "list"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new BaseNode(default)
                            {
                                type = NodeType.CallExpression,
                                callee = new IdentifierNode(new SourceLocation(new Position(1, 22, 22), new Position(1, 29, 29)), "process"),
                                arguments = new List<BaseNode>
                                {
                                    new IdentifierNode(new SourceLocation(new Position(1, 30, 30), new Position(1, 31, 31)), "x")
                                },
                                loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 32, 32))
                            },
                            loc = new SourceLocation(new Position(1, 22, 22), new Position(1, 33, 33))
                        },
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33))
            }, new Options {ecmaVersion = 6});

            Test("class A { *static() {} }", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 24, 24)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 24, 24)),
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 22, 22)),
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 11, 11), new Position(1, 17, 17)), "static"),
                                    @static = false,
                                    kind = "method",
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 22, 22)),
                                        id = null,
                                        @params = new List<BaseNode>(),
                                        generator = true,
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            loc = new SourceLocation(new Position(1, 20, 20), new Position(1, 22, 22)),
                                            body = new List<BaseNode>()
                                        },
                                        bexpression = false
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

            Test("`${/\\d/.exec('1')[0]}`", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)),
                        expression = new BaseNode(default)
                        {
                            type = NodeType.TemplateLiteral,
                            loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 22, 22)),
                            expressions = new List<BaseNode>
                            {
                                new MemberExpressionNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 20, 20)),
                                new BaseNode(default)
                                    {
                                        type = NodeType.CallExpression,
                                        loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 17, 17)),
                                        callee = new MemberExpressionNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 12, 12)),
                                            new BaseNode(default)
                                            {
                                                type = NodeType.Literal,
                                                loc = new SourceLocation(new Position(1, 3, 3), new Position(1, 7, 7)),
                                                regex = new RegexNode
                                                {
                                                    pattern = "\\d",
                                                    flags = ""
                                                },
                                                raw = "/\\d/"
                                            },
                                            new IdentifierNode(new SourceLocation(new Position(1, 8, 8), new Position(1, 12, 12)), "exec"),
                                            false),
                                        arguments = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.Literal,
                                                loc = new SourceLocation(new Position(1, 13, 13), new Position(1, 16, 16)),
                                                value = "1",
                                                raw = "'1'"
                                            }
                                        }
                                    },
                                new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 19, 19)),
                                        value = 0,
                                        raw = "0"
                                    },
                                true)
                            },
                            quasis = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.TemplateElement,
                                    loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 1, 1)),
                                    value = new TemplateNode("", ""),
                                    tail = false
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.TemplateElement,
                                    loc = new SourceLocation(new Position(1, 21, 21), new Position(1, 21, 21)),
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

            Test("var _𐒦 = 10;", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12)),
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), "_𐒦"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12)),
                                    value = 10,
                                    raw = "10"
                                }
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("var 𫠝_ = 10;", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 12, 12)),
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)), "𫠝_"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    loc = new SourceLocation(new Position(1, 10, 10), new Position(1, 12, 12)),
                                    value = 10,
                                    raw = "10"
                                }
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("var _\\u{104A6} = 10;", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 19, 19)),
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14)), "_𐒦"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 19, 19)),
                                    value = 10,
                                    raw = "10"
                                }
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("let [x,] = [1]", new BaseNode(default)
            {
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 14, 14)),
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 14, 14)),
                                id = new BaseNode(default)
                                {
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 8, 8)),
                                    elements = new List<BaseNode>
                                    {
                                        new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "x")
                                    },
                                    type = NodeType.ArrayPattern
                                },
                                init = new BaseNode(default)
                                {
                                    loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 14, 14)),
                                    elements = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)),
                                            value = 1,
                                            raw = "1",
                                            type = NodeType.Literal
                                        }
                                    },
                                    type = NodeType.ArrayExpression
                                },
                                type = NodeType.VariableDeclarator
                            }
                        },
                        kind = "let",
                        type = NodeType.VariableDeclaration
                    }
                },
                type = NodeType.Program
            }, new Options {ecmaVersion = 6});

            Test("let {x} = y", new BaseNode(default)
            {
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 11, 11)),
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 11, 11)),
                                id = new BaseNode(default)
                                {
                                    loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 7, 7)),
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            loc = new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)),
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "x"),
                                            kind = "init",
                                            value = new IdentifierNode(new SourceLocation(new Position(1, 5, 5), new Position(1, 6, 6)), "x"),
                                            type = NodeType.Property
                                        }
                                    },
                                    type = NodeType.ObjectPattern
                                },
                                init = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 11, 11)), "y"),
                                type = NodeType.VariableDeclarator
                            }
                        },
                        kind = "let",
                        type = NodeType.VariableDeclaration
                    }
                },
                type = NodeType.Program
            }, new Options {ecmaVersion = 6});

            Test("[x,,] = 1", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("for (var [name, value] in obj) {}", new BaseNode(default)
            {
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        left = new BaseNode(default)
                        {
                            declarations = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    id = new BaseNode(default)
                                    {
                                        elements = new List<BaseNode>
                                        {
                                            new IdentifierNode(default, "name"),
                                            new IdentifierNode(default, "value")
                                        },
                                        type = NodeType.ArrayPattern
                                    },
                                    init = null,
                                    type = NodeType.VariableDeclarator
                                }
                            },
                            kind = "var",
                            type = NodeType.VariableDeclaration
                        },
                        right = new IdentifierNode(default, "obj"),
                        fbody = new BaseNode(default)
                        {
                            body = new List<BaseNode>(),
                            type = NodeType.BlockStatement
                        },
                        type = NodeType.ForInStatement
                    }
                },
                sourceType = "script",
                type = NodeType.Program
            }, new Options {ecmaVersion = 6});

            testFail("let [x]", "Complex binding patterns require an initialization value (1:7)", new Options {ecmaVersion = 6});
            testFail("var [x]", "Complex binding patterns require an initialization value (1:7)", new Options {ecmaVersion = 6});
            testFail("var _𖫵 = 11;", "Unexpected character '𖫵' (1:5)", new Options {ecmaVersion = 6});
            testFail("var 𫠞_ = 12;", "Unexpected character '𫠞' (1:4)", new Options {ecmaVersion = 6});
            testFail("var 𫠝_ = 10;", "Unexpected character '𫠝' (1:4)", new Options {ecmaVersion = 5});
            testFail("if (1) let x = 10;", "Unexpected token (1:7)", new Options {ecmaVersion = 6});
            testFail("for (;;) const x = 10;", "Unexpected token (1:9)", new Options {ecmaVersion = 6});
            testFail("while (1) function foo(){}", "Unexpected token (1:10)", new Options {ecmaVersion = 6});
            testFail("if (1) ; else class Cls {}", "Unexpected token (1:14)", new Options {ecmaVersion = 6});

            testFail("'use strict'; [...eval] = arr", "Assigning to eval in strict mode (1:18)", new Options {ecmaVersion = 6});
            testFail("'use strict'; ({eval = defValue} = obj)", "Assigning to eval in strict mode (1:16)", new Options {ecmaVersion = 6});

            testFail("[...eval] = arr", "Assigning to eval in strict mode (1:4)", new Options {ecmaVersion = 6, sourceType = "module"});

            testFail("function* y({yield}) {}", "Can not use 'yield' as identifier inside a generator (1:13)", new Options {ecmaVersion = 6});

            Test("function foo() { new.target; }", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(default, "foo"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.MetaProperty,
                                        meta = new IdentifierNode(default, "new"),
                                        property = new IdentifierNode(default, "target")
                                    }
                                }
                            }
                        },
                        generator = false,
                        bexpression = false
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            testFail("new.prop", "The only valid meta property for new is new.target (1:4)", new Options {ecmaVersion = 6});
            testFail("new.target", "new.target can only be used in functions (1:0)", new Options {ecmaVersion = 6});

            Test("export default function foo() {} false", new BaseNode(default)
            {
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        declaration = new BaseNode(default)
                        {
                            id = new IdentifierNode(default, "foo"),
                            generator = false,
                            bexpression = false,
                            @params = new List<BaseNode>(),
                            fbody = new BaseNode(default)
                            {
                                body = new List<BaseNode>(),
                                type = NodeType.BlockStatement
                            },
                            type = NodeType.FunctionDeclaration
                        },
                        type = NodeType.ExportDefaultDeclaration
                    },
                    new BaseNode(default)
                    {
                        expression = new BaseNode(default)
                        {
                            value = false,
                            raw = "false",
                            type = NodeType.Literal
                        },
                        type = NodeType.ExpressionStatement
                    }
                },
                sourceType = "module",
                type = NodeType.Program
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            // https://github.com/ternjs/acorn/issues/274

            testFail("`\\07`", "Octal literal in strict mode (1:1)", new Options {ecmaVersion = 6});

            // https://github.com/ternjs/acorn/issues/277

            testFail("x = { method() 42 }", "Unexpected token (1:15)", new Options {ecmaVersion = 6});

            testFail("x = { get method() 42 }", "Unexpected token (1:19)", new Options {ecmaVersion = 6});

            testFail("x = { set method(val) v = val }", "Unexpected token (1:22)", new Options {ecmaVersion = 6});

            // https://github.com/ternjs/acorn/issues/278

//            testFail("/\\u{110000}/u", "~", new Options {ecmaVersion = 6});

            // https://github.com/ternjs/acorn/issues/279

            testFail("super", "'super' outside of function or class (1:0)", new Options {ecmaVersion = 6});

            // https://github.com/ternjs/acorn/issues/275

            testFail("class A { get prop(x) {} }", "getter should have no params (1:18)", new Options {ecmaVersion = 6});
            testFail("class A { set prop() {} }", "setter should have exactly one param (1:18)", new Options {ecmaVersion = 6});
            testFail("class A { set prop(x, y) {} }", "setter should have exactly one param (1:18)", new Options {ecmaVersion = 6});

            // https://github.com/ternjs/acorn/issues/276

            testFail("({ __proto__: 1, __proto__: 2 })", "Redefinition of __proto__ property (1:17)", new Options {ecmaVersion = 6});
            testFail("({ '__proto__': 1, __proto__: 2 })", "Redefinition of __proto__ property (1:19)", new Options {ecmaVersion = 6});
            Test("({ ['__proto__']: 1, __proto__: 2 })", new BaseNode(default) { }, new Options {ecmaVersion = 6});
            Test("({ __proto__() { return 1 }, __proto__: 2 })", new BaseNode(default) { }, new Options {ecmaVersion = 6});
            Test("({ get __proto__() { return 1 }, __proto__: 2 })", new BaseNode(default) { }, new Options {ecmaVersion = 6});
            Test("({ __proto__, __proto__: 2 })", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("export default /foo/", new BaseNode(default) { }, new Options {ecmaVersion = 6, sourceType = "module"});

            Test("var await = 0", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.VariableDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 13, 13)),
                        declarations = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.VariableDeclarator,
                                loc = new SourceLocation(new Position(1, 4, 4), new Position(1, 13, 13)),
                                id = new IdentifierNode(new SourceLocation(new Position(1, 4, 4), new Position(1, 9, 9)), "await"),
                                init = new BaseNode(default)
                                {
                                    type = NodeType.Literal,
                                    loc = new SourceLocation(new Position(1, 12, 12), new Position(1, 13, 13)),
                                    value = 0,
                                    raw = "0"
                                }
                            }
                        },
                        kind = "var"
                    }
                },
                sourceType = "script"
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "script",
                allowReserved = false
            });

            testFail("var await = 0", "The keyword 'await' is reserved (1:4)", new Options
            {
                ecmaVersion = 6,
                sourceType = "module",
                allowReserved = false
            });

            // https://github.com/ternjs/acorn/issues/363

            Test("/[a-z]/gimuy", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.Literal,
                            regex = new RegexNode
                            {
                                pattern = "[a-z]",
                                flags = "gimuy"
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});
            testFail("/[a-z]/s", "Invalid regular expression flag (1:1)", new Options {ecmaVersion = 6});

            testFail("[...x in y] = []", "Assigning to rvalue (1:4)", new Options {ecmaVersion = 6});

            testFail("export let x = a; export function x() {}", "Identifier 'x' has already been declared (1:34)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("export let [{x = 2}] = a; export {x}", "Duplicate export 'x' (1:34)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("export default 100; export default 3", "Duplicate export 'default' (1:27)", new Options {ecmaVersion = 6, sourceType = "module"});

            Test("(([,]) => 0)", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ArrowFunctionExpression,
                            @params = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<BaseNode> {null}
                                }
                            },
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.Literal,
                                value = 0,
                                raw = "0"
                            },
                            bexpression = true
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            // 'eval' and 'arguments' are not reserved word, but those can not be a BindingIdentifier.

            Test("function foo() { return {arguments} }", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 37, 37)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                        generator = false,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 37, 37)),
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ReturnStatement,
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 35, 35)),
                                    argument = new BaseNode(default)
                                    {
                                        type = NodeType.ObjectExpression,
                                        loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 35, 35)),
                                        properties = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.Property,
                                                loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 34, 34)),
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 34, 34)), "arguments"),
                                                kind = "init",
                                                value = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 34, 34)), "arguments")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            Test("function foo() { return {eval} }", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 32, 32)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                        generator = false,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 32, 32)),
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ReturnStatement,
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 30, 30)),
                                    argument = new BaseNode(default)
                                    {
                                        type = NodeType.ObjectExpression,
                                        loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 30, 30)),
                                        properties = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.Property,
                                                loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 29, 29)),
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 29, 29)), "eval"),
                                                kind = "init",
                                                value = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 29, 29)), "eval")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            Test("function foo() { 'use strict'; return {arguments} }", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 51, 51)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 51, 51)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                        generator = false,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 51, 51)),
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 30, 30)),
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 29, 29)),
                                        value = "use strict",
                                        raw = "'use strict'"
                                    }
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.ReturnStatement,
                                    loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 49, 49)),
                                    argument = new BaseNode(default)
                                    {
                                        type = NodeType.ObjectExpression,
                                        loc = new SourceLocation(new Position(1, 38, 38), new Position(1, 49, 49)),
                                        properties = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.Property,
                                                loc = new SourceLocation(new Position(1, 39, 39), new Position(1, 48, 48)),
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new IdentifierNode(new SourceLocation(new Position(1, 39, 39), new Position(1, 48, 48)), "arguments"),
                                                kind = "init",
                                                value = new IdentifierNode(new SourceLocation(new Position(1, 39, 39), new Position(1, 48, 48)), "arguments")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            Test("function foo() { 'use strict'; return {eval} }", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 46, 46)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                        generator = false,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 46, 46)),
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 30, 30)),
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 29, 29)),
                                        value = "use strict",
                                        raw = "'use strict'"
                                    }
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.ReturnStatement,
                                    loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 44, 44)),
                                    argument = new BaseNode(default)
                                    {
                                        type = NodeType.ObjectExpression,
                                        loc = new SourceLocation(new Position(1, 38, 38), new Position(1, 44, 44)),
                                        properties = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.Property,
                                                loc = new SourceLocation(new Position(1, 39, 39), new Position(1, 43, 43)),
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new IdentifierNode(new SourceLocation(new Position(1, 39, 39), new Position(1, 43, 43)), "eval"),
                                                kind = "init",
                                                value = new IdentifierNode(new SourceLocation(new Position(1, 39, 39), new Position(1, 43, 43)), "eval")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            Test("function foo() { return {yield} }", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 33, 33)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 9, 9), new Position(1, 12, 12)), "foo"),
                        generator = false,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 33, 33)),
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ReturnStatement,
                                    loc = new SourceLocation(new Position(1, 17, 17), new Position(1, 31, 31)),
                                    argument = new BaseNode(default)
                                    {
                                        type = NodeType.ObjectExpression,
                                        loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 31, 31)),
                                        properties = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.Property,
                                                loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 30, 30)),
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 30, 30)), "yield"),
                                                kind = "init",
                                                value = new IdentifierNode(new SourceLocation(new Position(1, 25, 25), new Position(1, 30, 30)), "yield")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            testFail("function foo() { 'use strict'; return {yield} }", "The keyword 'yield' is reserved (1:39)", new Options {ecmaVersion = 6});

            testFail("function foo() { 'use strict'; var {arguments} = {} }", "Binding arguments in strict mode (1:36)", new Options {ecmaVersion = 6});
            testFail("function foo() { 'use strict'; var {eval} = {} }", "Binding eval in strict mode (1:36)", new Options {ecmaVersion = 6});
            testFail("function foo() { 'use strict'; var {arguments = 1} = {} }", "Binding arguments in strict mode (1:36)", new Options {ecmaVersion = 6});
            testFail("function foo() { 'use strict'; var {eval = 1} = {} }", "Binding eval in strict mode (1:36)", new Options {ecmaVersion = 6});

            // cannot use yield expressions in parameters.
            testFail("function* wrap() { function* foo(a = 1 + (yield)) {} }", "Yield expression cannot be a default value (1:42)", new Options {ecmaVersion = 6});
            testFail("function* wrap() { return (a = 1 + (yield)) => a }", "Yield expression cannot be a default value (1:36)", new Options {ecmaVersion = 6});

            // can use yield expressions in parameters if it's inside of a nested generator.
            Test("function* foo(a = function*(b) { yield b }) { }", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 47, 47)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 47, 47)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), "foo"),
                        generator = true,
                        bexpression = false,
                        @params = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.AssignmentPattern,
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 42, 42)),
                                left = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "a"),
                                right = new BaseNode(default)
                                {
                                    type = NodeType.FunctionExpression,
                                    loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 42, 42)),
                                    id = null,
                                    generator = true,
                                    bexpression = false,
                                    @params = new List<BaseNode>
                                    {
                                        new IdentifierNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 29, 29)), "b")
                                    },
                                    fbody = new BaseNode(default)
                                    {
                                        type = NodeType.BlockStatement,
                                        loc = new SourceLocation(new Position(1, 31, 31), new Position(1, 42, 42)),
                                        body = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.ExpressionStatement,
                                                loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 40, 40)),
                                                expression = new BaseNode(default)
                                                {
                                                    type = NodeType.YieldExpression,
                                                    loc = new SourceLocation(new Position(1, 33, 33), new Position(1, 40, 40)),
                                                    @delegate = false,
                                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 39, 39), new Position(1, 40, 40)), "b")
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            loc = new SourceLocation(new Position(1, 44, 44), new Position(1, 47, 47)),
                            body = new List<BaseNode>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            // 'yield' as function names.

            Test("function* yield() {}", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 20, 20)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), "yield"),
                        generator = true,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20)),
                            body = new List<BaseNode>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            Test("({*yield() {}})", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 15, 15)),
                        expression = new BaseNode(default)
                        {
                            type = NodeType.ObjectExpression,
                            loc = new SourceLocation(new Position(1, 1, 1), new Position(1, 14, 14)),
                            properties = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.Property,
                                    loc = new SourceLocation(new Position(1, 2, 2), new Position(1, 13, 13)),
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 3, 3), new Position(1, 8, 8)), "yield"),
                                    kind = "init",
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 13, 13)),
                                        id = null,
                                        generator = true,
                                        bexpression = false,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            loc = new SourceLocation(new Position(1, 11, 11), new Position(1, 13, 13)),
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            Test("class A {*yield() {}}", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ClassDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 21, 21)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 6, 6), new Position(1, 7, 7)), "A"),
                        superClass = null,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.ClassBody,
                            loc = new SourceLocation(new Position(1, 8, 8), new Position(1, 21, 21)),
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.MethodDefinition,
                                    loc = new SourceLocation(new Position(1, 9, 9), new Position(1, 20, 20)),
                                    computed = false,
                                    key = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 15, 15)), "yield"),
                                    @static = false,
                                    kind = "method",
                                    value = new BaseNode(default)
                                    {
                                        type = NodeType.FunctionExpression,
                                        loc = new SourceLocation(new Position(1, 15, 15), new Position(1, 20, 20)),
                                        id = null,
                                        generator = true,
                                        bexpression = false,
                                        @params = new List<BaseNode>(),
                                        fbody = new BaseNode(default)
                                        {
                                            type = NodeType.BlockStatement,
                                            loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 20, 20)),
                                            body = new List<BaseNode>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            testFail("(function* yield() {})", "Can not use 'yield' as identifier inside a generator (1:11)", new Options {ecmaVersion = 6});
            testFail("function* wrap() {\nfunction* yield() {}\n}", "Can not use 'yield' as identifier inside a generator (2:10)", new Options {ecmaVersion = 6});
            Test("function* wrap() {\n({*yield() {}})\n}", new BaseNode(default) { }, new Options {ecmaVersion = 6});
            Test("function* wrap() {\nclass A {*yield() {}}\n}", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            // Forbid yield expressions in default parameters:
            testFail("function* foo(a = yield b) {}", "Yield expression cannot be a default value (1:18)", new Options {ecmaVersion = 6});
            testFail("(function* foo(a = yield b) {})", "Yield expression cannot be a default value (1:19)", new Options {ecmaVersion = 6});
            testFail("({*foo(a = yield b) {}})", "Yield expression cannot be a default value (1:11)", new Options {ecmaVersion = 6});
            testFail("(class {*foo(a = yield b) {}})", "Yield expression cannot be a default value (1:17)", new Options {ecmaVersion = 6});
            testFail("function* foo(a = class extends (yield b) {}) {}", "Yield expression cannot be a default value (1:33)", new Options {ecmaVersion = 6});

            // Allow yield expressions inside functions in default parameters:
            Test("function* foo(a = function* foo() { yield b }) {}", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 49, 49)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 49, 49)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), "foo"),
                        generator = true,
                        bexpression = false,
                        @params = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.AssignmentPattern,
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 45, 45)),
                                left = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "a"),
                                right = new BaseNode(default)
                                {
                                    type = NodeType.FunctionExpression,
                                    loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 45, 45)),
                                    id = new IdentifierNode(new SourceLocation(new Position(1, 28, 28), new Position(1, 31, 31)), "foo"),
                                    generator = true,
                                    bexpression = false,
                                    @params = new List<BaseNode>(),
                                    fbody = new BaseNode(default)
                                    {
                                        type = NodeType.BlockStatement,
                                        loc = new SourceLocation(new Position(1, 34, 34), new Position(1, 45, 45)),
                                        body = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.ExpressionStatement,
                                                loc = new SourceLocation(new Position(1, 36, 36), new Position(1, 43, 43)),
                                                expression = new BaseNode(default)
                                                {
                                                    type = NodeType.YieldExpression,
                                                    loc = new SourceLocation(new Position(1, 36, 36), new Position(1, 43, 43)),
                                                    @delegate = false,
                                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 42, 42), new Position(1, 43, 43)), "b")
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            loc = new SourceLocation(new Position(1, 47, 47), new Position(1, 49, 49)),
                            body = new List<BaseNode>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            Test("function* foo(a = {*bar() { yield b }}) {}", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 42, 42)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 42, 42)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), "foo"),
                        generator = true,
                        bexpression = false,
                        @params = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.AssignmentPattern,
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 38, 38)),
                                left = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "a"),
                                right = new BaseNode(default)
                                {
                                    type = NodeType.ObjectExpression,
                                    loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 38, 38)),
                                    properties = new List<BaseNode>
                                    {
                                        new BaseNode(default)
                                        {
                                            type = NodeType.Property,
                                            loc = new SourceLocation(new Position(1, 19, 19), new Position(1, 37, 37)),
                                            method = true,
                                            shorthand = false,
                                            computed = false,
                                            key = new IdentifierNode(new SourceLocation(new Position(1, 20, 20), new Position(1, 23, 23)), "bar"),
                                            kind = "init",
                                            value = new BaseNode(default)
                                            {
                                                type = NodeType.FunctionExpression,
                                                loc = new SourceLocation(new Position(1, 23, 23), new Position(1, 37, 37)),
                                                id = null,
                                                generator = true,
                                                bexpression = false,
                                                @params = new List<BaseNode>(),
                                                fbody = new BaseNode(default)
                                                {
                                                    type = NodeType.BlockStatement,
                                                    loc = new SourceLocation(new Position(1, 26, 26), new Position(1, 37, 37)),
                                                    body = new List<BaseNode>
                                                    {
                                                        new BaseNode(default)
                                                        {
                                                            type = NodeType.ExpressionStatement,
                                                            loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 35, 35)),
                                                            expression = new BaseNode(default)
                                                            {
                                                                type = NodeType.YieldExpression,
                                                                loc = new SourceLocation(new Position(1, 28, 28), new Position(1, 35, 35)),
                                                                @delegate = false,
                                                                argument = new IdentifierNode(new SourceLocation(new Position(1, 34, 34), new Position(1, 35, 35)), "b")
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
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            loc = new SourceLocation(new Position(1, 40, 40), new Position(1, 42, 42)),
                            body = new List<BaseNode>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            Test("function* foo(a = class {*bar() { yield b }}) {}", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(1, 48, 48)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 13, 13)), "foo"),
                        generator = true,
                        bexpression = false,
                        @params = new List<BaseNode>
                        {
                            new BaseNode(default)
                            {
                                type = NodeType.AssignmentPattern,
                                loc = new SourceLocation(new Position(1, 14, 14), new Position(1, 44, 44)),
                                left = new IdentifierNode(new SourceLocation(new Position(1, 14, 14), new Position(1, 15, 15)), "a"),
                                right = new BaseNode(default)
                                {
                                    type = NodeType.ClassExpression,
                                    loc = new SourceLocation(new Position(1, 18, 18), new Position(1, 44, 44)),
                                    id = null,
                                    superClass = null,
                                    fbody = new BaseNode(default)
                                    {
                                        type = NodeType.ClassBody,
                                        loc = new SourceLocation(new Position(1, 24, 24), new Position(1, 44, 44)),
                                        body = new List<BaseNode>
                                        {
                                            new BaseNode(default)
                                            {
                                                type = NodeType.MethodDefinition,
                                                loc = new SourceLocation(new Position(1, 25, 25), new Position(1, 43, 43)),
                                                computed = false,
                                                key = new IdentifierNode(new SourceLocation(new Position(1, 26, 26), new Position(1, 29, 29)), "bar"),
                                                @static = false,
                                                kind = "method",
                                                value = new BaseNode(default)
                                                {
                                                    type = NodeType.FunctionExpression,
                                                    loc = new SourceLocation(new Position(1, 29, 29), new Position(1, 43, 43)),
                                                    id = null,
                                                    generator = true,
                                                    bexpression = false,
                                                    @params = new List<BaseNode>(),
                                                    fbody = new BaseNode(default)
                                                    {
                                                        type = NodeType.BlockStatement,
                                                        loc = new SourceLocation(new Position(1, 32, 32), new Position(1, 43, 43)),
                                                        body = new List<BaseNode>
                                                        {
                                                            new BaseNode(default)
                                                            {
                                                                type = NodeType.ExpressionStatement,
                                                                loc = new SourceLocation(new Position(1, 34, 34), new Position(1, 41, 41)),
                                                                expression = new BaseNode(default)
                                                                {
                                                                    type = NodeType.YieldExpression,
                                                                    loc = new SourceLocation(new Position(1, 34, 34), new Position(1, 41, 41)),
                                                                    @delegate = false,
                                                                    argument = new IdentifierNode(new SourceLocation(new Position(1, 40, 40), new Position(1, 41, 41)), "b")
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
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            loc = new SourceLocation(new Position(1, 46, 46), new Position(1, 48, 48)),
                            body = new List<BaseNode>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            // Distinguish ParenthesizedExpression or ArrowFunctionExpression
            Test("function* wrap() {\n(a = yield b)\n}", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 34)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 34)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "wrap"),
                        generator = true,
                        bexpression = false,
                        @params = new List<BaseNode>(),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            loc = new SourceLocation(new Position(1, 17, 17), new Position(3, 1, 34)),
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    loc = new SourceLocation(new Position(2, 0, 19), new Position(2, 13, 32)),
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.AssignmentExpression,
                                        loc = new SourceLocation(new Position(2, 1, 20), new Position(2, 12, 31)),
                                        @operator = "=",
                                        left = new IdentifierNode(new SourceLocation(new Position(2, 1, 20), new Position(2, 2, 21)), "a"),
                                        right = new BaseNode(default)
                                        {
                                            type = NodeType.YieldExpression,
                                            loc = new SourceLocation(new Position(2, 5, 24), new Position(2, 12, 31)),
                                            @delegate = false,
                                            argument = new IdentifierNode(new SourceLocation(new Position(2, 11, 30), new Position(2, 12, 31)), "b")
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            testFail("function* wrap() {\n(a = yield b) => a\n}", "Yield expression cannot be a default value (2:5)", new Options {ecmaVersion = 6});

            Test("function* wrap() {\n({a = yield b} = obj)\n}", new BaseNode(default)
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 42)),
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        loc = new SourceLocation(new Position(1, 0, 0), new Position(3, 1, 42)),
                        id = new IdentifierNode(new SourceLocation(new Position(1, 10, 10), new Position(1, 14, 14)), "wrap"),
                        @params = new List<BaseNode>(),
                        generator = true,
                        bexpression = false,
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            loc = new SourceLocation(new Position(1, 17, 17), new Position(3, 1, 42)),
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    loc = new SourceLocation(new Position(2, 0, 19), new Position(2, 21, 40)),
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.AssignmentExpression,
                                        loc = new SourceLocation(new Position(2, 1, 20), new Position(2, 20, 39)),
                                        @operator = "=",
                                        left = new BaseNode(default)
                                        {
                                            type = NodeType.ObjectPattern,
                                            loc = new SourceLocation(new Position(2, 1, 20), new Position(2, 14, 33)),
                                            properties = new List<BaseNode>
                                            {
                                                new BaseNode(default)
                                                {
                                                    type = NodeType.Property,
                                                    loc = new SourceLocation(new Position(2, 2, 21), new Position(2, 13, 32)),
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new IdentifierNode(new SourceLocation(new Position(2, 2, 21), new Position(2, 3, 22)), "a"),
                                                    kind = "init",
                                                    value = new BaseNode(default)
                                                    {
                                                        type = NodeType.AssignmentPattern,
                                                        loc = new SourceLocation(new Position(2, 2, 21), new Position(2, 13, 32)),
                                                        left = new IdentifierNode(new SourceLocation(new Position(2, 2, 21), new Position(2, 3, 22)), "a"),
                                                        right = new BaseNode(default)
                                                        {
                                                            type = NodeType.YieldExpression,
                                                            loc = new SourceLocation(new Position(2, 6, 25), new Position(2, 13, 32)),
                                                            @delegate = false,
                                                            argument = new IdentifierNode(new SourceLocation(new Position(2, 12, 31), new Position(2, 13, 32)), "b")
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        right = new IdentifierNode(new SourceLocation(new Position(2, 17, 36), new Position(2, 20, 39)), "obj")
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            Test("export default class Foo {}++x", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        declaration = new BaseNode(default)
                        {
                            type = NodeType.ClassDeclaration,
                            id = new IdentifierNode(default, "Foo"),
                            superClass = null,
                            fbody = new BaseNode(default)
                            {
                                type = NodeType.ClassBody,
                                body = new List<BaseNode>()
                            }
                        }
                    },
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = true,
                            argument = new IdentifierNode(default, "x")
                        }
                    }
                },
                sourceType = "module"
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            Test("function *f() { yield\n{}/1/g\n}", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new IdentifierNode(default, "f"),
                        fbody = new BaseNode(default)
                        {
                            type = NodeType.BlockStatement,
                            body = new List<BaseNode>
                            {
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.YieldExpression,
                                        argument = null,
                                        @delegate= false
                                    }
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<BaseNode>()
                                },
                                new BaseNode(default)
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new BaseNode(default)
                                    {
                                        type = NodeType.Literal,
                                        raw = "/1/g",
                                        regex = new RegexNode
                                        {
                                            pattern = "1",
                                            flags = "g"
                                        }
                                    }
                                }
                            }
                        },
                        generator = true
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("class B extends A { foo(a = super.foo()) { return a }}", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            testFail("function* wrap() {\n({a = yield b} = obj) => a\n}", "Yield expression cannot be a default value (2:6)", new Options {ecmaVersion = 6});

            // invalid syntax '*foo: 1'
            testFail("({*foo: 1})", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

            Test("export { x as y } from './y.js';\nexport { x as z } from './z.js';",
                new BaseNode(default) { }, new Options {sourceType = "module", ecmaVersion = 6});

            Test("export { default as y } from './y.js';\nexport default 42;",
                new BaseNode(default) { }, new Options {sourceType = "module", ecmaVersion = 6});

            testFail("export { default} from './y.js';\nexport default 42;", "Duplicate export 'default' (2:7)", new Options {sourceType = "module", ecmaVersion = 6});

            testFail("foo: class X {}", "Invalid labelled declaration (1:5)", new Options {ecmaVersion = 6});

            testFail("'use strict'; bar: function x() {}", "Invalid labelled declaration (1:19)", new Options {ecmaVersion = 6});

            testFail("({x, y}) = {}", "Parenthesized pattern (1:0)", new Options {ecmaVersion = 6});

            Test("[x, (y), {z, u: (v)}] = foo", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("export default function(x) {};", new BaseNode(default) {body = new List<BaseNode> {new BaseNode(default) { }, new BaseNode(default) { }}}, new Options {ecmaVersion = 6, sourceType = "module"});

            testFail("var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:17)", new Options {ecmaVersion = 6});

            testFail("{ var foo = 1; let foo = 1; }", "Identifier 'foo' has already been declared (1:19)", new Options {ecmaVersion = 6});

            testFail("let foo = 1; var foo = 1;", "Identifier 'foo' has already been declared (1:17)", new Options {ecmaVersion = 6});

            testFail("let foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:17)", new Options {ecmaVersion = 6});

            testFail("var foo = 1; const foo = 1;", "Identifier 'foo' has already been declared (1:19)", new Options {ecmaVersion = 6});

            testFail("const foo = 1; var foo = 1;", "Identifier 'foo' has already been declared (1:19)", new Options {ecmaVersion = 6});

            testFail("var [foo] = [1]; let foo = 1;", "Identifier 'foo' has already been declared (1:21)", new Options {ecmaVersion = 6});

            testFail("var [{ bar: [foo] }] = x; let {foo} = 1;", "Identifier 'foo' has already been declared (1:31)", new Options {ecmaVersion = 6});

            testFail("if (x) var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:24)", new Options {ecmaVersion = 6});

            testFail("if (x) {} else var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:32)", new Options {ecmaVersion = 6});

            testFail("if (x) var foo = 1; else {} let foo = 1;", "Identifier 'foo' has already been declared (1:32)", new Options {ecmaVersion = 6});

            testFail("if (x) {} else if (y) {} else var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:47)", new Options {ecmaVersion = 6});

            testFail("while (x) var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:27)", new Options {ecmaVersion = 6});

            testFail("do var foo = 1; while (x) let foo = 1;", "Identifier 'foo' has already been declared (1:30)", new Options {ecmaVersion = 6});

            testFail("for (;;) var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:26)", new Options {ecmaVersion = 6});

            testFail("for (const x of y) var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:36)", new Options {ecmaVersion = 6});

            testFail("for (const x in y) var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:36)", new Options {ecmaVersion = 6});

            testFail("label: var foo = 1; let foo = 1;", "Identifier 'foo' has already been declared (1:24)", new Options {ecmaVersion = 6});

            testFail("switch (x) { case 0: var foo = 1 } let foo = 1;", "Identifier 'foo' has already been declared (1:39)", new Options {ecmaVersion = 6});

            testFail("try { var foo = 1; } catch (e) {} let foo = 1;", "Identifier 'foo' has already been declared (1:38)", new Options {ecmaVersion = 6});

            testFail("function foo() {} let foo = 1;", "Identifier 'foo' has already been declared (1:22)", new Options {ecmaVersion = 6});

            testFail("{ var foo = 1; } let foo = 1;", "Identifier 'foo' has already been declared (1:21)", new Options {ecmaVersion = 6});

            testFail("let foo = 1; { var foo = 1; }", "Identifier 'foo' has already been declared (1:19)", new Options {ecmaVersion = 6});

            testFail("let foo = 1; function x(foo) {} { var foo = 1; }", "Identifier 'foo' has already been declared (1:38)", new Options {ecmaVersion = 6});

            testFail("if (x) { if (y) var foo = 1; } let foo = 1;", "Identifier 'foo' has already been declared (1:35)", new Options {ecmaVersion = 6});

            testFail("var foo = 1; function x() {} let foo = 1;", "Identifier 'foo' has already been declared (1:33)", new Options {ecmaVersion = 6});

            testFail("{ let foo = 1; { let foo = 2; } let foo = 1; }", "Identifier 'foo' has already been declared (1:36)", new Options {ecmaVersion = 6});

            testFail("for (var foo of y) {} let foo = 1;", "Identifier 'foo' has already been declared (1:26)", new Options {ecmaVersion = 6});

            testFail("function x(foo) { let foo = 1; }", "Identifier 'foo' has already been declared (1:22)", new Options {ecmaVersion = 6});

            testFail("var [...foo] = x; let foo = 1;", "Identifier 'foo' has already been declared (1:22)", new Options {ecmaVersion = 6});

            testFail("foo => { let foo; }", "Identifier 'foo' has already been declared (1:13)", new Options {ecmaVersion = 6});

            testFail("({ x(foo) { let foo; } })", "Identifier 'foo' has already been declared (1:16)", new Options {ecmaVersion = 6});

            testFail("try {} catch (foo) { let foo = 1; }", "Identifier 'foo' has already been declared (1:25)", new Options {ecmaVersion = 6});

            Test("var foo = 1; var foo = 1;", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("if (x) var foo = 1; var foo = 1;", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("function x() { var foo = 1; } let foo = 1;", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("function foo() { let foo = 1; }", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("var foo = 1; { let foo = 1; }", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("{ let foo = 1; { let foo = 2; } }", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("var foo; try {} catch (_) { let foo; }", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("let x = 1; function foo(x) {}", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("for (let i = 0;;); for (let i = 0;;);", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("for (const foo of bar); for (const foo of bar);", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("for (const foo in bar); for (const foo in bar);", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("for (let foo in bar) { let foo = 1; }", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("for (let foo of bar) { let foo = 1; }", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("class Foo { method(foo) {} method2() { let foo; } }", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("() => { let foo; }; foo => {}", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("() => { let foo; }; () => { let foo; }", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("switch(x) { case 1: let foo = 1; } let foo = 1;", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("'use strict'; function foo() { let foo = 1; }", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("let foo = 1; function x() { var foo = 1; }", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("[...foo, bar = 1]", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("for (var a of /b/) {}", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("for (var {a} of /b/) {}", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("for (let {a} of /b/) {}", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("function* bar() { yield /re/ }", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("function* bar() { yield class {} }", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("() => {}\n/re/", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("(() => {}) + 2", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            testFail("(x) => {} + 2", "Unexpected token (1:10)", new Options {ecmaVersion = 6});

            Test("function *f1() { function g() { return yield / 1 } }", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("class Foo {} /regexp/", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("(class Foo {} / 2)", new BaseNode(default) { }, new Options {ecmaVersion = 6});

            Test("1 <!--b", new BaseNode(default)
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(default)
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(default)
                        {
                            type = NodeType.BinaryExpression,
                            @operator = "<"
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            testFail("class A extends B { constructor() { super } }", "Unexpected token (1:42)", new Options {ecmaVersion = 6});
            testFail("class A extends B { constructor() { super; } }", "Unexpected token (1:41)", new Options {ecmaVersion = 6});
            testFail("class A extends B { constructor() { (super)() } }", "Unexpected token (1:42)", new Options {ecmaVersion = 6});
            testFail("class A extends B { foo() { (super).foo } }", "Unexpected token (1:34)", new Options {ecmaVersion = 6});
            Test("({super: 1})", new BaseNode(default) { }, new Options {ecmaVersion = 6});
            Test("import {super as a} from 'a'", new BaseNode(default) { }, new Options {ecmaVersion = 6, sourceType = "module"});
            Test("export {a as super}", new BaseNode(default) { }, new Options {ecmaVersion = 6, sourceType = "module"});
            Test("let instanceof Foo", new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
            {
                type = NodeType.Program,
                body = new List<BaseNode>
                {
                    new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new BaseNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 18, 18)))
                        {
                            type = NodeType.BinaryExpression,
                            left = new IdentifierNode(new SourceLocation(new Position(1, 0, 0), new Position(1, 3, 3)), "let"),
                            @operator = "instanceof",
                            right = new IdentifierNode(new SourceLocation(new Position(1, 15, 15), new Position(1, 18, 18)), "Foo")
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});
        }
    }
}
