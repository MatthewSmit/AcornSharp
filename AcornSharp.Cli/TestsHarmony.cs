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

            Test("\"\\u{714E}\\u{8336}\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = "煎茶",
                            raw = "\"\\u{714E}\\u{8336}\"",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("\"\\u{20BB7}\\u{91CE}\\u{5BB6}\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = "𠮷野家",
                            raw = "\"\\u{20BB7}\\u{91CE}\\u{5BB6}\"",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Numeric Literal

            Test("00", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = 0,
                            raw = "00",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 2))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0o0", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = 0,
                            raw = "0o0",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function test() {'use strict'; 0o0; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "test",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 13))
                        },
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = "use strict",
                                        raw = "'use strict'",
                                        loc = new SourceLocation(new Position(1, 17), new Position(1, 29))
                                    },
                                    loc = new SourceLocation(new Position(1, 17), new Position(1, 30))
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 0,
                                        raw = "0o0",
                                        loc = new SourceLocation(new Position(1, 31), new Position(1, 34))
                                    },
                                    loc = new SourceLocation(new Position(1, 31), new Position(1, 35))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 16), new Position(1, 37))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 37))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 37))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0o2", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = 2,
                            raw = "0o2",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0o12", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = 10,
                            raw = "0o12",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0O0", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = 0,
                            raw = "0O0",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function test() {'use strict'; 0O0; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "test",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 13))
                        },
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = "use strict",
                                        raw = "'use strict'",
                                        loc = new SourceLocation(new Position(1, 17), new Position(1, 29))
                                    },
                                    loc = new SourceLocation(new Position(1, 17), new Position(1, 30))
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 0,
                                        raw = "0O0",
                                        loc = new SourceLocation(new Position(1, 31), new Position(1, 34))
                                    },
                                    loc = new SourceLocation(new Position(1, 31), new Position(1, 35))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 16), new Position(1, 37))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 37))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 37))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0O2", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = 2,
                            raw = "0O2",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0O12", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = 10,
                            raw = "0O12",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0b0", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = 0,
                            raw = "0b0",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0b1", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = 1,
                            raw = "0b1",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0b10", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = 2,
                            raw = "0b10",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0B0", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = 0,
                            raw = "0B0",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0B1", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = 1,
                            raw = "0B1",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("0B10", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = 2,
                            raw = "0B10",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6 Template Strings

            Test("`42`", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression =  new Node
                        {
                            type = NodeType.TemplateLiteral,
                            quasis = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("42", "42"),
                                    tail = true,
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 3))
                                }
                            },
                            expressions = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("raw`42`", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.TaggedTemplateExpression,
                            tag = new Node
                            {
                                type = NodeType.Identifier,
                                name = "raw",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                            },
                            quasi = new Node
                            {
                                type = NodeType.TemplateLiteral,
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.TemplateElement,
                                        value = new TemplateNode("42", "42"),
                                        tail = true,
                                        loc = new SourceLocation(new Position(1, 4), new Position(1, 6))
                                    }
                                },
                                expressions = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 3), new Position(1, 7))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 7))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("raw`hello ${name}`", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.TaggedTemplateExpression,
                            tag = new Node
                            {
                                type = NodeType.Identifier,
                                name = "raw",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                            },
                            quasi = new Node
                            {
                                type = NodeType.TemplateLiteral,
                                quasis = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.TemplateElement,
                                        value = new TemplateNode("hello ", "hello "),
                                        tail = false,
                                        loc = new SourceLocation(new Position(1, 4), new Position(1, 10))
                                    },
                                    new Node
                                    {
                                        type = NodeType.TemplateElement,
                                        value = new TemplateNode("", ""),
                                        tail = true,
                                        loc = new SourceLocation(new Position(1, 17), new Position(1, 17))
                                    }
                                },
                                expressions = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "name",
                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 16))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 3), new Position(1, 18))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("`$`", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.TemplateLiteral,
                            quasis = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("$", "$"),
                                    tail = true,
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                                }
                            },
                            expressions = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("`\\n\\r\\b\\v\\t\\f\\\n\\\r\n`", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.TemplateLiteral,
                            quasis = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("\\n\\r\\b\\v\\t\\f\\\n\\\n", "\n\r\b\u000b\t\f"),
                                    tail = true,
                                    loc = new SourceLocation(new Position(1, 1), new Position(3, 0))
                                }
                            },
                            expressions = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 0), new Position(3, 1))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(3, 1))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(3, 1))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("`\n\r\n\r`", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.TemplateLiteral,
                            quasis = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("\n\n\n", "\n\n\n"),
                                    tail = true,
                                    loc = new SourceLocation(new Position(1, 1), new Position(4, 0))
                                }
                            },
                            expressions = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 0), new Position(4, 1))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(4, 1))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(4, 1))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("`\\u{000042}\\u0042\\x42u0\\A`", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.TemplateLiteral,
                            quasis = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("\\u{000042}\\u0042\\x42u0\\A", "BBBu0A"),
                                    tail = true,
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 25))
                                }
                            },
                            expressions = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("new raw`42`", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.NewExpression,
                            callee = new Node
                            {
                                type = NodeType.TaggedTemplateExpression,
                                tag = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "raw",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                                },
                                quasi = new Node
                                {
                                    type = NodeType.TemplateLiteral,
                                    quasis = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode("42", "42"),
                                            tail = true,
                                            loc = new SourceLocation(new Position(1, 8), new Position(1, 10))
                                        }
                                    },
                                    expressions = new List<Node>(),
                                    loc = new SourceLocation(new Position(1, 7), new Position(1, 11))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 11))
                            },
                            arguments = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("`outer${{x: {y: 10}}}bar${`nested${function(){return 1;}}endnest`}end`", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.TemplateLiteral,
                            expressions = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "x"
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.ObjectExpression,
                                                properties = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.Property,
                                                        method = false,
                                                        shorthand = false,
                                                        computed = false,
                                                        key = new Node
                                                        {
                                                            type = NodeType.Identifier,
                                                            name = "y"
                                                        },
                                                        value = new Node
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
                                new Node
                                {
                                    type = NodeType.TemplateLiteral,
                                    expressions = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<Node>(),
                                            generator = false,
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.ReturnStatement,
                                                        argument = new Node
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
                                    quasis = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode("nested", "nested"),
                                            tail = false
                                        },
                                        new Node
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode("endnest", "endnest"),
                                            tail = true
                                        }
                                    }
                                }
                            },
                            quasis = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("outer", "outer"),
                                    tail = false
                                },
                                new Node
                                {
                                    type = NodeType.TemplateElement,
                                    value = new TemplateNode("bar", "bar"),
                                    tail = false
                                },
                                new Node
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

            Test("switch (answer) { case 42: let t = 42; break; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.SwitchStatement,
                        discriminant = new Node
                        {
                            type = NodeType.Identifier,
                            name = "answer",
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 14))
                        },
                        cases = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.SwitchCase,
                                test = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    raw = "42",
                                    loc = new SourceLocation(new Position(1, 23), new Position(1, 25))
                                },
                                sconsequent = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.VariableDeclaration,
                                        declarations = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.VariableDeclarator,
                                                id = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "t",
                                                    loc = new SourceLocation(new Position(1, 31), new Position(1, 32))
                                                },
                                                init = new Node
                                                {
                                                    type = NodeType.Literal,
                                                    value = 42,
                                                    raw = "42",
                                                    loc = new SourceLocation(new Position(1, 35), new Position(1, 37))
                                                },
                                                loc = new SourceLocation(new Position(1, 31), new Position(1, 37))
                                            }
                                        },
                                        kind = "let",
                                        loc = new SourceLocation(new Position(1, 27), new Position(1, 38))
                                    },
                                    new Node
                                    {
                                        type = NodeType.BreakStatement,
                                        label = null,
                                        loc = new SourceLocation(new Position(1, 39), new Position(1, 45))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 18), new Position(1, 45))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 47))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 47))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Arrow Function

            Test("() => \"test\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = NodeType.Literal,
                                value = "test",
                                raw = "\"test\"",
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 12))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("e => \"test\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "e",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.Literal,
                                value = "test",
                                raw = "\"test\"",
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 11))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(e) => \"test\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "e",
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.Literal,
                                value = "test",
                                raw = "\"test\"",
                                loc = new SourceLocation(new Position(1, 7), new Position(1, 13))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(a, b) => \"test\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "a",
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                                },
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "b",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.Literal,
                                value = "test",
                                raw = "\"test\"",
                                loc = new SourceLocation(new Position(1, 10), new Position(1, 16))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("e => { 42; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "e",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.Literal,
                                            value = 42,
                                            raw = "42",
                                            loc = new SourceLocation(new Position(1, 7), new Position(1, 9))
                                        },
                                        loc = new SourceLocation(new Position(1, 7), new Position(1, 10))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 12))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("e => ({ property: 42 })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "e",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "property",
                                            loc = new SourceLocation(new Position(1, 8), new Position(1, 16))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.Literal,
                                            value = 42,
                                            raw = "42",
                                            loc = new SourceLocation(new Position(1, 18), new Position(1, 20))
                                        },
                                        kind = "init",
                                        method = false,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 8), new Position(1, 20))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 22))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("e => { label: 42 }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "e",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.LabeledStatement,
                                        label = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "label",
                                            loc = new SourceLocation(new Position(1, 7), new Position(1, 12))
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.ExpressionStatement,
                                            expression = new Node
                                            {
                                                type = NodeType.Literal,
                                                value = 42,
                                                raw = "42",
                                                loc = new SourceLocation(new Position(1, 14), new Position(1, 16))
                                            },
                                            loc = new SourceLocation(new Position(1, 14), new Position(1, 16))
                                        },
                                        loc = new SourceLocation(new Position(1, 7), new Position(1, 16))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 18))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(a, b) => { 42; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "a",
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                                },
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "b",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.Literal,
                                            value = 42,
                                            raw = "42",
                                            loc = new SourceLocation(new Position(1, 12), new Position(1, 14))
                                        },
                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 15))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 10), new Position(1, 17))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("([a, , b]) => 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "a",
                                            loc = new SourceLocation(new Position(1, 2), new Position(1, 3))
                                        },
                                        null,
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "b",
                                            loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 9))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.Literal,
                                value = 42,
                                raw = "42",
                                loc = new SourceLocation(new Position(1, 14), new Position(1, 16))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }, new Options
            {
                ecmaVersion = 6
            });

            testFail("([a.a]) => 42", "Assigning to rvalue (1:2)", new Options {ecmaVersion = 6});
            testFail("() => {}()", "Unexpected token (1:8)", new Options {ecmaVersion = 6});
            testFail("(a) => {}()", "Unexpected token (1:9)", new Options {ecmaVersion = 6});
            testFail("a => {}()", "Unexpected token (1:7)", new Options {ecmaVersion = 6});
            testFail("console.log(typeof () => {});", "Unexpected token (1:20)", new Options {ecmaVersion = 6});

            Test("(() => {})()", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            start = 0,
                            end = 12,
                            callee = new Node
                            {
                                type = NodeType.ArrowFunctionExpression,
                                id = null,
                                @params = new List<Node>(),
                                fbody = new Node
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<Node>(),
                                    start = 7,
                                    end = 9,
                                    loc = new SourceLocation(new Position(1, 7), new Position(1, 9))
                                },
                                generator = false,
                                bexpression = false,
                                loc = new SourceLocation(new Position(1, 1), new Position(1, 9))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("((() => {}))()", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            start = 0,
                            end = 14,
                            callee = new Node
                            {
                                type = NodeType.ArrowFunctionExpression,
                                id = null,
                                @params = new List<Node>(),
                                fbody = new Node
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<Node>(),
                                    start = 8,
                                    end = 10,
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 10))
                                },
                                generator = false,
                                bexpression = false,
                                loc = new SourceLocation(new Position(1, 2), new Position(1, 10))
                            }
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }, new Options
            {
                ecmaVersion = 6
            });


            Test("(x=1) => x * x", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.AssignmentPattern,
                                    left = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                                    },
                                    right = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 1,
                                        raw = "1",
                                        loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                    },
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 4))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BinaryExpression,
                                @operator = "*",
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                },
                                right = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                },
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 14))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("eval => 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "eval",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.Literal,
                                value = 42,
                                raw = "42",
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 10))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("arguments => 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "arguments",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.Literal,
                                value = 42,
                                raw = "42",
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 15))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(a) => 00", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "a",
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.Literal,
                                value = 0,
                                raw = "00",
                                loc = new SourceLocation(new Position(1, 7), new Position(1, 9))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(eval, a) => 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "eval",
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 5))
                                },
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "a",
                                    loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.Literal,
                                value = 42,
                                raw = "42",
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 15))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(eval = 10) => 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.AssignmentPattern,
                                    left = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "eval",
                                        loc = new SourceLocation(new Position(1, 1), new Position(1, 5))
                                    },
                                    right = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 10,
                                        raw = "10",
                                        loc = new SourceLocation(new Position(1, 8), new Position(1, 10))
                                    },
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 10))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.Literal,
                                value = 42,
                                raw = "42",
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 17))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(eval, a = 10) => 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "eval",
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 5))
                                },
                                new Node
                                {
                                    type = NodeType.AssignmentPattern,
                                    left = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "a",
                                        loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                    },
                                    right = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 10,
                                        raw = "10",
                                        loc = new SourceLocation(new Position(1, 11), new Position(1, 13))
                                    },
                                    loc = new SourceLocation(new Position(1, 7), new Position(1, 13))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.Literal,
                                value = 42,
                                raw = "42",
                                loc = new SourceLocation(new Position(1, 18), new Position(1, 20))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(x => x)", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 7))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x => y => 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.ArrowFunctionExpression,
                                id = null,
                                @params = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "y",
                                        loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                    }
                                },
                                fbody = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 42,
                                    raw = "42",
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                                },
                                generator = false,
                                bexpression = true,
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 12))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(x) => ((y, z) => (x, y, z))", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.ArrowFunctionExpression,
                                id = null,
                                @params = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "y",
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                    },
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "z",
                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                    }
                                },
                                fbody = new Node
                                {
                                    type = NodeType.SequenceExpression,
                                    expressions = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 19), new Position(1, 20))
                                        },
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "y",
                                            loc = new SourceLocation(new Position(1, 22), new Position(1, 23))
                                        },
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "z",
                                            loc = new SourceLocation(new Position(1, 25), new Position(1, 26))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 19), new Position(1, 26))
                                },
                                generator = false,
                                bexpression = true,
                                loc = new SourceLocation(new Position(1, 8), new Position(1, 27))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("foo(() => {})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            callee = new Node
                            {
                                type = NodeType.Identifier,
                                name = "foo",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                            },
                            arguments = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ArrowFunctionExpression,
                                    id = null,
                                    @params = new List<Node>(),
                                    fbody = new Node
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<Node>(),
                                        loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                                    },
                                    generator = false,
                                    bexpression = false,
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 12))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("foo((x, y) => {})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            callee = new Node
                            {
                                type = NodeType.Identifier,
                                name = "foo",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 3))
                            },
                            arguments = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ArrowFunctionExpression,
                                    id = null,
                                    @params = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                        },
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "y",
                                            loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                        }
                                    },
                                    fbody = new Node
                                    {
                                        type = NodeType.BlockStatement,
                                        body = new List<Node>(),
                                        loc = new SourceLocation(new Position(1, 14), new Position(1, 16))
                                    },
                                    generator = false,
                                    bexpression = false,
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 16))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Method Definition

            Test("x = { method() { } }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "method",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 12))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<Node>(),
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 15), new Position(1, 18))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 12), new Position(1, 18))
                                        },
                                        kind = "init",
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 18))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 20))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x = { method(test) { } }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "method",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 12))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "test",
                                                    loc = new SourceLocation(new Position(1, 13), new Position(1, 17))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 19), new Position(1, 22))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 12), new Position(1, 22))
                                        },
                                        kind = "init",
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 22))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 24))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x = { 'method'() { } }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Literal,
                                            value = "method",
                                            raw = "'method'",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 14))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<Node>(),
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 17), new Position(1, 20))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 14), new Position(1, 20))
                                        },
                                        kind = "init",
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 20))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 22))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x = { get() { } }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "get",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 9))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<Node>(),
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 12), new Position(1, 15))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 9), new Position(1, 15))
                                        },
                                        kind = "init",
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 15))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 17))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x = { set() { } }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "set",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 9))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<Node>(),
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 12), new Position(1, 15))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 9), new Position(1, 15))
                                        },
                                        kind = "init",
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 15))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 17))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            // Harmony: Object Literal Property Value Shorthand

            Test("x = { y, z }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "y",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "y",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                        },
                                        kind = "init",
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                    },
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "z",
                                            loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "z",
                                            loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                        },
                                        kind = "init",
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 12))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            // Harmony: Destructuring

            Test("[a, b] = [b, a]", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.ArrayPattern,
                                elements = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "a",
                                        loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                                    },
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "b",
                                        loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                            },
                            right = new Node
                            {
                                type = NodeType.ArrayExpression,
                                elements = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "b",
                                        loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                                    },
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "a",
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 15))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({ responseText: text } = res)", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.ObjectPattern,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "responseText",
                                            loc = new SourceLocation(new Position(1, 3), new Position(1, 15))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "text",
                                            loc = new SourceLocation(new Position(1, 17), new Position(1, 21))
                                        },
                                        kind = "init",
                                        method = false,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 3), new Position(1, 21))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 1), new Position(1, 23))
                            },
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "res",
                                loc = new SourceLocation(new Position(1, 26), new Position(1, 29))
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 29))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("const {a} = {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 6), new Position(1, 9))
                                },
                                init = new Node
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<Node>(),
                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 14))
                                },
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 14))
                            }
                        },
                        kind = "const",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("const [a] = []", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "a",
                                            loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 6), new Position(1, 9))
                                },
                                init = new Node
                                {
                                    type = NodeType.ArrayExpression,
                                    elements = new List<Node> { },
                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 14))
                                },
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 14))
                            }
                        },
                        kind = "const",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("let {a} = {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                                },
                                init = new Node
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<Node>(),
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 12))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("let [a] = []", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "a",
                                            loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                                },
                                init = new Node
                                {
                                    type = NodeType.ArrayExpression,
                                    elements = new List<Node> { },
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 12))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var {a} = {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                                },
                                init = new Node
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<Node>(),
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 12))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var [a] = []", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "a",
                                            loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                                },
                                init = new Node
                                {
                                    type = NodeType.ArrayExpression,
                                    elements = new List<Node> { },
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 12))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("const {a:b} = {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "b",
                                                loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 7), new Position(1, 10))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 6), new Position(1, 11))
                                },
                                init = new Node
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<Node>(),
                                    loc = new SourceLocation(new Position(1, 14), new Position(1, 16))
                                },
                                loc = new SourceLocation(new Position(1, 6), new Position(1, 16))
                            }
                        },
                        kind = "const",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("let {a:b} = {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "b",
                                                loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 5), new Position(1, 8))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 9))
                                },
                                init = new Node
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<Node>(),
                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 14))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 14))
                            }
                        },
                        kind = "let",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var {a:b} = {}", new Node
            {
                type = NodeType.Program,
                sourceType = "script",
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "b",
                                                loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 5), new Position(1, 8))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 9))
                                },
                                init = new Node
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<Node>(),
                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 14))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 14))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            // Harmony: Modules

            Test("export var document", new Node
            {
                type = NodeType.Program,
                sourceType = "module",
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "document",
                                        loc = new SourceLocation(new Position(1, 11), new Position(1, 19))
                                    },
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 19))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 19))
                        },
                        specifiers = new List<Node>(),
                        source = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export var document = { }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "document",
                                        loc = new SourceLocation(new Position(1, 11), new Position(1, 19))
                                    },
                                    init = new Node
                                    {
                                        type = NodeType.ObjectExpression,
                                        properties = new List<Node>(),
                                        loc = new SourceLocation(new Position(1, 22), new Position(1, 25))
                                    },
                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 25))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 25))
                        },
                        specifiers = new List<Node>(),
                        source = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 25))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 25))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            testFail("export var await", "The keyword 'await' is reserved (1:11)", new Options {ecmaVersion = 6, sourceType = "module"});

            Test("export let document", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "document",
                                        loc = new SourceLocation(new Position(1, 11), new Position(1, 19))
                                    },
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 19))
                                }
                            },
                            kind = "let",
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 19))
                        },
                        specifiers = new List<Node>(),
                        source = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export let document = { }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "document",
                                        loc = new SourceLocation(new Position(1, 11), new Position(1, 19))
                                    },
                                    init = new Node
                                    {
                                        type = NodeType.ObjectExpression,
                                        properties = new List<Node>(),
                                        loc = new SourceLocation(new Position(1, 22), new Position(1, 25))
                                    },
                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 25))
                                }
                            },
                            kind = "let",
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 25))
                        },
                        specifiers = new List<Node>(),
                        source = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 25))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 25))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export const document = { }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "document",
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 21))
                                    },
                                    init = new Node
                                    {
                                        type = NodeType.ObjectExpression,
                                        properties = new List<Node>(),
                                        loc = new SourceLocation(new Position(1, 24), new Position(1, 27))
                                    },
                                    loc = new SourceLocation(new Position(1, 13), new Position(1, 27))
                                }
                            },
                            kind = "const",
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 27))
                        },
                        specifiers = new List<Node>(),
                        source = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export function parse() { }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new Node
                        {
                            type = NodeType.FunctionDeclaration,
                            id = new Node
                            {
                                type = NodeType.Identifier,
                                name = "parse",
                                loc = new SourceLocation(new Position(1, 16), new Position(1, 21))
                            },
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 24), new Position(1, 27))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 27))
                        },
                        specifiers = new List<Node>(),
                        source = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export class Class {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = new Node
                        {
                            type = NodeType.ClassDeclaration,
                            id = new Node
                            {
                                type = NodeType.Identifier,
                                name = "Class",
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 18))
                            },
                            superClass = null,
                            fbody = new Node
                            {
                                type = NodeType.ClassBody,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 19), new Position(1, 21))
                            },
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 21))
                        },
                        specifiers = new List<Node>(),
                        source = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            testFail("export new Foo();", "Unexpected token (1:7)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("export typeof foo;", "Unexpected token (1:7)", new Options {ecmaVersion = 6, sourceType = "module"});

            Test("export default 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        declaration = new Node
                        {
                            type = NodeType.Literal,
                            value = 42,
                            raw = "42",
                            loc = new SourceLocation(new Position(1, 15), new Position(1, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export default function () {}", new Node
            {
                type = NodeType.Program,
                range = (0, 29),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        range = (0, 29),
                        declaration = new Node
                        {
                            type = NodeType.FunctionDeclaration,
                            range = (15, 29),
                            id = null,
                            generator = false,
                            bexpression = false,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                range = (27, 29),
                                body = new List<Node>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            Test("export default function f() {}", new Node
            {
                type = NodeType.Program,
                range = (0, 30),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        range = (0, 30),
                        declaration = new Node
                        {
                            type = NodeType.FunctionDeclaration,
                            range = (15, 30),
                            id = new Node
                            {
                                type = NodeType.Identifier,
                                range = (24, 25),
                                name = "f"
                            },
                            generator = false,
                            bexpression = false,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                range = (28, 30),
                                body = new List<Node>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            Test("export default class {}", new Node
            {
                type = NodeType.Program,
                range = (0, 23),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        range = (0, 23),
                        declaration = new Node
                        {
                            type = NodeType.ClassDeclaration,
                            range = (15, 23),
                            id = null,
                            superClass = null,
                            fbody = new Node
                            {
                                type = NodeType.ClassBody,
                                range = (21, 23),
                                body = new List<Node>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            Test("export default class A {}", new Node
            {
                type = NodeType.Program,
                range = (0, 25),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        range = (0, 25),
                        declaration = new Node
                        {
                            type = NodeType.ClassDeclaration,
                            range = (15, 25),
                            id = new Node
                            {
                                type = NodeType.Identifier,
                                range = (21, 22),
                                name = "A"
                            },
                            superClass = null,
                            fbody = new Node
                            {
                                type = NodeType.ClassBody,
                                range = (23, 25),
                                body = new List<Node>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            Test("export default (class{});", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        declaration = new Node
                        {
                            type = NodeType.ClassExpression,
                            id = null,
                            superClass = null,
                            fbody = new Node
                            {
                                type = NodeType.ClassBody,
                                body = new List<Node>()
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            testFail("export *", "Unexpected token (1:8)", new Options {ecmaVersion = 6, sourceType = "module"});

            Test("export * from \"crypto\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportAllDeclaration,
                        source = new Node
                        {
                            type = NodeType.Literal,
                            value = "crypto",
                            raw = "\"crypto\"",
                            loc = new SourceLocation(new Position(1, 14), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export { encrypt }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = null,
                        specifiers = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ExportSpecifier,
                                exported = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "encrypt",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                                },
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "encrypt",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                                },
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                            }
                        },
                        source = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export { encrypt, decrypt }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = null,
                        specifiers = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ExportSpecifier,
                                exported = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "encrypt",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                                },
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "encrypt",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                                },
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                            },
                            new Node
                            {
                                type = NodeType.ExportSpecifier,
                                exported = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "decrypt",
                                    loc = new SourceLocation(new Position(1, 18), new Position(1, 25))
                                },
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "decrypt",
                                    loc = new SourceLocation(new Position(1, 18), new Position(1, 25))
                                },
                                loc = new SourceLocation(new Position(1, 18), new Position(1, 25))
                            }
                        },
                        source = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export { encrypt as default }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = null,
                        specifiers = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ExportSpecifier,
                                exported = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "default",
                                    loc = new SourceLocation(new Position(1, 20), new Position(1, 27))
                                },
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "encrypt",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                                },
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 27))
                            }
                        },
                        source = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export { encrypt, decrypt as dec }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = null,
                        specifiers = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ExportSpecifier,
                                exported = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "encrypt",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                                },
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "encrypt",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                                },
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                            },
                            new Node
                            {
                                type = NodeType.ExportSpecifier,
                                exported = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "dec",
                                    loc = new SourceLocation(new Position(1, 29), new Position(1, 32))
                                },
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "decrypt",
                                    loc = new SourceLocation(new Position(1, 18), new Position(1, 25))
                                },
                                loc = new SourceLocation(new Position(1, 18), new Position(1, 32))
                            }
                        },
                        source = null,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("export { default } from \"other\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportNamedDeclaration,
                        declaration = null,
                        specifiers = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ExportSpecifier,
                                exported = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "default",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                                },
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "default",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                                },
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                            }
                        },
                        source = new Node
                        {
                            type = NodeType.Literal,
                            loc = new SourceLocation(new Position(1, 24), new Position(1, 31)),
                            value = "other",
                            raw = "\"other\""
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            testFail("export { default }", "Unexpected keyword 'default' (1:9)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("export { if }", "Unexpected keyword 'if' (1:9)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("export { default as foo }", "Unexpected keyword 'default' (1:9)", new Options {ecmaVersion = 6, sourceType = "module"});
            testFail("export { if as foo }", "Unexpected keyword 'if' (1:9)", new Options {ecmaVersion = 6, sourceType = "module"});

            Test("import \"jquery\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ImportDeclaration,
                        specifiers = new List<Node>(),
                        source = new Node
                        {
                            type = NodeType.Literal,
                            value = "jquery",
                            raw = "\"jquery\"",
                            loc = new SourceLocation(new Position(1, 7), new Position(1, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("import $ from \"jquery\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ImportDeclaration,
                        specifiers = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ImportDefaultSpecifier,
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "$",
                                    loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                },
                                loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                            }
                        },
                        source = new Node
                        {
                            type = NodeType.Literal,
                            value = "jquery",
                            raw = "\"jquery\"",
                            loc = new SourceLocation(new Position(1, 14), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("import { encrypt, decrypt } from \"crypto\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ImportDeclaration,
                        specifiers = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ImportSpecifier,
                                imported = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "encrypt",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                                },
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "encrypt",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                                },
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                            },
                            new Node
                            {
                                type = NodeType.ImportSpecifier,
                                imported = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "decrypt",
                                    loc = new SourceLocation(new Position(1, 18), new Position(1, 25))
                                },
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "decrypt",
                                    loc = new SourceLocation(new Position(1, 18), new Position(1, 25))
                                },
                                loc = new SourceLocation(new Position(1, 18), new Position(1, 25))
                            }
                        },
                        source = new Node
                        {
                            type = NodeType.Literal,
                            value = "crypto",
                            raw = "\"crypto\"",
                            loc = new SourceLocation(new Position(1, 33), new Position(1, 41))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 41))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 41))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("import { encrypt as enc } from \"crypto\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ImportDeclaration,
                        specifiers = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ImportSpecifier,
                                imported = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "encrypt",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 16))
                                },
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "enc",
                                    loc = new SourceLocation(new Position(1, 20), new Position(1, 23))
                                },
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 23))
                            }
                        },
                        source = new Node
                        {
                            type = NodeType.Literal,
                            value = "crypto",
                            raw = "\"crypto\"",
                            loc = new SourceLocation(new Position(1, 31), new Position(1, 39))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 39))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 39))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("import crypto, { decrypt, encrypt as enc } from \"crypto\"", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0), new Position(1, 56)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ImportDeclaration,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 56)),
                        specifiers = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ImportDefaultSpecifier,
                                loc = new SourceLocation(new Position(1, 7), new Position(1, 13)),
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    loc = new SourceLocation(new Position(1, 7), new Position(1, 13)),
                                    name = "crypto"
                                }
                            },
                            new Node
                            {
                                type = NodeType.ImportSpecifier,
                                loc = new SourceLocation(new Position(1, 17), new Position(1, 24)),
                                imported = new Node
                                {
                                    type = NodeType.Identifier,
                                    loc = new SourceLocation(new Position(1, 17), new Position(1, 24)),
                                    name = "decrypt"
                                },
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    loc = new SourceLocation(new Position(1, 17), new Position(1, 24)),
                                    name = "decrypt"
                                }
                            },
                            new Node
                            {
                                type = NodeType.ImportSpecifier,
                                loc = new SourceLocation(new Position(1, 26), new Position(1, 40)),
                                imported = new Node
                                {
                                    type = NodeType.Identifier,
                                    loc = new SourceLocation(new Position(1, 26), new Position(1, 33)),
                                    name = "encrypt"
                                },
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    loc = new SourceLocation(new Position(1, 37), new Position(1, 40)),
                                    name = "enc"
                                }
                            }
                        },
                        source = new Node
                        {
                            type = NodeType.Literal,
                            loc = new SourceLocation(new Position(1, 48), new Position(1, 56)),
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

            Test("import { null as nil } from \"bar\"", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ImportDeclaration,
                        specifiers = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ImportSpecifier,
                                imported = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "null",
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 13))
                                },
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "nil",
                                    loc = new SourceLocation(new Position(1, 17), new Position(1, 20))
                                },
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 20))
                            }
                        },
                        source = new Node
                        {
                            type = NodeType.Literal,
                            value = "bar",
                            raw = "\"bar\"",
                            loc = new SourceLocation(new Position(1, 28), new Position(1, 33))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("import * as crypto from \"crypto\"", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0), new Position(1, 32)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ImportDeclaration,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 32)),
                        specifiers = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ImportNamespaceSpecifier,
                                loc = new SourceLocation(new Position(1, 7), new Position(1, 18)),
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 18)),
                                    name = "crypto"
                                }
                            }
                        },
                        source = new Node
                        {
                            type = NodeType.Literal,
                            loc = new SourceLocation(new Position(1, 24), new Position(1, 32)),
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

            Test("(function* () { yield v })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.YieldExpression,
                                            argument = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "v",
                                                loc = new SourceLocation(new Position(1, 22), new Position(1, 23))
                                            },
                                            @delegate = false,
                                            loc = new SourceLocation(new Position(1, 16), new Position(1, 23))
                                        },
                                        loc = new SourceLocation(new Position(1, 16), new Position(1, 23))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 14), new Position(1, 25))
                            },
                            generator = true,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 25))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("(function* () { yield\nv })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.YieldExpression,
                                            argument = null,
                                            @delegate = false,
                                            loc = new SourceLocation(new Position(1, 16), new Position(1, 21))
                                        },
                                        loc = new SourceLocation(new Position(1, 16), new Position(1, 21))
                                    },
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "v",
                                            loc = new SourceLocation(new Position(2, 0), new Position(2, 1))
                                        },
                                        loc = new SourceLocation(new Position(2, 0), new Position(2, 1))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 14), new Position(2, 3))
                            },
                            generator = true,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1), new Position(2, 3))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(2, 4))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(2, 4))
            }, new Options
            {
                ecmaVersion = 6,
                sourceType = "module"
            });

            Test("(function* () { yield *v })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.YieldExpression,
                                            argument = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "v",
                                                loc = new SourceLocation(new Position(1, 23), new Position(1, 24))
                                            },
                                            @delegate = true,
                                            loc = new SourceLocation(new Position(1, 16), new Position(1, 24))
                                        },
                                        loc = new SourceLocation(new Position(1, 16), new Position(1, 24))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 14), new Position(1, 26))
                            },
                            generator = true,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function* test () { yield *v }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "test",
                            loc = new SourceLocation(new Position(1, 10), new Position(1, 14))
                        },
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.YieldExpression,
                                        argument = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "v",
                                            loc = new SourceLocation(new Position(1, 27), new Position(1, 28))
                                        },
                                        @delegate = true,
                                        loc = new SourceLocation(new Position(1, 20), new Position(1, 28))
                                    },
                                    loc = new SourceLocation(new Position(1, 20), new Position(1, 28))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 18), new Position(1, 30))
                        },
                        generator = true,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var x = { *test () { yield *v } };", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = new Node
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "test",
                                                loc = new SourceLocation(new Position(1, 11), new Position(1, 15))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.FunctionExpression,
                                                id = null,
                                                @params = new List<Node>(),
                                                fbody = new Node
                                                {
                                                    type = NodeType.BlockStatement,
                                                    body = new List<Node>
                                                    {
                                                        new Node
                                                        {
                                                            type = NodeType.ExpressionStatement,
                                                            expression = new Node
                                                            {
                                                                type = NodeType.YieldExpression,
                                                                argument = new Node
                                                                {
                                                                    type = NodeType.Identifier,
                                                                    name = "v",
                                                                    loc = new SourceLocation(new Position(1, 28), new Position(1, 29))
                                                                },
                                                                @delegate = true,
                                                                loc = new SourceLocation(new Position(1, 21), new Position(1, 29))
                                                            },
                                                            loc = new SourceLocation(new Position(1, 21), new Position(1, 29))
                                                        }
                                                    },
                                                    loc = new SourceLocation(new Position(1, 19), new Position(1, 31))
                                                },
                                                generator = true,
                                                bexpression = false,
                                                loc = new SourceLocation(new Position(1, 16), new Position(1, 31))
                                            },
                                            kind = "init",
                                            method = true,
                                            shorthand = false,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 31))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 33))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 33))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function* foo() { console.log(yield); }", new Node
            {
                body = new List<Node>
                {
                    new Node
                    {
                        id = new Node
                        {
                            name = "foo",
                            type = NodeType.Identifier,
                        },
                        generator = true,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            body = new List<Node>
                            {
                                new Node
                                {
                                    expression = new Node
                                    {
                                        callee = new Node
                                        {
                                            @object = new Node
                                            {
                                                name = "console",
                                                type = NodeType.Identifier,
                                            },
                                            property = new Node
                                            {
                                                name = "log",
                                                type = NodeType.Identifier,
                                            },
                                            computed = false,
                                            type = NodeType.MemberExpression,
                                        },
                                        arguments = new List<Node>
                                        {
                                            new Node
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

            Test("function* t() {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "t",
                            loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                        },
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 14), new Position(1, 16))
                        },
                        generator = true,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(function* () { yield yield 10 })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.YieldExpression,
                                            argument = new Node
                                            {
                                                type = NodeType.YieldExpression,
                                                argument = new Node
                                                {
                                                    type = NodeType.Literal,
                                                    value = 10,
                                                    raw = "10",
                                                    loc = new SourceLocation(new Position(1, 28), new Position(1, 30))
                                                },
                                                @delegate = false,
                                                loc = new SourceLocation(new Position(1, 22), new Position(1, 30))
                                            },
                                            @delegate = false,
                                            loc = new SourceLocation(new Position(1, 16), new Position(1, 30))
                                        },
                                        loc = new SourceLocation(new Position(1, 16), new Position(1, 30))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 14), new Position(1, 32))
                            },
                            generator = true,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 32))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
            }, new Options
            {
                ecmaVersion = 6
            });

            testFail("function *g() { (x = yield) => {} }", "Yield expression cannot be a default value (1:21)", new Options {ecmaVersion = 6});
            testFail("function *g() { ({x = yield}) => {} }", "Yield expression cannot be a default value (1:22)", new Options {ecmaVersion = 6});

            // Harmony: Iterators

            Test("for(x of list) process(x);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForOfStatement,
                        left = new Node
                        {
                            type = NodeType.Identifier,
                            name = "x",
                            loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                        },
                        right = new Node
                        {
                            type = NodeType.Identifier,
                            name = "list",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 13))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 22))
                                },
                                arguments = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 23), new Position(1, 24))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 15), new Position(1, 25))
                            },
                            loc = new SourceLocation(new Position(1, 15), new Position(1, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("for (var x of list) process(x);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForOfStatement,
                        left = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                    },
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 5), new Position(1, 10))
                        },
                        right = new Node
                        {
                            type = NodeType.Identifier,
                            name = "list",
                            loc = new SourceLocation(new Position(1, 14), new Position(1, 18))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 20), new Position(1, 27))
                                },
                                arguments = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 28), new Position(1, 29))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 20), new Position(1, 30))
                            },
                            loc = new SourceLocation(new Position(1, 20), new Position(1, 31))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("for (var x = 42 of list) process(x);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForOfStatement,
                        left = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                    },
                                    init = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 42,
                                        raw = "42",
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 15))
                                    },
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 15))
                                }
                            },
                            kind = "var",
                            loc = new SourceLocation(new Position(1, 5), new Position(1, 15))
                        },
                        right = new Node
                        {
                            type = NodeType.Identifier,
                            name = "list",
                            loc = new SourceLocation(new Position(1, 19), new Position(1, 23))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 25), new Position(1, 32))
                                },
                                arguments = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 33), new Position(1, 34))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 25), new Position(1, 35))
                            },
                            loc = new SourceLocation(new Position(1, 25), new Position(1, 36))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 36))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("for (let x of list) process(x);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForOfStatement,
                        left = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                    },
                                    init = null,
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                }
                            },
                            kind = "let",
                            loc = new SourceLocation(new Position(1, 5), new Position(1, 10))
                        },
                        right = new Node
                        {
                            type = NodeType.Identifier,
                            name = "list",
                            loc = new SourceLocation(new Position(1, 14), new Position(1, 18))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "process",
                                    loc = new SourceLocation(new Position(1, 20), new Position(1, 27))
                                },
                                arguments = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 28), new Position(1, 29))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 20), new Position(1, 30))
                            },
                            loc = new SourceLocation(new Position(1, 20), new Position(1, 31))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
            }, new Options
            {
                ecmaVersion = 6
            });

            // Harmony: Class (strawman)

            Test("var A = class extends B {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "A",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = new Node
                                {
                                    type = NodeType.ClassExpression,
                                    superClass = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "B",
                                        loc = new SourceLocation(new Position(1, 22), new Position(1, 23))
                                    },
                                    fbody = new Node
                                    {
                                        type = NodeType.ClassBody,
                                        body = new List<Node>(),
                                        loc = new SourceLocation(new Position(1, 24), new Position(1, 26))
                                    },
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 26))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 26))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A extends class B extends C {} {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = new Node
                        {
                            type = NodeType.ClassExpression,
                            id = new Node
                            {
                                type = NodeType.Identifier,
                                name = "B",
                                loc = new SourceLocation(new Position(1, 22), new Position(1, 23))
                            },
                            superClass = new Node
                            {
                                type = NodeType.Identifier,
                                name = "C",
                                loc = new SourceLocation(new Position(1, 32), new Position(1, 33))
                            },
                            fbody = new Node
                            {
                                type = NodeType.ClassBody,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 34), new Position(1, 36))
                            },
                            loc = new SourceLocation(new Position(1, 16), new Position(1, 36))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 37), new Position(1, 39))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 39))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 39))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A {get() {}}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "get",
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 12))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 15), new Position(1, 17))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 17))
                                    },
                                    kind = "method",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 17))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { static get() {}}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "get",
                                        loc = new SourceLocation(new Position(1, 17), new Position(1, 20))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 23), new Position(1, 25))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 20), new Position(1, 25))
                                    },
                                    kind = "method",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 25))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 26))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A extends B {get foo() {}}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = new Node
                        {
                            type = NodeType.Identifier,
                            name = "B",
                            loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 23), new Position(1, 26))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 29), new Position(1, 31))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 26), new Position(1, 31))
                                    },
                                    kind = "get",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 19), new Position(1, 31))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 18), new Position(1, 32))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 32))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 32))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A extends B { static get foo() {}}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = new Node
                        {
                            type = NodeType.Identifier,
                            name = "B",
                            loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                        },
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 31), new Position(1, 34))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 37), new Position(1, 39))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 34), new Position(1, 39))
                                    },
                                    kind = "get",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 20), new Position(1, 39))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 18), new Position(1, 40))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 40))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 40))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A {set a(v) {}}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "a",
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "v",
                                                loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 18), new Position(1, 20))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 14), new Position(1, 20))
                                    },
                                    kind = "set",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 20))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 21))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { static set a(v) {}}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "a",
                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 22))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "v",
                                                loc = new SourceLocation(new Position(1, 23), new Position(1, 24))
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 26), new Position(1, 28))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 22), new Position(1, 28))
                                    },
                                    kind = "set",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 28))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 29))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A {set(v) {};}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "set",
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 12))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "v",
                                                loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 16), new Position(1, 18))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 18))
                                    },
                                    kind = "method",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 18))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { static set(v) {};}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "set",
                                        loc = new SourceLocation(new Position(1, 17), new Position(1, 20))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "v",
                                                loc = new SourceLocation(new Position(1, 21), new Position(1, 22))
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 24), new Position(1, 26))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 20), new Position(1, 26))
                                    },
                                    kind = "method",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 26))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 28))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A {*gen(v) { yield v; }}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "gen",
                                        loc = new SourceLocation(new Position(1, 10), new Position(1, 13))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "v",
                                                loc = new SourceLocation(new Position(1, 14), new Position(1, 15))
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = NodeType.ExpressionStatement,
                                                    expression = new Node
                                                    {
                                                        type = NodeType.YieldExpression,
                                                        argument = new Node
                                                        {
                                                            type = NodeType.Identifier,
                                                            name = "v",
                                                            loc = new SourceLocation(new Position(1, 25), new Position(1, 26))
                                                        },
                                                        @delegate = false,
                                                        loc = new SourceLocation(new Position(1, 19), new Position(1, 26))
                                                    },
                                                    loc = new SourceLocation(new Position(1, 19), new Position(1, 27))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 17), new Position(1, 29))
                                        },
                                        generator = true,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 29))
                                    },
                                    kind = "method",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 29))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 30))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { static *gen(v) { yield v; }}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "gen",
                                        loc = new SourceLocation(new Position(1, 18), new Position(1, 21))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "v",
                                                loc = new SourceLocation(new Position(1, 22), new Position(1, 23))
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = NodeType.ExpressionStatement,
                                                    expression = new Node
                                                    {
                                                        type = NodeType.YieldExpression,
                                                        argument = new Node
                                                        {
                                                            type = NodeType.Identifier,
                                                            name = "v",
                                                            loc = new SourceLocation(new Position(1, 33), new Position(1, 34))
                                                        },
                                                        @delegate = false,
                                                        loc = new SourceLocation(new Position(1, 27), new Position(1, 34))
                                                    },
                                                    loc = new SourceLocation(new Position(1, 27), new Position(1, 35))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 25), new Position(1, 37))
                                        },
                                        generator = true,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 37))
                                    },
                                    kind = "method",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 37))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 38))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("\"use strict\"; (class A {constructor() { super() }})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = "use strict",
                            raw = "\"use strict\"",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ClassExpression,
                            id = new Node
                            {
                                type = NodeType.Identifier,
                                name = "A",
                                loc = new SourceLocation(new Position(1, 21), new Position(1, 22))
                            },
                            superClass = null,
                            fbody = new Node
                            {
                                type = NodeType.ClassBody,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.MethodDefinition,
                                        computed = false,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "constructor",
                                            loc = new SourceLocation(new Position(1, 24), new Position(1, 35))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<Node>(),
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new Node
                                                        {
                                                            type = NodeType.CallExpression,
                                                            callee = new Node
                                                            {
                                                                type = NodeType.Super,
                                                                loc = new SourceLocation(new Position(1, 40), new Position(1, 45))
                                                            },
                                                            arguments = new List<Node>(),
                                                            loc = new SourceLocation(new Position(1, 40), new Position(1, 47))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 40), new Position(1, 47))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 38), new Position(1, 49))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 35), new Position(1, 49))
                                        },
                                        kind = "constructor",
                                        @static = false,
                                        loc = new SourceLocation(new Position(1, 24), new Position(1, 49))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 23), new Position(1, 50))
                            },
                            loc = new SourceLocation(new Position(1, 15), new Position(1, 50))
                        },
                        loc = new SourceLocation(new Position(1, 14), new Position(1, 51))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 51))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A {'constructor'() {}}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id =  new Node{type = NodeType.Identifier, name = "A"},
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key =  new Node{type = NodeType.Literal, value = "constructor"},
                                    @static = false,
                                    kind = "constructor",
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        generator = false,
                                        bexpression = false,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>()
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

            Test("class A {static foo() {}}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 16), new Position(1, 19))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 22), new Position(1, 24))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 19), new Position(1, 24))
                                    },
                                    kind = "method",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 24))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 25))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 25))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 25))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A {foo() {} static bar() {}}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 12))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 15), new Position(1, 17))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 17))
                                    },
                                    kind = "method",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 17))
                                },
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "bar",
                                        loc = new SourceLocation(new Position(1, 25), new Position(1, 28))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 31), new Position(1, 33))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 28), new Position(1, 33))
                                    },
                                    kind = "method",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 18), new Position(1, 33))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 34))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 34))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("\"use strict\"; (class A { static constructor() { super() }})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            value = "use strict",
                            raw = "\"use strict\"",
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ClassExpression,
                            id = new Node
                            {
                                type = NodeType.Identifier,
                                name = "A",
                                loc = new SourceLocation(new Position(1, 21), new Position(1, 22))
                            },
                            superClass = null,
                            fbody = new Node
                            {
                                type = NodeType.ClassBody,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.MethodDefinition,
                                        computed = false,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "constructor",
                                            loc = new SourceLocation(new Position(1, 32), new Position(1, 43))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<Node>(),
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.ExpressionStatement,
                                                        expression = new Node
                                                        {
                                                            type = NodeType.CallExpression,
                                                            callee = new Node
                                                            {
                                                                type = NodeType.Super,
                                                                loc = new SourceLocation(new Position(1, 48), new Position(1, 53))
                                                            },
                                                            arguments = new List<Node>(),
                                                            loc = new SourceLocation(new Position(1, 48), new Position(1, 55))
                                                        },
                                                        loc = new SourceLocation(new Position(1, 48), new Position(1, 55))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 46), new Position(1, 57))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 43), new Position(1, 57))
                                        },
                                        kind = "method",
                                        @static = true,
                                        loc = new SourceLocation(new Position(1, 25), new Position(1, 57))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 23), new Position(1, 58))
                            },
                            loc = new SourceLocation(new Position(1, 15), new Position(1, 58))
                        },
                        loc = new SourceLocation(new Position(1, 14), new Position(1, 59))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 59))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { foo() {} bar() {}}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 10), new Position(1, 13))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 16), new Position(1, 18))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 18))
                                    },
                                    kind = "method",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 18))
                                },
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "bar",
                                        loc = new SourceLocation(new Position(1, 19), new Position(1, 22))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 25), new Position(1, 27))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 22), new Position(1, 27))
                                    },
                                    kind = "method",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 19), new Position(1, 27))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 28))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { get foo() {} set foo(v) {}}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 14), new Position(1, 17))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 20), new Position(1, 22))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 17), new Position(1, 22))
                                    },
                                    kind = "get",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 22))
                                },
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 27), new Position(1, 30))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "v",
                                                loc = new SourceLocation(new Position(1, 31), new Position(1, 32))
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 34), new Position(1, 36))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 30), new Position(1, 36))
                                    },
                                    kind = "set",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 23), new Position(1, 36))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 37))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 37))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 37))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { static get foo() {} get foo() {}}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 24))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 27), new Position(1, 29))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 24), new Position(1, 29))
                                    },
                                    kind = "get",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 29))
                                },
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 34), new Position(1, 37))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 40), new Position(1, 42))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 37), new Position(1, 42))
                                    },
                                    kind = "get",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 30), new Position(1, 42))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 43))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 43))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 43))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { static get foo() {} static get bar() {} }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 24))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 27), new Position(1, 29))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 24), new Position(1, 29))
                                    },
                                    kind = "get",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 29))
                                },
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "bar",
                                        loc = new SourceLocation(new Position(1, 41), new Position(1, 44))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 47), new Position(1, 49))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 44), new Position(1, 49))
                                    },
                                    kind = "get",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 30), new Position(1, 49))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 51))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 51))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 51))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { static get foo() {} static set foo(v) {} get foo() {} set foo(v) {}}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 24))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 27), new Position(1, 29))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 24), new Position(1, 29))
                                    },
                                    kind = "get",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 29))
                                },
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 41), new Position(1, 44))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "v",
                                                loc = new SourceLocation(new Position(1, 45), new Position(1, 46))
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 48), new Position(1, 50))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 44), new Position(1, 50))
                                    },
                                    kind = "set",
                                    @static = true,
                                    loc = new SourceLocation(new Position(1, 30), new Position(1, 50))
                                },
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 55), new Position(1, 58))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 61), new Position(1, 63))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 58), new Position(1, 63))
                                    },
                                    kind = "get",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 51), new Position(1, 63))
                                },
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 68), new Position(1, 71))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "v",
                                                loc = new SourceLocation(new Position(1, 72), new Position(1, 73))
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 75), new Position(1, 77))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 71), new Position(1, 77))
                                    },
                                    kind = "set",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 64), new Position(1, 77))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 78))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 78))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 78))
            }, new Options
            {
                ecmaVersion = 6
            });


            Test("class A { static [foo]() {} }", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0), new Position(1, 29)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 29)),
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7)),
                            name = "A"
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 29)),
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 27)),
                                    @static = true,
                                    computed = true,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        loc = new SourceLocation(new Position(1, 18), new Position(1, 21)),
                                        name = "foo"
                                    },
                                    kind = "method",
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        loc = new SourceLocation(new Position(1, 22), new Position(1, 27)),
                                        id = null,
                                        @params = new List<Node>(),
                                        generator = false,
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            loc = new SourceLocation(new Position(1, 25), new Position(1, 27)),
                                            body = new List<Node>()
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

            Test("class A { static get [foo]() {} }", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0), new Position(1, 33)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 33)),
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7)),
                            range = (6, 7),
                            name = "A"
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 33)),
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 31)),
                                    @static = true,
                                    computed = true,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        loc = new SourceLocation(new Position(1, 22), new Position(1, 25)),
                                        name = "foo"
                                    },
                                    kind = "get",
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        loc = new SourceLocation(new Position(1, 26), new Position(1, 31)),
                                        id = null,
                                        @params = new List<Node>(),
                                        generator = false,
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            loc = new SourceLocation(new Position(1, 29), new Position(1, 31)),
                                            body = new List<Node>()
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

            Test("class A { set foo(v) {} get foo() {} }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "A",
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 14), new Position(1, 17))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "v",
                                                loc = new SourceLocation(new Position(1, 18), new Position(1, 19))
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 21), new Position(1, 23))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 17), new Position(1, 23))
                                    },
                                    kind = "set",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 23))
                                },
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "foo",
                                        loc = new SourceLocation(new Position(1, 28), new Position(1, 31))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 34), new Position(1, 36))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 31), new Position(1, 36))
                                    },
                                    kind = "get",
                                    @static = false,
                                    loc = new SourceLocation(new Position(1, 24), new Position(1, 36))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 38))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A { foo() {} get foo() {} }", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0), new Position(1, 33)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 33)),
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7)),
                            name = "A"
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 33)),
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 18)),
                                    @static = false,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        loc = new SourceLocation(new Position(1, 10), new Position(1, 13)),
                                        name = "foo"
                                    },
                                    kind = "method",
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 18)),
                                        id = null,
                                        @params = new List<Node>(),
                                        generator = false,
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            loc = new SourceLocation(new Position(1, 16), new Position(1, 18)),
                                            body = new List<Node>()
                                        },
                                        bexpression = false
                                    }
                                },
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    loc = new SourceLocation(new Position(1, 19), new Position(1, 31)),
                                    @static = false,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        loc = new SourceLocation(new Position(1, 23), new Position(1, 26)),
                                        name = "foo"
                                    },
                                    kind = "get",
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        loc = new SourceLocation(new Position(1, 26), new Position(1, 31)),
                                        id = null,
                                        @params = new List<Node>(),
                                        generator = false,
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            loc = new SourceLocation(new Position(1, 29), new Position(1, 31)),
                                            body = new List<Node>()
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

            Test("class Semicolon { ; }", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0), new Position(1, 21)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 21)),
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 15)),
                            name = "Semicolon"
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            loc = new SourceLocation(new Position(1, 16), new Position(1, 21)),
                            body = new List<Node>()
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Computed Properties

            Test("({[x]: 10})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 10,
                                        raw = "10",
                                        loc = new SourceLocation(new Position(1, 7), new Position(1, 9))
                                    },
                                    kind = "init",
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    loc = new SourceLocation(new Position(1, 2), new Position(1, 9))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 11))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({[\"x\" + \"y\"]: 10})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    key = new Node
                                    {
                                        type = NodeType.BinaryExpression,
                                        @operator = "+",
                                        left = new Node
                                        {
                                            type = NodeType.Literal,
                                            value = "x",
                                            raw = "\"x\"",
                                            loc = new SourceLocation(new Position(1, 3), new Position(1, 6))
                                        },
                                        right = new Node
                                        {
                                            type = NodeType.Literal,
                                            value = "y",
                                            raw = "\"y\"",
                                            loc = new SourceLocation(new Position(1, 9), new Position(1, 12))
                                        },
                                        loc = new SourceLocation(new Position(1, 3), new Position(1, 12))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 10,
                                        raw = "10",
                                        loc = new SourceLocation(new Position(1, 15), new Position(1, 17))
                                    },
                                    kind = "init",
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    loc = new SourceLocation(new Position(1, 2), new Position(1, 17))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({[x]: function() {}})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 18), new Position(1, 20))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 7), new Position(1, 20))
                                    },
                                    kind = "init",
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    loc = new SourceLocation(new Position(1, 2), new Position(1, 20))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({[x]: 10, y: 20})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 10,
                                        raw = "10",
                                        loc = new SourceLocation(new Position(1, 7), new Position(1, 9))
                                    },
                                    kind = "init",
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    loc = new SourceLocation(new Position(1, 2), new Position(1, 9))
                                },
                                new Node
                                {
                                    type = NodeType.Property,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "y",
                                        loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.Literal,
                                        value = 20,
                                        raw = "20",
                                        loc = new SourceLocation(new Position(1, 14), new Position(1, 16))
                                    },
                                    kind = "init",
                                    method = false,
                                    shorthand = false,
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 16))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({get [x]() {}, set [x](v) {}})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 12), new Position(1, 14))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 9), new Position(1, 14))
                                    },
                                    kind = "get",
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    loc = new SourceLocation(new Position(1, 2), new Position(1, 14))
                                },
                                new Node
                                {
                                    type = NodeType.Property,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 21), new Position(1, 22))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "v",
                                                loc = new SourceLocation(new Position(1, 24), new Position(1, 25))
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 27), new Position(1, 29))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 23), new Position(1, 29))
                                    },
                                    kind = "set",
                                    method = false,
                                    shorthand = false,
                                    computed = true,
                                    loc = new SourceLocation(new Position(1, 16), new Position(1, 29))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 30))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 31))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({[x]() {}})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 8), new Position(1, 10))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 5), new Position(1, 10))
                                    },
                                    kind = "init",
                                    method = true,
                                    shorthand = false,
                                    computed = true,
                                    loc = new SourceLocation(new Position(1, 2), new Position(1, 10))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 11))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var {[x]: y} = {y}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "x",
                                                loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "y",
                                                loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = false,
                                            computed = true,
                                            loc = new SourceLocation(new Position(1, 5), new Position(1, 11))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 12))
                                },
                                init = new Node
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "y",
                                                loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "y",
                                                loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 18))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 18))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function f({[x]: y}) {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "f",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                        },
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ObjectPattern,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "y",
                                            loc = new SourceLocation(new Position(1, 17), new Position(1, 18))
                                        },
                                        kind = "init",
                                        method = false,
                                        shorthand = false,
                                        computed = true,
                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 18))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 19))
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 21), new Position(1, 23))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var x = {*[test]() { yield *v; }}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "x",
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                },
                                init = new Node
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "test",
                                                loc = new SourceLocation(new Position(1, 11), new Position(1, 15))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.FunctionExpression,
                                                id = null,
                                                @params = new List<Node>(),
                                                fbody = new Node
                                                {
                                                    type = NodeType.BlockStatement,
                                                    body = new List<Node>
                                                    {
                                                        new Node
                                                        {
                                                            type = NodeType.ExpressionStatement,
                                                            expression = new Node
                                                            {
                                                                type = NodeType.YieldExpression,
                                                                argument = new Node
                                                                {
                                                                    type = NodeType.Identifier,
                                                                    name = "v",
                                                                    loc = new SourceLocation(new Position(1, 28), new Position(1, 29))
                                                                },
                                                                @delegate = true,
                                                                loc = new SourceLocation(new Position(1, 21), new Position(1, 29))
                                                            },
                                                            loc = new SourceLocation(new Position(1, 21), new Position(1, 30))
                                                        }
                                                    },
                                                    loc = new SourceLocation(new Position(1, 19), new Position(1, 32))
                                                },
                                                generator = true,
                                                bexpression = false,
                                                loc = new SourceLocation(new Position(1, 16), new Position(1, 32))
                                            },
                                            kind = "init",
                                            method = true,
                                            shorthand = false,
                                            computed = true,
                                            loc = new SourceLocation(new Position(1, 9), new Position(1, 32))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 33))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 33))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("class A {[x]() {}}", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0), new Position(1, 18)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 18)),
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7)),
                            name = "A"
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            loc = new SourceLocation(new Position(1, 8), new Position(1, 18)),
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 17)),
                                    @static = false,
                                    computed = true,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        loc = new SourceLocation(new Position(1, 10), new Position(1, 11)),
                                        name = "x"
                                    },
                                    kind = "method",
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 17)),
                                        id = null,
                                        @params = new List<Node>(),
                                        generator = false,
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            loc = new SourceLocation(new Position(1, 15), new Position(1, 17)),
                                            body = new List<Node>()
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

            Test("function f([x] = [1]) {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "f",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                        },
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.AssignmentPattern,
                                left = new Node
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 14))
                                },
                                right = new Node
                                {
                                    type = NodeType.ArrayExpression,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Literal,
                                            value = 1,
                                            raw = "1",
                                            loc = new SourceLocation(new Position(1, 18), new Position(1, 19))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 17), new Position(1, 20))
                                },
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 20))
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 22), new Position(1, 24))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function f([x] = [1]) { 'use strict' }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "f",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                        },
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.AssignmentPattern,
                                left = new Node
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x",
                                            loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 14))
                                },
                                right = new Node
                                {
                                    type = NodeType.ArrayExpression,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Literal,
                                            value = 1,
                                            raw = "1",
                                            loc = new SourceLocation(new Position(1, 18), new Position(1, 19))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 17), new Position(1, 20))
                                },
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 20))
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    loc = new SourceLocation(new Position(1, 24), new Position(1, 36)),
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
                                        loc = new SourceLocation(new Position(1, 24), new Position(1, 36)),
                                        value = "use strict",
                                        raw = "'use strict'"
                                    }
                                }
                            },
                            loc = new SourceLocation(new Position(1, 22), new Position(1, 38))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 38))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function f({x} = {x: 10}) {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "f",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                        },
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.AssignmentPattern,
                                left = new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "x",
                                                loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "x",
                                                loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 14))
                                },
                                right = new Node
                                {
                                    type = NodeType.ObjectExpression,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "x",
                                                loc = new SourceLocation(new Position(1, 18), new Position(1, 19))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Literal,
                                                value = 10,
                                                raw = "10",
                                                loc = new SourceLocation(new Position(1, 21), new Position(1, 23))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 18), new Position(1, 23))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 17), new Position(1, 24))
                                },
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 24))
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 26), new Position(1, 28))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 28))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("f = function({x} = {x: 10}) {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "f",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.FunctionExpression,
                                id = null,
                                @params = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.AssignmentPattern,
                                        left = new Node
                                        {
                                            type = NodeType.ObjectPattern,
                                            properties = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Property,
                                                    key = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "x",
                                                        loc = new SourceLocation(new Position(1, 14), new Position(1, 15))
                                                    },
                                                    value = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "x",
                                                        loc = new SourceLocation(new Position(1, 14), new Position(1, 15))
                                                    },
                                                    kind = "init",
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    loc = new SourceLocation(new Position(1, 14), new Position(1, 15))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 13), new Position(1, 16))
                                        },
                                        right = new Node
                                        {
                                            type = NodeType.ObjectExpression,
                                            properties = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Property,
                                                    key = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "x",
                                                        loc = new SourceLocation(new Position(1, 20), new Position(1, 21))
                                                    },
                                                    value = new Node
                                                    {
                                                        type = NodeType.Literal,
                                                        value = 10,
                                                        raw = "10",
                                                        loc = new SourceLocation(new Position(1, 23), new Position(1, 25))
                                                    },
                                                    kind = "init",
                                                    method = false,
                                                    shorthand = false,
                                                    computed = false,
                                                    loc = new SourceLocation(new Position(1, 20), new Position(1, 25))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 19), new Position(1, 26))
                                        },
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 26))
                                    }
                                },
                                fbody = new Node
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<Node>(),
                                    loc = new SourceLocation(new Position(1, 28), new Position(1, 30))
                                },
                                generator = false,
                                bexpression = false,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 30))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 30))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({f: function({x} = {x: 10}) {}})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "f",
                                        loc = new SourceLocation(new Position(1, 2), new Position(1, 3))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.AssignmentPattern,
                                                left = new Node
                                                {
                                                    type = NodeType.ObjectPattern,
                                                    properties = new List<Node>
                                                    {
                                                        new Node
                                                        {
                                                            type = NodeType.Property,
                                                            key = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "x",
                                                                loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                                            },
                                                            value = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "x",
                                                                loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                                            },
                                                            kind = "init",
                                                            method = false,
                                                            shorthand = true,
                                                            computed = false,
                                                            loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                                        }
                                                    },
                                                    loc = new SourceLocation(new Position(1, 14), new Position(1, 17))
                                                },
                                                right = new Node
                                                {
                                                    type = NodeType.ObjectExpression,
                                                    properties = new List<Node>
                                                    {
                                                        new Node
                                                        {
                                                            type = NodeType.Property,
                                                            key = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "x",
                                                                loc = new SourceLocation(new Position(1, 21), new Position(1, 22))
                                                            },
                                                            value = new Node
                                                            {
                                                                type = NodeType.Literal,
                                                                value = 10,
                                                                raw = "10",
                                                                loc = new SourceLocation(new Position(1, 24), new Position(1, 26))
                                                            },
                                                            kind = "init",
                                                            method = false,
                                                            shorthand = false,
                                                            computed = false,
                                                            loc = new SourceLocation(new Position(1, 21), new Position(1, 26))
                                                        }
                                                    },
                                                    loc = new SourceLocation(new Position(1, 20), new Position(1, 27))
                                                },
                                                loc = new SourceLocation(new Position(1, 14), new Position(1, 27))
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 29), new Position(1, 31))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 5), new Position(1, 31))
                                    },
                                    kind = "init",
                                    method = false,
                                    shorthand = false,
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 2), new Position(1, 31))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 32))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({f({x} = {x: 10}) {}})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "f",
                                        loc = new SourceLocation(new Position(1, 2), new Position(1, 3))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.AssignmentPattern,
                                                left = new Node
                                                {
                                                    type = NodeType.ObjectPattern,
                                                    properties = new List<Node>
                                                    {
                                                        new Node
                                                        {
                                                            type = NodeType.Property,
                                                            key = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "x",
                                                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                                            },
                                                            value = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "x",
                                                                loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                                            },
                                                            kind = "init",
                                                            method = false,
                                                            shorthand = true,
                                                            computed = false,
                                                            loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                                        }
                                                    },
                                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 7))
                                                },
                                                right = new Node
                                                {
                                                    type = NodeType.ObjectExpression,
                                                    properties = new List<Node>
                                                    {
                                                        new Node
                                                        {
                                                            type = NodeType.Property,
                                                            key = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                name = "x",
                                                                loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                                                            },
                                                            value = new Node
                                                            {
                                                                type = NodeType.Literal,
                                                                value = 10,
                                                                raw = "10",
                                                                loc = new SourceLocation(new Position(1, 14), new Position(1, 16))
                                                            },
                                                            kind = "init",
                                                            method = false,
                                                            shorthand = false,
                                                            computed = false,
                                                            loc = new SourceLocation(new Position(1, 11), new Position(1, 16))
                                                        }
                                                    },
                                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 17))
                                                },
                                                loc = new SourceLocation(new Position(1, 4), new Position(1, 17))
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 19), new Position(1, 21))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 3), new Position(1, 21))
                                    },
                                    kind = "init",
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 2), new Position(1, 21))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(class {f({x} = {x: 10}) {}})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ClassExpression,
                            superClass = null,
                            fbody = new Node
                            {
                                type = NodeType.ClassBody,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.MethodDefinition,
                                        computed = false,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "f",
                                            loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = NodeType.AssignmentPattern,
                                                    left = new Node
                                                    {
                                                        type = NodeType.ObjectPattern,
                                                        properties = new List<Node>
                                                        {
                                                            new Node
                                                            {
                                                                type = NodeType.Property,
                                                                key = new Node
                                                                {
                                                                    type = NodeType.Identifier,
                                                                    name = "x",
                                                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                                                                },
                                                                value = new Node
                                                                {
                                                                    type = NodeType.Identifier,
                                                                    name = "x",
                                                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                                                                },
                                                                kind = "init",
                                                                method = false,
                                                                shorthand = true,
                                                                computed = false,
                                                                loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                                                            }
                                                        },
                                                        loc = new SourceLocation(new Position(1, 10), new Position(1, 13))
                                                    },
                                                    right = new Node
                                                    {
                                                        type = NodeType.ObjectExpression,
                                                        properties = new List<Node>
                                                        {
                                                            new Node
                                                            {
                                                                type = NodeType.Property,
                                                                key = new Node
                                                                {
                                                                    type = NodeType.Identifier,
                                                                    name = "x",
                                                                    loc = new SourceLocation(new Position(1, 17), new Position(1, 18))
                                                                },
                                                                value = new Node
                                                                {
                                                                    type = NodeType.Literal,
                                                                    value = 10,
                                                                    raw = "10",
                                                                    loc = new SourceLocation(new Position(1, 20), new Position(1, 22))
                                                                },
                                                                kind = "init",
                                                                method = false,
                                                                shorthand = false,
                                                                computed = false,
                                                                loc = new SourceLocation(new Position(1, 17), new Position(1, 22))
                                                            }
                                                        },
                                                        loc = new SourceLocation(new Position(1, 16), new Position(1, 23))
                                                    },
                                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 23))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 25), new Position(1, 27))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 9), new Position(1, 27))
                                        },
                                        kind = "method",
                                        @static = false,
                                        loc = new SourceLocation(new Position(1, 8), new Position(1, 27))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 7), new Position(1, 28))
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 28))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 29))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(({x} = {x: 10}) => {})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.AssignmentPattern,
                                    left = new Node
                                    {
                                        type = NodeType.ObjectPattern,
                                        properties = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Property,
                                                key = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "x",
                                                    loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                                },
                                                value = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "x",
                                                    loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                                },
                                                kind = "init",
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 2), new Position(1, 5))
                                    },
                                    right = new Node
                                    {
                                        type = NodeType.ObjectExpression,
                                        properties = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Property,
                                                key = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "x",
                                                    loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                                },
                                                value = new Node
                                                {
                                                    type = NodeType.Literal,
                                                    value = 10,
                                                    raw = "10",
                                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 14))
                                                },
                                                kind = "init",
                                                method = false,
                                                shorthand = false,
                                                computed = false,
                                                loc = new SourceLocation(new Position(1, 9), new Position(1, 14))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 8), new Position(1, 15))
                                    },
                                    loc = new SourceLocation(new Position(1, 2), new Position(1, 15))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 20), new Position(1, 22))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 23))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x = function(y = 1) {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.FunctionExpression,
                                id = null,
                                @params = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.AssignmentPattern,
                                        left = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "y",
                                            loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                        },
                                        right = new Node
                                        {
                                            type = NodeType.Literal,
                                            value = 1,
                                            raw = "1",
                                            loc = new SourceLocation(new Position(1, 17), new Position(1, 18))
                                        },
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 18))
                                    }
                                },
                                fbody = new Node
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<Node>(),
                                    loc = new SourceLocation(new Position(1, 20), new Position(1, 22))
                                },
                                generator = false,
                                bexpression = false,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 22))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function f(a = 1) {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "f",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                        },
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.AssignmentPattern,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "a",
                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                                },
                                right = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 1,
                                    raw = "1",
                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                },
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 16))
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 18), new Position(1, 20))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x = { f: function(a=1) {} }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "f",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = NodeType.AssignmentPattern,
                                                    left = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "a",
                                                        loc = new SourceLocation(new Position(1, 18), new Position(1, 19))
                                                    },
                                                    right = new Node
                                                    {
                                                        type = NodeType.Literal,
                                                        value = 1,
                                                        raw = "1",
                                                        loc = new SourceLocation(new Position(1, 20), new Position(1, 21))
                                                    },
                                                    loc = new SourceLocation(new Position(1, 18), new Position(1, 21))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 23), new Position(1, 25))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 9), new Position(1, 25))
                                        },
                                        kind = "init",
                                        method = false,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 25))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 27))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("x = { f(a=1) {} }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                            },
                            right = new Node
                            {
                                type = NodeType.ObjectExpression,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "f",
                                            loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.FunctionExpression,
                                            id = null,
                                            @params = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = NodeType.AssignmentPattern,
                                                    left = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "a",
                                                        loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                                    },
                                                    right = new Node
                                                    {
                                                        type = NodeType.Literal,
                                                        value = 1,
                                                        raw = "1",
                                                        loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                                                    },
                                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 11))
                                                }
                                            },
                                            fbody = new Node
                                            {
                                                type = NodeType.BlockStatement,
                                                body = new List<Node>(),
                                                loc = new SourceLocation(new Position(1, 13), new Position(1, 15))
                                            },
                                            generator = false,
                                            bexpression = false,
                                            loc = new SourceLocation(new Position(1, 7), new Position(1, 15))
                                        },
                                        kind = "init",
                                        method = true,
                                        shorthand = false,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 6), new Position(1, 15))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 17))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Rest parameters

            Test("function f(a, ...b) {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "f",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                        },
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.Identifier,
                                name = "a",
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                            },
                            new Node
                            {
                                type = NodeType.RestElement,
                                argument = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "b",
                                    loc = new SourceLocation(new Position(1, 17), new Position(1, 18))
                                }
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 20), new Position(1, 22))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: Destructured Parameters

            Test("function x([ a, b ]){}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "x",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                        },
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ArrayPattern,
                                elements = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "a",
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                    },
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "b",
                                        loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 19))
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 20), new Position(1, 22))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("function x({ a, b }){}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "x",
                            loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                        },
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ObjectPattern,
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "a",
                                            loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "a",
                                            loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                        },
                                        kind = "init",
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                    },
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "b",
                                            loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                                        },
                                        value = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "b",
                                            loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                                        },
                                        kind = "init",
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 19))
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>(),
                            loc = new SourceLocation(new Position(1, 20), new Position(1, 22))
                        },
                        generator = false,
                        bexpression = false,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }, new Options
            {
                ecmaVersion = 6
            });

            testFail("function x(...[ a, b ]){}", "Unexpected token (1:14)", new Options {ecmaVersion = 6});

            testFail("function x({ a: { w, x }, b: [y, z] }, ...[a, b, c]){}", "Unexpected token (1:42)", new Options {ecmaVersion = 6});

            Test("(function x([ a, b ]){})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                            },
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "a",
                                            loc = new SourceLocation(new Position(1, 14), new Position(1, 15))
                                        },
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "b",
                                            loc = new SourceLocation(new Position(1, 17), new Position(1, 18))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 20))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 21), new Position(1, 23))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 23))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(function x({ a, b }){})", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x",
                                loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                            },
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 14), new Position(1, 15))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 14), new Position(1, 15))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 14), new Position(1, 15))
                                        },
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "b",
                                                loc = new SourceLocation(new Position(1, 17), new Position(1, 18))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "b",
                                                loc = new SourceLocation(new Position(1, 17), new Position(1, 18))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 17), new Position(1, 18))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 20))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 21), new Position(1, 23))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 23))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            testFail("(function x(...[ a, b ]){})", "Unexpected token (1:15)", new Options {ecmaVersion = 6});
            testFail("var a = { set foo(...v) {} };", "Setter cannot use rest params (1:18)", new Options {ecmaVersion = 6});
            testFail("class a { set foo(...v) {} };", "Setter cannot use rest params (1:18)", new Options {ecmaVersion = 6});

            testFail("(function x({ a: { w, x }, b: [y, z] }, ...[a, b, c]){})", "Unexpected token (1:43)", new Options {ecmaVersion = 6});

            Test("({ x([ a, b ]){} })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.ArrayPattern,
                                                elements = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "a",
                                                        loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                                    },
                                                    new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "b",
                                                        loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 5), new Position(1, 13))
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 14), new Position(1, 16))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 4), new Position(1, 16))
                                    },
                                    kind = "init",
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 3), new Position(1, 16))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({ x(...[ a, b ]){} })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.RestElement,
                                                argument = new Node
                                                {
                                                    type = NodeType.ArrayPattern,
                                                    elements = new List<Node>
                                                    {
                                                        new Node
                                                        {
                                                            type = NodeType.Identifier,
                                                            name = "a",
                                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                                                        },
                                                        new Node
                                                        {
                                                            type = NodeType.Identifier,
                                                            name = "b",
                                                            loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                                        }
                                                    },
                                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 16))
                                                }
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 17), new Position(1, 19))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 4), new Position(1, 19))
                                    },
                                    kind = "init",
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 3), new Position(1, 19))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 21))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }, new Options
            {
                ecmaVersion = 7
            });

            Test("({ x({ a: { w, x }, b: [y, z] }, ...[a, b, c]){} })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ObjectExpression,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                    },
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        id = null,
                                        @params = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.ObjectPattern,
                                                properties = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.Property,
                                                        key = new Node
                                                        {
                                                            type = NodeType.Identifier,
                                                            name = "a",
                                                            loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                                        },
                                                        value = new Node
                                                        {
                                                            type = NodeType.ObjectPattern,
                                                            properties = new List<Node>
                                                            {
                                                                new Node
                                                                {
                                                                    type = NodeType.Property,
                                                                    key = new Node
                                                                    {
                                                                        type = NodeType.Identifier,
                                                                        name = "w",
                                                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                                                    },
                                                                    value = new Node
                                                                    {
                                                                        type = NodeType.Identifier,
                                                                        name = "w",
                                                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                                                    },
                                                                    kind = "init",
                                                                    method = false,
                                                                    shorthand = true,
                                                                    computed = false,
                                                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                                                },
                                                                new Node
                                                                {
                                                                    type = NodeType.Property,
                                                                    key = new Node
                                                                    {
                                                                        type = NodeType.Identifier,
                                                                        name = "x",
                                                                        loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                                                    },
                                                                    value = new Node
                                                                    {
                                                                        type = NodeType.Identifier,
                                                                        name = "x",
                                                                        loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                                                    },
                                                                    kind = "init",
                                                                    method = false,
                                                                    shorthand = true,
                                                                    computed = false,
                                                                    loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                                                }
                                                            },
                                                            loc = new SourceLocation(new Position(1, 10), new Position(1, 18))
                                                        },
                                                        kind = "init",
                                                        method = false,
                                                        shorthand = false,
                                                        computed = false,
                                                        loc = new SourceLocation(new Position(1, 7), new Position(1, 18))
                                                    },
                                                    new Node
                                                    {
                                                        type = NodeType.Property,
                                                        key = new Node
                                                        {
                                                            type = NodeType.Identifier,
                                                            name = "b",
                                                            loc = new SourceLocation(new Position(1, 20), new Position(1, 21))
                                                        },
                                                        value = new Node
                                                        {
                                                            type = NodeType.ArrayPattern,
                                                            elements = new List<Node>
                                                            {
                                                                new Node
                                                                {
                                                                    type = NodeType.Identifier,
                                                                    name = "y",
                                                                    loc = new SourceLocation(new Position(1, 24), new Position(1, 25))
                                                                },
                                                                new Node
                                                                {
                                                                    type = NodeType.Identifier,
                                                                    name = "z",
                                                                    loc = new SourceLocation(new Position(1, 27), new Position(1, 28))
                                                                }
                                                            },
                                                            loc = new SourceLocation(new Position(1, 23), new Position(1, 29))
                                                        },
                                                        kind = "init",
                                                        method = false,
                                                        shorthand = false,
                                                        computed = false,
                                                        loc = new SourceLocation(new Position(1, 20), new Position(1, 29))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 5), new Position(1, 31))
                                            },
                                            new Node
                                            {
                                                type = NodeType.RestElement,
                                                argument = new Node
                                                {
                                                    type = NodeType.ArrayPattern,
                                                    elements = new List<Node>
                                                    {
                                                        new Node
                                                        {
                                                            type = NodeType.Identifier,
                                                            name = "a",
                                                            loc = new SourceLocation(new Position(1, 37), new Position(1, 38))
                                                        },
                                                        new Node
                                                        {
                                                            type = NodeType.Identifier,
                                                            name = "b",
                                                            loc = new SourceLocation(new Position(1, 40), new Position(1, 41))
                                                        },
                                                        new Node
                                                        {
                                                            type = NodeType.Identifier,
                                                            name = "c",
                                                            loc = new SourceLocation(new Position(1, 43), new Position(1, 44))
                                                        }
                                                    },
                                                    loc = new SourceLocation(new Position(1, 36), new Position(1, 45))
                                                }
                                            }
                                        },
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            body = new List<Node>(),
                                            loc = new SourceLocation(new Position(1, 46), new Position(1, 48))
                                        },
                                        generator = false,
                                        bexpression = false,
                                        loc = new SourceLocation(new Position(1, 4), new Position(1, 48))
                                    },
                                    kind = "init",
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    loc = new SourceLocation(new Position(1, 3), new Position(1, 48))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 50))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 51))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 51))
            }, new Options
            {
                ecmaVersion = 7
            });

            Test("(...a) => {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.RestElement,
                                    argument = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "a",
                                        loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                    }
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 10), new Position(1, 12))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 12))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("(a, ...b) => {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "a",
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                                },
                                new Node
                                {
                                    type = NodeType.RestElement,
                                    argument = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "b",
                                        loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                    }
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 15))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 15))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({ a }) => {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 6))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 11), new Position(1, 13))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({ a }, ...b) => {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 6))
                                },
                                new Node
                                {
                                    type = NodeType.RestElement,
                                    argument = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "b",
                                        loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                                    }
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 17), new Position(1, 19))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 19))
            }, new Options
            {
                ecmaVersion = 6
            });

            testFail("(...[a, b]) => {}", "Unexpected token (1:4)", new Options {ecmaVersion = 6});

            testFail("(a, ...[b]) => {}", "Unexpected token (1:7)", new Options {ecmaVersion = 6});

            Test("({ a: [a, b] }, ...c) => {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.ArrayPattern,
                                                elements = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "a",
                                                        loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                                    },
                                                    new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "b",
                                                        loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 6), new Position(1, 12))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 3), new Position(1, 12))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 14))
                                },
                                new Node
                                {
                                    type = NodeType.RestElement,
                                    argument = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "c",
                                        loc = new SourceLocation(new Position(1, 19), new Position(1, 20))
                                    }
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 25), new Position(1, 27))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({ a: b, c }, [d, e], ...f) => {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "b",
                                                loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 3), new Position(1, 7))
                                        },
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "c",
                                                loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "c",
                                                loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                            },
                                            kind = "init",
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 1), new Position(1, 12))
                                },
                                new Node
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "d",
                                            loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                        },
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "e",
                                            loc = new SourceLocation(new Position(1, 18), new Position(1, 19))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 14), new Position(1, 20))
                                },
                                new Node
                                {
                                    type = NodeType.RestElement,
                                    argument = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "f",
                                        loc = new SourceLocation(new Position(1, 25), new Position(1, 26))
                                    }
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>(),
                                loc = new SourceLocation(new Position(1, 31), new Position(1, 33))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
            }, new Options
            {
                ecmaVersion = 6
            });

            // ES6: SpreadElement

            Test("[...a] = b", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.ArrayPattern,
                                elements = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.RestElement,
                                        argument = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "a",
                                            loc = new SourceLocation(new Position(1, 4), new Position(1, 5))
                                        },
                                        loc = new SourceLocation(new Position(1, 1), new Position(1, 5))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 6))
                            },
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "b",
                                loc = new SourceLocation(new Position(1, 9), new Position(1, 10))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("[a, ...b] = c", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.ArrayPattern,
                                elements = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "a",
                                        loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                                    },
                                    new Node
                                    {
                                        type = NodeType.RestElement,
                                        argument = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "b",
                                            loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                        },
                                        loc = new SourceLocation(new Position(1, 4), new Position(1, 8))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                            },
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "c",
                                loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("[{ a, b }, ...c] = d", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.ArrayPattern,
                                elements = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ObjectPattern,
                                        properties = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Property,
                                                key = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "a",
                                                    loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                                },
                                                value = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "a",
                                                    loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                                },
                                                kind = "init",
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                loc = new SourceLocation(new Position(1, 3), new Position(1, 4))
                                            },
                                            new Node
                                            {
                                                type = NodeType.Property,
                                                key = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "b",
                                                    loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                                },
                                                value = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "b",
                                                    loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                                },
                                                kind = "init",
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                loc = new SourceLocation(new Position(1, 6), new Position(1, 7))
                                            }
                                        },
                                        loc = new SourceLocation(new Position(1, 1), new Position(1, 9))
                                    },
                                    new Node
                                    {
                                        type = NodeType.RestElement,
                                        argument = new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "c",
                                            loc = new SourceLocation(new Position(1, 14), new Position(1, 15))
                                        },
                                        loc = new SourceLocation(new Position(1, 11), new Position(1, 15))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 16))
                            },
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "d",
                                loc = new SourceLocation(new Position(1, 19), new Position(1, 20))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 20))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("[a, ...[b, c]] = d", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.ArrayPattern,
                                elements = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "a",
                                        loc = new SourceLocation(new Position(1, 1), new Position(1, 2))
                                    },
                                    new Node
                                    {
                                        type = NodeType.RestElement,
                                        argument = new Node
                                        {
                                            type = NodeType.ArrayPattern,
                                            elements = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "b",
                                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                                },
                                                new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    name = "c",
                                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 7), new Position(1, 13))
                                        },
                                        loc = new SourceLocation(new Position(1, 4), new Position(1, 13))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                            },
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                name = "d",
                                loc = new SourceLocation(new Position(1, 17), new Position(1, 18))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 18))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var [...a] = b", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.RestElement,
                                            argument = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "a",
                                                loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                            },
                                            loc = new SourceLocation(new Position(1, 5), new Position(1, 9))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 10))
                                },
                                init = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "b",
                                    loc = new SourceLocation(new Position(1, 13), new Position(1, 14))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 14))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var [a, ...b] = c", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "a",
                                            loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                        },
                                        new Node
                                        {
                                            type = NodeType.RestElement,
                                            argument = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "b",
                                                loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                                            },
                                            loc = new SourceLocation(new Position(1, 8), new Position(1, 12))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 13))
                                },
                                init = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "c",
                                    loc = new SourceLocation(new Position(1, 16), new Position(1, 17))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 17))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 17))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var [{ a, b }, ...c] = d", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.ObjectPattern,
                                            properties = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Property,
                                                    key = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "a",
                                                        loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                                    },
                                                    value = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "a",
                                                        loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                                    },
                                                    kind = "init",
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    loc = new SourceLocation(new Position(1, 7), new Position(1, 8))
                                                },
                                                new Node
                                                {
                                                    type = NodeType.Property,
                                                    key = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "b",
                                                        loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                                                    },
                                                    value = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "b",
                                                        loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                                                    },
                                                    kind = "init",
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    loc = new SourceLocation(new Position(1, 10), new Position(1, 11))
                                                }
                                            },
                                            loc = new SourceLocation(new Position(1, 5), new Position(1, 13))
                                        },
                                        new Node
                                        {
                                            type = NodeType.RestElement,
                                            argument = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "c",
                                                loc = new SourceLocation(new Position(1, 18), new Position(1, 19))
                                            },
                                            loc = new SourceLocation(new Position(1, 15), new Position(1, 19))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 20))
                                },
                                init = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "d",
                                    loc = new SourceLocation(new Position(1, 23), new Position(1, 24))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 24))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 24))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var [a, ...[b, c]] = d", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "a",
                                            loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                        },
                                        new Node
                                        {
                                            type = NodeType.RestElement,
                                            argument = new Node
                                            {
                                                type = NodeType.ArrayPattern,
                                                elements = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "b",
                                                        loc = new SourceLocation(new Position(1, 12), new Position(1, 13))
                                                    },
                                                    new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        name = "c",
                                                        loc = new SourceLocation(new Position(1, 15), new Position(1, 16))
                                                    }
                                                },
                                                loc = new SourceLocation(new Position(1, 11), new Position(1, 17))
                                            },
                                            loc = new SourceLocation(new Position(1, 8), new Position(1, 17))
                                        }
                                    },
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 18))
                                },
                                init = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "d",
                                    loc = new SourceLocation(new Position(1, 21), new Position(1, 22))
                                },
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 22))
                            }
                        },
                        kind = "var",
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 22))
            }, new Options
            {
                ecmaVersion = 7
            });

            Test("func(...a)", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            callee = new Node
                            {
                                type = NodeType.Identifier,
                                name = "func",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                            },
                            arguments = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.SpreadElement,
                                    argument = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "a",
                                        loc = new SourceLocation(new Position(1, 8), new Position(1, 9))
                                    },
                                    loc = new SourceLocation(new Position(1, 5), new Position(1, 9))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 10))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("func(a, ...b)", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            callee = new Node
                            {
                                type = NodeType.Identifier,
                                name = "func",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 4))
                            },
                            arguments = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "a",
                                    loc = new SourceLocation(new Position(1, 5), new Position(1, 6))
                                },
                                new Node
                                {
                                    type = NodeType.SpreadElement,
                                    argument = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "b",
                                        loc = new SourceLocation(new Position(1, 11), new Position(1, 12))
                                    },
                                    loc = new SourceLocation(new Position(1, 8), new Position(1, 12))
                                }
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13))
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("func(...a, b)", new Node
            {
                type = NodeType.Program,
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13)),
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 13)),
                            callee = new Node
                            {
                                type = NodeType.Identifier,
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 4)),
                                name = "func"
                            },
                            arguments = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.SpreadElement,
                                    loc = new SourceLocation(new Position(1, 5), new Position(1, 9)),
                                    argument = new Node
                                    {
                                        type = NodeType.Identifier,
                                        loc = new SourceLocation(new Position(1, 8), new Position(1, 9)),
                                        name = "a"
                                    }
                                },
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    loc = new SourceLocation(new Position(1, 11), new Position(1, 12)),
                                    name = "b"
                                }
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("/[a-z]/u", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            regex = new RegexNode
                            {
                                pattern = "[a-z]",
                                flags = "u"
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 8))
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("/[\\uD834\\uDF06-\\uD834\\uDF08a-z]/u", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.Literal,
                            regex = new RegexNode
                            {
                                pattern = "[\\uD834\\uDF06-\\uD834\\uDF08a-z]",
                                flags = "u"
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 33))
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("do {} while (false) foo();", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 26,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.DoWhileStatement,
                        start = 0,
                        end = 19,
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 3,
                            end = 5,
                            body = new List<Node>()
                        },
                        test = new Node
                        {
                            type = NodeType.Literal,
                            start = 13,
                            end = 18,
                            value = false,
                            raw = "false"
                        }
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 20,
                        end = 26,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            start = 20,
                            end = 25,
                            callee = new Node
                            {
                                type = NodeType.Identifier,
                                start = 20,
                                end = 23,
                                name = "foo"
                            },
                            arguments = new List<Node>()
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

            testFail("var super", "Unexpected token (1:4)", new Options {ecmaVersion = 6});

            testFail("var default", "Unexpected token (1:4)", new Options {ecmaVersion = 6});

            testFail("let default", "Unexpected token (1:4)", new Options {ecmaVersion = 6});

            testFail("const default", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

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

            testFail("let [this] = [10]", "Unexpected token (1:5)", new Options {ecmaVersion = 6});
            testFail("let {this} = x", "Unexpected keyword 'this' (1:5)", new Options {ecmaVersion = 6});
            testFail("let [function] = [10]", "Unexpected token (1:5)", new Options {ecmaVersion = 6});
            testFail("let [function] = x", "Unexpected token (1:5)", new Options {ecmaVersion = 6});
            testFail("([function] = [10])", "Unexpected token (1:10)", new Options {ecmaVersion = 6});
            testFail("([this] = [10])", "Assigning to rvalue (1:2)", new Options {ecmaVersion = 6});
            testFail("({this} = x)", "Unexpected keyword 'this' (1:2)", new Options {ecmaVersion = 6});
            testFail("var x = {this}", "Unexpected keyword 'this' (1:9)", new Options {ecmaVersion = 6});

            Test("yield* 10", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            @operator = "*",
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "yield",
                                loc = new SourceLocation(new Position(1, 0), new Position(1, 5))
                            },
                            right = new Node
                            {
                                type = NodeType.Literal,
                                value = 10,
                                raw = "10",
                                loc = new SourceLocation(new Position(1, 7), new Position(1, 9))
                            },
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 9))
            }, new Options
            {
                ecmaVersion = 6,
                loose = false
            });

            Test("e => yield* 10", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            id = null,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "e",
                                    loc = new SourceLocation(new Position(1, 0), new Position(1, 1))
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BinaryExpression,
                                @operator = "*",
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "yield",
                                    loc = new SourceLocation(new Position(1, 5), new Position(1, 10))
                                },
                                right = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 10,
                                    raw = "10",
                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 14))
                                },
                                loc = new SourceLocation(new Position(1, 5), new Position(1, 14))
                            },
                            generator = false,
                            bexpression = true,
                            loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 14))
            }, new Options
            {
                ecmaVersion = 6,
                loose = false
            });

            testFail("(function () { yield 10 })", "Unexpected token (1:21)", new Options {ecmaVersion = 6});

            Test("(function () { yield* 10 })", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.FunctionExpression,
                            id = null,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                body = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.ExpressionStatement,
                                        expression = new Node
                                        {
                                            type = NodeType.BinaryExpression,
                                            @operator = "*",
                                            left = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "yield",
                                                loc = new SourceLocation(new Position(1, 15), new Position(1, 20))
                                            },
                                            right = new Node
                                            {
                                                type = NodeType.Literal,
                                                value = 10,
                                                raw = "10",
                                                loc = new SourceLocation(new Position(1, 22), new Position(1, 24))
                                            },
                                            loc = new SourceLocation(new Position(1, 15), new Position(1, 24))
                                        },
                                        loc = new SourceLocation(new Position(1, 15), new Position(1, 24))
                                    }
                                },
                                loc = new SourceLocation(new Position(1, 13), new Position(1, 26))
                            },
                            generator = false,
                            bexpression = false,
                            loc = new SourceLocation(new Position(1, 1), new Position(1, 26))
                        },
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
                    }
                },
                loc = new SourceLocation(new Position(1, 0), new Position(1, 27))
            }, new Options
            {
                ecmaVersion = 6,
                loose = false
            });

            Test("let + 1", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.BinaryExpression,
                            left = new Node
                            {
                                type = NodeType.Identifier,
                                name = "let"
                            },
                            @operator = "+",
                            right = new Node
                            {
                                type = NodeType.Literal,
                                value = 1,
                                raw = "1"
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("var let = 1", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "let"
                                },
                                init = new Node
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

            Test("var yield = 2", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "yield"
                                },
                                init = new Node
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

            Test("[...{ a }] = b", new Node { }, new Options {ecmaVersion = 6});

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
            Test("doSmth(`${x} + ${y} = ${x + y}`)", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.CallExpression,
                            callee = new Node
                            {
                                type = NodeType.Identifier,
                                name = "doSmth"
                            },
                            arguments = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.TemplateLiteral,
                                    expressions = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x"
                                        },
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "y"
                                        },
                                        new Node
                                        {
                                            type = NodeType.BinaryExpression,
                                            left = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "x"
                                            },
                                            @operator = "+",
                                            right = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "y"
                                            }
                                        }
                                    },
                                    quasis = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode("", ""),
                                            tail = false
                                        },
                                        new Node
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode(" + ", " + "),
                                            tail = false
                                        },
                                        new Node
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode(" = ", " = "),
                                            tail = false
                                        },
                                        new Node
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
            Test("function normal(x, y = 10) {}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "normal"
                        },
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.Identifier,
                                name = "x"
                            },
                            new Node
                            {
                                type = NodeType.AssignmentPattern,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "y"
                                },
                                right = new Node
                                {
                                    type = NodeType.Literal,
                                    value = 10,
                                    raw = "10"
                                }
                            }
                        },
                        generator = false,
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>()
                        },
                        bexpression = false
                    }
                }
            }, new Options {ecmaVersion = 6});

            // test preserveParens option with arrow functions
            Test("() => 42", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, preserveParens = true});

            // https://github.com/ternjs/acorn/issues/161
            Test("import foo, * as bar from 'baz';", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ImportDeclaration,
                        specifiers = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.ImportDefaultSpecifier,
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "foo"
                                }
                            },
                            new Node
                            {
                                type = NodeType.ImportNamespaceSpecifier,
                                local = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "bar"
                                }
                            }
                        },
                        source = new Node
                        {
                            type = NodeType.Literal,
                            value = "baz",
                            raw = "'baz'"
                        }
                    }
                }
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            // https://github.com/ternjs/acorn/issues/173
            Test("`{${x}}`, `}`", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.SequenceExpression,
                            expressions = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.TemplateLiteral,
                                    expressions = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            name = "x"
                                        }
                                    },
                                    quasis = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode("{", "{"),
                                            tail = false
                                        },
                                        new Node
                                        {
                                            type = NodeType.TemplateElement,
                                            value = new TemplateNode("}", "}"),
                                            tail = true
                                        }
                                    }
                                },
                                new Node
                                {
                                    type = NodeType.TemplateLiteral,
                                    expressions = new List<Node>(),
                                    quasis = new List<Node>
                                    {
                                        new Node
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
            Test("var {get} = obj;", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                id = new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "get"
                                            },
                                            kind = "init",
                                            value = new Node
                                            {
                                                type = NodeType.Identifier,
                                                name = "get"
                                            }
                                        }
                                    }
                                },
                                init = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "obj"
                                }
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options {ecmaVersion = 6});

            // Destructuring defaults (https://github.com/ternjs/acorn/issues/181)

            Test("var {propName: localVar = defaultValue} = obj", new Node
            {
                type = NodeType.Program,
                range = (0, 45),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        range = (0, 45),
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                range = (4, 45),
                                id = new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    range = (4, 39),
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            range = (5, 38),
                                            method = false,
                                            shorthand = false,
                                            computed = false,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                range = (5, 13),
                                                name = "propName"
                                            },
                                            value = new Node
                                            {
                                                type = NodeType.AssignmentPattern,
                                                range = (15, 38),
                                                left = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    range = (15, 23),
                                                    name = "localVar"
                                                },
                                                right = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    range = (26, 38),
                                                    name = "defaultValue"
                                                }
                                            },
                                            kind = "init"
                                        }
                                    }
                                },
                                init = new Node
                                {
                                    type = NodeType.Identifier,
                                    range = (42, 45),
                                    name = "obj"
                                }
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var {propName = defaultValue} = obj", new Node
            {
                type = NodeType.Program,
                range = (0, 35),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        range = (0, 35),
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                range = (4, 35),
                                id = new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    range = (4, 29),
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            range = (5, 28),
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                range = (5, 13),
                                                name = "propName"
                                            },
                                            kind = "init",
                                            value = new Node
                                            {
                                                type = NodeType.AssignmentPattern,
                                                range = (5, 28),
                                                left = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    range = (5, 13),
                                                    name = "propName"
                                                },
                                                right = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    range = (16, 28),
                                                    name = "defaultValue"
                                                }
                                            }
                                        }
                                    }
                                },
                                init = new Node
                                {
                                    type = NodeType.Identifier,
                                    range = (32, 35),
                                    name = "obj"
                                }
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var {get = defaultValue} = obj", new Node
            {
                type = NodeType.Program,
                range = (0, 30),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        range = (0, 30),
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                range = (4, 30),
                                id = new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    range = (4, 24),
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            range = (5, 23),
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                range = (5, 8),
                                                name = "get"
                                            },
                                            kind = "init",
                                            value = new Node
                                            {
                                                type = NodeType.AssignmentPattern,
                                                range = (5, 23),
                                                left = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    range = (5, 8),
                                                    name = "get"
                                                },
                                                right = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    range = (11, 23),
                                                    name = "defaultValue"
                                                }
                                            }
                                        }
                                    }
                                },
                                init = new Node
                                {
                                    type = NodeType.Identifier,
                                    range = (27, 30),
                                    name = "obj"
                                }
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("var [localVar = defaultValue] = obj", new Node
            {
                type = NodeType.Program,
                range = (0, 35),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        range = (0, 35),
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                range = (4, 35),
                                id = new Node
                                {
                                    type = NodeType.ArrayPattern,
                                    range = (4, 29),
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.AssignmentPattern,
                                            range = (5, 28),
                                            left = new Node
                                            {
                                                type = NodeType.Identifier,
                                                range = (5, 13),
                                                name = "localVar"
                                            },
                                            right = new Node
                                            {
                                                type = NodeType.Identifier,
                                                range = (16, 28),
                                                name = "defaultValue"
                                            }
                                        }
                                    }
                                },
                                init = new Node
                                {
                                    type = NodeType.Identifier,
                                    range = (32, 35),
                                    name = "obj"
                                }
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({x = 0} = obj)", new Node
            {
                type = NodeType.Program,
                range = (0, 15),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        range = (0, 15),
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            range = (1, 14),
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.ObjectPattern,
                                range = (1, 8),
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        range = (2, 7),
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            range = (2, 3),
                                            name = "x"
                                        },
                                        kind = "init",
                                        value = new Node
                                        {
                                            type = NodeType.AssignmentPattern,
                                            range = (2, 7),
                                            left = new Node
                                            {
                                                type = NodeType.Identifier,
                                                range = (2, 3),
                                                name = "x"
                                            },
                                            right = new Node
                                            {
                                                type = NodeType.Literal,
                                                range = (6, 7),
                                                value = 0
                                            }
                                        }
                                    }
                                }
                            },
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                range = (11, 14),
                                name = "obj"
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("({x = 0}) => x", new Node
            {
                type = NodeType.Program,
                range = (0, 14),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        range = (0, 14),
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            range = (0, 14),
                            id = null,
                            generator = false,
                            bexpression = true,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ObjectPattern,
                                    range = (1, 8),
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            range = (2, 7),
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                range = (2, 3),
                                                name = "x"
                                            },
                                            kind = "init",
                                            value = new Node
                                            {
                                                type = NodeType.AssignmentPattern,
                                                range = (2, 7),
                                                left = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    range = (2, 3),
                                                    name = "x"
                                                },
                                                right = new Node
                                                {
                                                    type = NodeType.Literal,
                                                    range = (6, 7),
                                                    value = 0
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.Identifier,
                                range = (13, 14),
                                name = "x"
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("[a, {b: {c = 1}}] = arr", new Node
            {
                type = NodeType.Program,
                range = (0, 23),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        range = (0, 23),
                        expression = new Node
                        {
                            type = NodeType.AssignmentExpression,
                            range = (0, 23),
                            @operator = "=",
                            left = new Node
                            {
                                type = NodeType.ArrayPattern,
                                range = (0, 17),
                                elements = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        range = (1, 2),
                                        name = "a"
                                    },
                                    new Node
                                    {
                                        type = NodeType.ObjectPattern,
                                        range = (4, 16),
                                        properties = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Property,
                                                range = (5, 15),
                                                method = false,
                                                shorthand = false,
                                                computed = false,
                                                key = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    range = (5, 6),
                                                    name = "b"
                                                },
                                                value = new Node
                                                {
                                                    type = NodeType.ObjectPattern,
                                                    range = (8, 15),
                                                    properties = new List<Node>
                                                    {
                                                        new Node
                                                        {
                                                            type = NodeType.Property,
                                                            range = (9, 14),
                                                            method = false,
                                                            shorthand = true,
                                                            computed = false,
                                                            key = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                range = (9, 10),
                                                                name = "c"
                                                            },
                                                            kind = "init",
                                                            value = new Node
                                                            {
                                                                type = NodeType.AssignmentPattern,
                                                                range = (9, 14),
                                                                left = new Node
                                                                {
                                                                    type = NodeType.Identifier,
                                                                    range = (9, 10),
                                                                    name = "c"
                                                                },
                                                                right = new Node
                                                                {
                                                                    type = NodeType.Literal,
                                                                    range = (13, 14),
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
                            right = new Node
                            {
                                type = NodeType.Identifier,
                                range = (20, 23),
                                name = "arr"
                            }
                        }
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            Test("for ({x = 0} in arr);", new Node
            {
                type = NodeType.Program,
                range = (0, 21),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForInStatement,
                        range = (0, 21),
                        left = new Node
                        {
                            type = NodeType.ObjectPattern,
                            range = (5, 12),
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    range = (6, 11),
                                    method = false,
                                    shorthand = true,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        range = (6, 7),
                                        name = "x"
                                    },
                                    kind = "init",
                                    value = new Node
                                    {
                                        type = NodeType.AssignmentPattern,
                                        range = (6, 11),
                                        left = new Node
                                        {
                                            type = NodeType.Identifier,
                                            range = (6, 7),
                                            name = "x"
                                        },
                                        right = new Node
                                        {
                                            type = NodeType.Literal,
                                            range = (10, 11),
                                            value = 0
                                        }
                                    }
                                }
                            }
                        },
                        right = new Node
                        {
                            type = NodeType.Identifier,
                            range = (16, 19),
                            name = "arr"
                        },
                        fbody = new Node
                        {
                            type = NodeType.EmptyStatement,
                            range = (20, 21)
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

            Test("try {} catch ({message}) {}", new Node
            {
                type = NodeType.Program,
                range = (0, 27),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.TryStatement,
                        range = (0, 27),
                        block = new Node
                        {
                            type = NodeType.BlockStatement,
                            range = (4, 6),
                            body = new List<Node>()
                        },
                        handler = new Node
                        {
                            type = NodeType.CatchClause,
                            range = (7, 27),
                            param = new Node
                            {
                                type = NodeType.ObjectPattern,
                                range = (14, 23),
                                properties = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Property,
                                        range = (15, 22),
                                        method = false,
                                        shorthand = true,
                                        computed = false,
                                        key = new Node
                                        {
                                            type = NodeType.Identifier,
                                            range = (15, 22),
                                            name = "message"
                                        },
                                        kind = "init",
                                        value = new Node
                                        {
                                            type = NodeType.Identifier,
                                            range = (15, 22),
                                            name = "message"
                                        }
                                    }
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.BlockStatement,
                                range = (25, 27),
                                body = new List<Node>()
                            }
                        },
                        finalizer = null
                    }
                }
            }, new Options
            {
                ecmaVersion = 6
            });

            // https://github.com/ternjs/acorn/issues/192

            Test("class A { static() {} }", new Node
            {
                type = NodeType.Program,
                range = (0, 23),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        range = (0, 23),
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            range = (6, 7),
                            name = "A"
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            range = (8, 23),
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    range = (10, 21),
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        range = (10, 16),
                                        name = "static"
                                    },
                                    @static = false,
                                    kind = "method",
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        range = (16, 21),
                                        id = null,
                                        @params = new List<Node>(),
                                        generator = false,
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            range = (19, 21),
                                            body = new List<Node>()
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

            Test("for (const x of list) process(x);", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ForOfStatement,
                        left = new Node
                        {
                            type = NodeType.VariableDeclaration,
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.VariableDeclarator,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        range = (11, 12)
                                    },
                                    init = null,
                                    range = (11, 12)
                                }
                            },
                            kind = "const",
                            range = (5, 12)
                        },
                        right = new Node
                        {
                            type = NodeType.Identifier,
                            name = "list",
                            range = (16, 20)
                        },
                        fbody = new Node
                        {
                            type = NodeType.ExpressionStatement,
                            expression = new Node
                            {
                                type = NodeType.CallExpression,
                                callee = new Node
                                {
                                    type = NodeType.Identifier,
                                    name = "process",
                                    range = (22, 29)
                                },
                                arguments = new List<Node>
                                {
                                    new Node
                                    {
                                        type = NodeType.Identifier,
                                        name = "x",
                                        range = (30, 31)
                                    }
                                },
                                range = (22, 32)
                            },
                            range = (22, 33)
                        },
                        range = (0, 33)
                    }
                },
                range = (0, 33)
            }, new Options {ecmaVersion = 6});

            Test("class A { *static() {} }", new Node
            {
                type = NodeType.Program,
                range = (0, 24),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        range = (0, 24),
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            range = (6, 7),
                            name = "A"
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            range = (8, 24),
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    range = (10, 22),
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        range = (11, 17),
                                        name = "static"
                                    },
                                    @static = false,
                                    kind = "method",
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        range = (17, 22),
                                        id = null,
                                        @params = new List<Node>(),
                                        generator = true,
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            range = (20, 22),
                                            body = new List<Node>()
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

            Test("`${/\\d/.exec('1')[0]}`", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 22,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        start = 0,
                        end = 22,
                        expression = new Node
                        {
                            type = NodeType.TemplateLiteral,
                            start = 0,
                            end = 22,
                            expressions = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MemberExpression,
                                    start = 3,
                                    end = 20,
                                    @object = new Node
                                    {
                                        type = NodeType.CallExpression,
                                        start = 3,
                                        end = 17,
                                        callee = new Node
                                        {
                                            type = NodeType.MemberExpression,
                                            start = 3,
                                            end = 12,
                                            @object = new Node
                                            {
                                                type = NodeType.Literal,
                                                start = 3,
                                                end = 7,
                                                regex = new RegexNode
                                                {
                                                    pattern = "\\d",
                                                    flags = ""
                                                },
                                                raw = "/\\d/"
                                            },
                                            property = new Node
                                            {
                                                type = NodeType.Identifier,
                                                start = 8,
                                                end = 12,
                                                name = "exec"
                                            },
                                            computed = false
                                        },
                                        arguments = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Literal,
                                                start = 13,
                                                end = 16,
                                                value = "1",
                                                raw = "'1'"
                                            }
                                        }
                                    },
                                    property = new Node
                                    {
                                        type = NodeType.Literal,
                                        start = 18,
                                        end = 19,
                                        value = 0,
                                        raw = "0"
                                    },
                                    computed = true
                                }
                            },
                            quasis = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.TemplateElement,
                                    start = 1,
                                    end = 1,
                                    value = new TemplateNode("", ""),
                                    tail = false
                                },
                                new Node
                                {
                                    type = NodeType.TemplateElement,
                                    start = 21,
                                    end = 21,
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

            Test("var _𐒦 = 10;", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 13,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        start = 0,
                        end = 13,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                start = 4,
                                end = 12,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    start = 4,
                                    end = 7,
                                    name = "_𐒦"
                                },
                                init = new Node
                                {
                                    type = NodeType.Literal,
                                    start = 10,
                                    end = 12,
                                    value = 10,
                                    raw = "10"
                                }
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("var 𫠝_ = 10;", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 13,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        start = 0,
                        end = 13,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                start = 4,
                                end = 12,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    start = 4,
                                    end = 7,
                                    name = "𫠝_"
                                },
                                init = new Node
                                {
                                    type = NodeType.Literal,
                                    start = 10,
                                    end = 12,
                                    value = 10,
                                    raw = "10"
                                }
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("var _\\u{104A6} = 10;", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 20,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        start = 0,
                        end = 20,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                start = 4,
                                end = 19,
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    start = 4,
                                    end = 14,
                                    name = "_𐒦"
                                },
                                init = new Node
                                {
                                    type = NodeType.Literal,
                                    start = 17,
                                    end = 19,
                                    value = 10,
                                    raw = "10"
                                }
                            }
                        },
                        kind = "var"
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("let [x,] = [1]", new Node
            {
                start = 0,
                body = new List<Node>
                {
                    new Node
                    {
                        start = 0,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                start = 4,
                                id = new Node
                                {
                                    start = 4,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            start = 5,
                                            name = "x",
                                            type = NodeType.Identifier,
                                            end = 6
                                        }
                                    },
                                    type = NodeType.ArrayPattern,
                                    end = 8
                                },
                                init = new Node
                                {
                                    start = 11,
                                    elements = new List<Node>
                                    {
                                        new Node
                                        {
                                            start = 12,
                                            value = 1,
                                            raw = "1",
                                            type = NodeType.Literal,
                                            end = 13
                                        }
                                    },
                                    type = NodeType.ArrayExpression,
                                    end = 14
                                },
                                type = NodeType.VariableDeclarator,
                                end = 14
                            }
                        },
                        kind = "let",
                        type = NodeType.VariableDeclaration,
                        end = 14
                    }
                },
                type = NodeType.Program,
                end = 14
            }, new Options {ecmaVersion = 6});

            Test("let {x} = y", new Node
            {
                start = 0,
                body = new List<Node>
                {
                    new Node
                    {
                        start = 0,
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                start = 4,
                                id = new Node
                                {
                                    start = 4,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            start = 5,
                                            method = false,
                                            shorthand = true,
                                            computed = false,
                                            key = new Node
                                            {
                                                start = 5,
                                                name = "x",
                                                type = NodeType.Identifier,
                                                end = 6
                                            },
                                            kind = "init",
                                            value = new Node
                                            {
                                                start = 5,
                                                name = "x",
                                                type = NodeType.Identifier,
                                                end = 6
                                            },
                                            type = NodeType.Property,
                                            end = 6
                                        }
                                    },
                                    type = NodeType.ObjectPattern,
                                    end = 7
                                },
                                init = new Node
                                {
                                    start = 10,
                                    name = "y",
                                    type = NodeType.Identifier,
                                    end = 11
                                },
                                type = NodeType.VariableDeclarator,
                                end = 11
                            }
                        },
                        kind = "let",
                        type = NodeType.VariableDeclaration,
                        end = 11
                    }
                },
                type = NodeType.Program,
                end = 11
            }, new Options {ecmaVersion = 6});

            Test("[x,,] = 1", new Node { }, new Options {ecmaVersion = 6});

            Test("for (var [name, value] in obj) {}", new Node
            {
                body = new List<Node>
                {
                    new Node
                    {
                        left = new Node
                        {
                            declarations = new List<Node>
                            {
                                new Node
                                {
                                    id = new Node
                                    {
                                        elements = new List<Node>
                                        {
                                            new Node
                                            {
                                                name = "name",
                                                type = NodeType.Identifier
                                            },
                                            new Node
                                            {
                                                name = "value",
                                                type = NodeType.Identifier
                                            }
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
                        right = new Node
                        {
                            name = "obj",
                            type = NodeType.Identifier
                        },
                        fbody = new Node
                        {
                            body = new List<Node>(),
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

            Test("function foo() { new.target; }", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "foo"
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.MetaProperty,
                                        meta = new Node {type = NodeType.Identifier, name = "new"},
                                        property = new Node {type = NodeType.Identifier, name = "target"}
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

            Test("export default function foo() {} false", new Node
            {
                body = new List<Node>
                {
                    new Node
                    {
                        declaration = new Node
                        {
                            id = new Node
                            {
                                name = "foo",
                                type = NodeType.Identifier
                            },
                            generator = false,
                            bexpression = false,
                            @params = new List<Node>(),
                            fbody = new Node
                            {
                                body = new List<Node>(),
                                type = NodeType.BlockStatement
                            },
                            type = NodeType.FunctionDeclaration
                        },
                        type = NodeType.ExportDefaultDeclaration
                    },
                    new Node
                    {
                        expression = new Node
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
            Test("({ ['__proto__']: 1, __proto__: 2 })", new Node { }, new Options {ecmaVersion = 6});
            Test("({ __proto__() { return 1 }, __proto__: 2 })", new Node { }, new Options {ecmaVersion = 6});
            Test("({ get __proto__() { return 1 }, __proto__: 2 })", new Node { }, new Options {ecmaVersion = 6});
            Test("({ __proto__, __proto__: 2 })", new Node { }, new Options {ecmaVersion = 6});

            Test("export default /foo/", new Node { }, new Options {ecmaVersion = 6, sourceType = "module"});

            Test("var await = 0", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 13,
                loc = new SourceLocation(new Position(1, 0), new Position(1, 13)),
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.VariableDeclaration,
                        start = 0,
                        end = 13,
                        loc = new SourceLocation(new Position(1, 0), new Position(1, 13)),
                        declarations = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.VariableDeclarator,
                                start = 4,
                                end = 13,
                                loc = new SourceLocation(new Position(1, 4), new Position(1, 13)),
                                id = new Node
                                {
                                    type = NodeType.Identifier,
                                    start = 4,
                                    end = 9,
                                    loc = new SourceLocation(new Position(1, 4), new Position(1, 9)),
                                    name = "await"
                                },
                                init = new Node
                                {
                                    type = NodeType.Literal,
                                    start = 12,
                                    end = 13,
                                    loc = new SourceLocation(new Position(1, 12), new Position(1, 13)),
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

            Test("/[a-z]/gimuy", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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

            Test("(([,]) => 0)", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.ArrowFunctionExpression,
                            @params = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ArrayPattern,
                                    elements = new List<Node> {null}
                                }
                            },
                            fbody = new Node
                            {
                                type = NodeType.Literal,
                                value = 0,
                                raw = "0"
                            }
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            // 'eval' and 'arguments' are not reserved word, but those can not be a BindingIdentifier.

            Test("function foo() { return {arguments} }", new Node
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
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            start = 9,
                            end = 12,
                            name = "foo"
                        },
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 15,
                            end = 37,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ReturnStatement,
                                    start = 17,
                                    end = 35,
                                    argument = new Node
                                    {
                                        type = NodeType.ObjectExpression,
                                        start = 24,
                                        end = 35,
                                        properties = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Property,
                                                start = 25,
                                                end = 34,
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    start = 25,
                                                    end = 34,
                                                    name = "arguments"
                                                },
                                                kind = "init",
                                                value = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    start = 25,
                                                    end = 34,
                                                    name = "arguments"
                                                }
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

            Test("function foo() { return {eval} }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 32,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 32,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            start = 9,
                            end = 12,
                            name = "foo"
                        },
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 15,
                            end = 32,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ReturnStatement,
                                    start = 17,
                                    end = 30,
                                    argument = new Node
                                    {
                                        type = NodeType.ObjectExpression,
                                        start = 24,
                                        end = 30,
                                        properties = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Property,
                                                start = 25,
                                                end = 29,
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    start = 25,
                                                    end = 29,
                                                    name = "eval"
                                                },
                                                kind = "init",
                                                value = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    start = 25,
                                                    end = 29,
                                                    name = "eval"
                                                }
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

            Test("function foo() { 'use strict'; return {arguments} }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 51,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 51,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            start = 9,
                            end = 12,
                            name = "foo"
                        },
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 15,
                            end = 51,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 17,
                                    end = 30,
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
                                        start = 17,
                                        end = 29,
                                        value = "use strict",
                                        raw = "'use strict'"
                                    }
                                },
                                new Node
                                {
                                    type = NodeType.ReturnStatement,
                                    start = 31,
                                    end = 49,
                                    argument = new Node
                                    {
                                        type = NodeType.ObjectExpression,
                                        start = 38,
                                        end = 49,
                                        properties = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Property,
                                                start = 39,
                                                end = 48,
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    start = 39,
                                                    end = 48,
                                                    name = "arguments"
                                                },
                                                kind = "init",
                                                value = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    start = 39,
                                                    end = 48,
                                                    name = "arguments"
                                                }
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

            Test("function foo() { 'use strict'; return {eval} }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 46,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 46,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            start = 9,
                            end = 12,
                            name = "foo"
                        },
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 15,
                            end = 46,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 17,
                                    end = 30,
                                    expression = new Node
                                    {
                                        type = NodeType.Literal,
                                        start = 17,
                                        end = 29,
                                        value = "use strict",
                                        raw = "'use strict'"
                                    }
                                },
                                new Node
                                {
                                    type = NodeType.ReturnStatement,
                                    start = 31,
                                    end = 44,
                                    argument = new Node
                                    {
                                        type = NodeType.ObjectExpression,
                                        start = 38,
                                        end = 44,
                                        properties = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Property,
                                                start = 39,
                                                end = 43,
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    start = 39,
                                                    end = 43,
                                                    name = "eval"
                                                },
                                                kind = "init",
                                                value = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    start = 39,
                                                    end = 43,
                                                    name = "eval"
                                                }
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

            Test("function foo() { return {yield} }", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 33,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 33,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            start = 9,
                            end = 12,
                            name = "foo"
                        },
                        generator = false,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 15,
                            end = 33,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ReturnStatement,
                                    start = 17,
                                    end = 31,
                                    argument = new Node
                                    {
                                        type = NodeType.ObjectExpression,
                                        start = 24,
                                        end = 31,
                                        properties = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.Property,
                                                start = 25,
                                                end = 30,
                                                method = false,
                                                shorthand = true,
                                                computed = false,
                                                key = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    start = 25,
                                                    end = 30,
                                                    name = "yield"
                                                },
                                                kind = "init",
                                                value = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    start = 25,
                                                    end = 30,
                                                    name = "yield"
                                                }
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
            Test("function* foo(a = function*(b) { yield b }) { }", new Node
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
                        end = 47,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            start = 10,
                            end = 13,
                            name = "foo"
                        },
                        generator = true,
                        bexpression = false,
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.AssignmentPattern,
                                start = 14,
                                end = 42,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    start = 14,
                                    end = 15,
                                    name = "a"
                                },
                                right = new Node
                                {
                                    type = NodeType.FunctionExpression,
                                    start = 18,
                                    end = 42,
                                    id = null,
                                    generator = true,
                                    bexpression = false,
                                    @params = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Identifier,
                                            start = 28,
                                            end = 29,
                                            name = "b"
                                        }
                                    },
                                    fbody = new Node
                                    {
                                        type = NodeType.BlockStatement,
                                        start = 31,
                                        end = 42,
                                        body = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.ExpressionStatement,
                                                start = 33,
                                                end = 40,
                                                expression = new Node
                                                {
                                                    type = NodeType.YieldExpression,
                                                    start = 33,
                                                    end = 40,
                                                    @delegate = false,
                                                    argument = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        start = 39,
                                                        end = 40,
                                                        name = "b"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 44,
                            end = 47,
                            body = new List<Node>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            // 'yield' as function names.

            Test("function* yield() {}", new Node
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
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            start = 10,
                            end = 15,
                            name = "yield"
                        },
                        generator = true,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 18,
                            end = 20,
                            body = new List<Node>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            Test("({*yield() {}})", new Node
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
                            type = NodeType.ObjectExpression,
                            start = 1,
                            end = 14,
                            properties = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.Property,
                                    start = 2,
                                    end = 13,
                                    method = true,
                                    shorthand = false,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        start = 3,
                                        end = 8,
                                        name = "yield"
                                    },
                                    kind = "init",
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        start = 8,
                                        end = 13,
                                        id = null,
                                        generator = true,
                                        bexpression = false,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            start = 11,
                                            end = 13,
                                            body = new List<Node>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            Test("class A {*yield() {}}", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 21,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ClassDeclaration,
                        start = 0,
                        end = 21,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            start = 6,
                            end = 7,
                            name = "A"
                        },
                        superClass = null,
                        fbody = new Node
                        {
                            type = NodeType.ClassBody,
                            start = 8,
                            end = 21,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.MethodDefinition,
                                    start = 9,
                                    end = 20,
                                    computed = false,
                                    key = new Node
                                    {
                                        type = NodeType.Identifier,
                                        start = 10,
                                        end = 15,
                                        name = "yield"
                                    },
                                    @static = false,
                                    kind = "method",
                                    value = new Node
                                    {
                                        type = NodeType.FunctionExpression,
                                        start = 15,
                                        end = 20,
                                        id = null,
                                        generator = true,
                                        bexpression = false,
                                        @params = new List<Node>(),
                                        fbody = new Node
                                        {
                                            type = NodeType.BlockStatement,
                                            start = 18,
                                            end = 20,
                                            body = new List<Node>()
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
            Test("function* wrap() {\n({*yield() {}})\n}", new Node { }, new Options {ecmaVersion = 6});
            Test("function* wrap() {\nclass A {*yield() {}}\n}", new Node { }, new Options {ecmaVersion = 6});

            // Forbid yield expressions in default parameters:
            testFail("function* foo(a = yield b) {}", "Yield expression cannot be a default value (1:18)", new Options {ecmaVersion = 6});
            testFail("(function* foo(a = yield b) {})", "Yield expression cannot be a default value (1:19)", new Options {ecmaVersion = 6});
            testFail("({*foo(a = yield b) {}})", "Yield expression cannot be a default value (1:11)", new Options {ecmaVersion = 6});
            testFail("(class {*foo(a = yield b) {}})", "Yield expression cannot be a default value (1:17)", new Options {ecmaVersion = 6});
            testFail("function* foo(a = class extends (yield b) {}) {}", "Yield expression cannot be a default value (1:33)", new Options {ecmaVersion = 6});

            // Allow yield expressions inside functions in default parameters:
            Test("function* foo(a = function* foo() { yield b }) {}", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 49,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 49,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            start = 10,
                            end = 13,
                            name = "foo"
                        },
                        generator = true,
                        bexpression = false,
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.AssignmentPattern,
                                start = 14,
                                end = 45,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    start = 14,
                                    end = 15,
                                    name = "a"
                                },
                                right = new Node
                                {
                                    type = NodeType.FunctionExpression,
                                    start = 18,
                                    end = 45,
                                    id = new Node
                                    {
                                        type = NodeType.Identifier,
                                        start = 28,
                                        end = 31,
                                        name = "foo"
                                    },
                                    generator = true,
                                    bexpression = false,
                                    @params = new List<Node>(),
                                    fbody = new Node
                                    {
                                        type = NodeType.BlockStatement,
                                        start = 34,
                                        end = 45,
                                        body = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.ExpressionStatement,
                                                start = 36,
                                                end = 43,
                                                expression = new Node
                                                {
                                                    type = NodeType.YieldExpression,
                                                    start = 36,
                                                    end = 43,
                                                    @delegate = false,
                                                    argument = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        start = 42,
                                                        end = 43,
                                                        name = "b"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 47,
                            end = 49,
                            body = new List<Node>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            Test("function* foo(a = {*bar() { yield b }}) {}", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 42,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 42,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            start = 10,
                            end = 13,
                            name = "foo"
                        },
                        generator = true,
                        bexpression = false,
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.AssignmentPattern,
                                start = 14,
                                end = 38,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    start = 14,
                                    end = 15,
                                    name = "a"
                                },
                                right = new Node
                                {
                                    type = NodeType.ObjectExpression,
                                    start = 18,
                                    end = 38,
                                    properties = new List<Node>
                                    {
                                        new Node
                                        {
                                            type = NodeType.Property,
                                            start = 19,
                                            end = 37,
                                            method = true,
                                            shorthand = false,
                                            computed = false,
                                            key = new Node
                                            {
                                                type = NodeType.Identifier,
                                                start = 20,
                                                end = 23,
                                                name = "bar"
                                            },
                                            kind = "init",
                                            value = new Node
                                            {
                                                type = NodeType.FunctionExpression,
                                                start = 23,
                                                end = 37,
                                                id = null,
                                                generator = true,
                                                bexpression = false,
                                                @params = new List<Node>(),
                                                fbody = new Node
                                                {
                                                    type = NodeType.BlockStatement,
                                                    start = 26,
                                                    end = 37,
                                                    body = new List<Node>
                                                    {
                                                        new Node
                                                        {
                                                            type = NodeType.ExpressionStatement,
                                                            start = 28,
                                                            end = 35,
                                                            expression = new Node
                                                            {
                                                                type = NodeType.YieldExpression,
                                                                start = 28,
                                                                end = 35,
                                                                @delegate = false,
                                                                argument = new Node
                                                                {
                                                                    type = NodeType.Identifier,
                                                                    start = 34,
                                                                    end = 35,
                                                                    name = "b"
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
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 40,
                            end = 42,
                            body = new List<Node>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            Test("function* foo(a = class {*bar() { yield b }}) {}", new Node
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
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            start = 10,
                            end = 13,
                            name = "foo"
                        },
                        generator = true,
                        bexpression = false,
                        @params = new List<Node>
                        {
                            new Node
                            {
                                type = NodeType.AssignmentPattern,
                                start = 14,
                                end = 44,
                                left = new Node
                                {
                                    type = NodeType.Identifier,
                                    start = 14,
                                    end = 15,
                                    name = "a"
                                },
                                right = new Node
                                {
                                    type = NodeType.ClassExpression,
                                    start = 18,
                                    end = 44,
                                    id = null,
                                    superClass = null,
                                    fbody = new Node
                                    {
                                        type = NodeType.ClassBody,
                                        start = 24,
                                        end = 44,
                                        body = new List<Node>
                                        {
                                            new Node
                                            {
                                                type = NodeType.MethodDefinition,
                                                start = 25,
                                                end = 43,
                                                computed = false,
                                                key = new Node
                                                {
                                                    type = NodeType.Identifier,
                                                    start = 26,
                                                    end = 29,
                                                    name = "bar"
                                                },
                                                @static = false,
                                                kind = "method",
                                                value = new Node
                                                {
                                                    type = NodeType.FunctionExpression,
                                                    start = 29,
                                                    end = 43,
                                                    id = null,
                                                    generator = true,
                                                    bexpression = false,
                                                    @params = new List<Node>(),
                                                    fbody = new Node
                                                    {
                                                        type = NodeType.BlockStatement,
                                                        start = 32,
                                                        end = 43,
                                                        body = new List<Node>
                                                        {
                                                            new Node
                                                            {
                                                                type = NodeType.ExpressionStatement,
                                                                start = 34,
                                                                end = 41,
                                                                expression = new Node
                                                                {
                                                                    type = NodeType.YieldExpression,
                                                                    start = 34,
                                                                    end = 41,
                                                                    @delegate = false,
                                                                    argument = new Node
                                                                    {
                                                                        type = NodeType.Identifier,
                                                                        start = 40,
                                                                        end = 41,
                                                                        name = "b"
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
                            }
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 46,
                            end = 48,
                            body = new List<Node>()
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            // Distinguish ParenthesizedExpression or ArrowFunctionExpression
            Test("function* wrap() {\n(a = yield b)\n}", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 34,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 34,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            start = 10,
                            end = 14,
                            name = "wrap"
                        },
                        generator = true,
                        bexpression = false,
                        @params = new List<Node>(),
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 17,
                            end = 34,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 19,
                                    end = 32,
                                    expression = new Node
                                    {
                                        type = NodeType.AssignmentExpression,
                                        start = 20,
                                        end = 31,
                                        @operator = "=",
                                        left = new Node
                                        {
                                            type = NodeType.Identifier,
                                            start = 20,
                                            end = 21,
                                            name = "a"
                                        },
                                        right = new Node
                                        {
                                            type = NodeType.YieldExpression,
                                            start = 24,
                                            end = 31,
                                            @delegate = false,
                                            argument = new Node
                                            {
                                                type = NodeType.Identifier,
                                                start = 30,
                                                end = 31,
                                                name = "b"
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

            testFail("function* wrap() {\n(a = yield b) => a\n}", "Yield expression cannot be a default value (2:5)", new Options {ecmaVersion = 6});

            Test("function* wrap() {\n({a = yield b} = obj)\n}", new Node
            {
                type = NodeType.Program,
                start = 0,
                end = 42,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        start = 0,
                        end = 42,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            start = 10,
                            end = 14,
                            name = "wrap"
                        },
                        @params = new List<Node>(),
                        generator = true,
                        bexpression = false,
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            start = 17,
                            end = 42,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    start = 19,
                                    end = 40,
                                    expression = new Node
                                    {
                                        type = NodeType.AssignmentExpression,
                                        start = 20,
                                        end = 39,
                                        @operator = "=",
                                        left = new Node
                                        {
                                            type = NodeType.ObjectPattern,
                                            start = 20,
                                            end = 33,
                                            properties = new List<Node>
                                            {
                                                new Node
                                                {
                                                    type = NodeType.Property,
                                                    start = 21,
                                                    end = 32,
                                                    method = false,
                                                    shorthand = true,
                                                    computed = false,
                                                    key = new Node
                                                    {
                                                        type = NodeType.Identifier,
                                                        start = 21,
                                                        end = 22,
                                                        name = "a"
                                                    },
                                                    kind = "init",
                                                    value = new Node
                                                    {
                                                        type = NodeType.AssignmentPattern,
                                                        start = 21,
                                                        end = 32,
                                                        left = new Node
                                                        {
                                                            type = NodeType.Identifier,
                                                            start = 21,
                                                            end = 22,
                                                            name = "a"
                                                        },
                                                        right = new Node
                                                        {
                                                            type = NodeType.YieldExpression,
                                                            start = 25,
                                                            end = 32,
                                                            @delegate = false,
                                                            argument = new Node
                                                            {
                                                                type = NodeType.Identifier,
                                                                start = 31,
                                                                end = 32,
                                                                name = "b"
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        right = new Node
                                        {
                                            type = NodeType.Identifier,
                                            start = 36,
                                            end = 39,
                                            name = "obj"
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                sourceType = "script"
            }, new Options {ecmaVersion = 6});

            Test("export default class Foo {}++x", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExportDefaultDeclaration,
                        declaration = new Node
                        {
                            type = NodeType.ClassDeclaration,
                            id = new Node
                            {
                                type = NodeType.Identifier,
                                name = "Foo"
                            },
                            superClass = null,
                            fbody = new Node
                            {
                                type = NodeType.ClassBody,
                                body = new List<Node>()
                            }
                        }
                    },
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
                        {
                            type = NodeType.UpdateExpression,
                            @operator = "++",
                            prefix = true,
                            argument = new Node
                            {
                                type = NodeType.Identifier,
                                name = "x"
                            }
                        }
                    }
                },
                sourceType = "module"
            }, new Options {ecmaVersion = 6, sourceType = "module"});

            Test("function *f() { yield\n{}/1/g\n}", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.FunctionDeclaration,
                        id = new Node
                        {
                            type = NodeType.Identifier,
                            name = "f"
                        },
                        fbody = new Node
                        {
                            type = NodeType.BlockStatement,
                            body = new List<Node>
                            {
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
                                    {
                                        type = NodeType.YieldExpression,
                                        argument = null
                                    }
                                },
                                new Node
                                {
                                    type = NodeType.BlockStatement,
                                    body = new List<Node>()
                                },
                                new Node
                                {
                                    type = NodeType.ExpressionStatement,
                                    expression = new Node
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
                        }
                    }
                }
            }, new Options {ecmaVersion = 6});

            Test("class B extends A { foo(a = super.foo()) { return a }}", new Node { }, new Options {ecmaVersion = 6});

            testFail("function* wrap() {\n({a = yield b} = obj) => a\n}", "Yield expression cannot be a default value (2:6)", new Options {ecmaVersion = 6});

            // invalid syntax '*foo: 1'
            testFail("({*foo: 1})", "Unexpected token (1:6)", new Options {ecmaVersion = 6});

            Test("export { x as y } from './y.js';\nexport { x as z } from './z.js';",
                new Node { }, new Options {sourceType = "module", ecmaVersion = 6});

            Test("export { default as y } from './y.js';\nexport default 42;",
                new Node { }, new Options {sourceType = "module", ecmaVersion = 6});

            testFail("export { default} from './y.js';\nexport default 42;", "Duplicate export 'default' (2:7)", new Options {sourceType = "module", ecmaVersion = 6});

            testFail("foo: class X {}", "Invalid labeled declaration (1:5)", new Options {ecmaVersion = 6});

            testFail("'use strict'; bar: function x() {}", "Invalid labeled declaration (1:19)", new Options {ecmaVersion = 6});

            testFail("({x, y}) = {}", "Parenthesized pattern (1:0)", new Options {ecmaVersion = 6});

            Test("[x, (y), {z, u: (v)}] = foo", new Node { }, new Options {ecmaVersion = 6});

            Test("export default function(x) {};", new Node {body = new List<Node> {new Node { }, new Node { }}}, new Options {ecmaVersion = 6, sourceType = "module"});

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

            Test("var foo = 1; var foo = 1;", new Node { }, new Options {ecmaVersion = 6});

            Test("if (x) var foo = 1; var foo = 1;", new Node { }, new Options {ecmaVersion = 6});

            Test("function x() { var foo = 1; } let foo = 1;", new Node { }, new Options {ecmaVersion = 6});

            Test("function foo() { let foo = 1; }", new Node { }, new Options {ecmaVersion = 6});

            Test("var foo = 1; { let foo = 1; }", new Node { }, new Options {ecmaVersion = 6});

            Test("{ let foo = 1; { let foo = 2; } }", new Node { }, new Options {ecmaVersion = 6});

            Test("var foo; try {} catch (_) { let foo; }", new Node { }, new Options {ecmaVersion = 6});

            Test("let x = 1; function foo(x) {}", new Node { }, new Options {ecmaVersion = 6});

            Test("for (let i = 0;;); for (let i = 0;;);", new Node { }, new Options {ecmaVersion = 6});

            Test("for (const foo of bar); for (const foo of bar);", new Node { }, new Options {ecmaVersion = 6});

            Test("for (const foo in bar); for (const foo in bar);", new Node { }, new Options {ecmaVersion = 6});

            Test("for (let foo in bar) { let foo = 1; }", new Node { }, new Options {ecmaVersion = 6});

            Test("for (let foo of bar) { let foo = 1; }", new Node { }, new Options {ecmaVersion = 6});

            Test("class Foo { method(foo) {} method2() { let foo; } }", new Node { }, new Options {ecmaVersion = 6});

            Test("() => { let foo; }; foo => {}", new Node { }, new Options {ecmaVersion = 6});

            Test("() => { let foo; }; () => { let foo; }", new Node { }, new Options {ecmaVersion = 6});

            Test("switch(x) { case 1: let foo = 1; } let foo = 1;", new Node { }, new Options {ecmaVersion = 6});

            Test("'use strict'; function foo() { let foo = 1; }", new Node { }, new Options {ecmaVersion = 6});

            Test("let foo = 1; function x() { var foo = 1; }", new Node { }, new Options {ecmaVersion = 6});

            Test("[...foo, bar = 1]", new Node { }, new Options {ecmaVersion = 6});

            Test("for (var a of /b/) {}", new Node { }, new Options {ecmaVersion = 6});

            Test("for (var {a} of /b/) {}", new Node { }, new Options {ecmaVersion = 6});

            Test("for (let {a} of /b/) {}", new Node { }, new Options {ecmaVersion = 6});

            Test("function* bar() { yield /re/ }", new Node { }, new Options {ecmaVersion = 6});

            Test("function* bar() { yield class {} }", new Node { }, new Options {ecmaVersion = 6});

            Test("() => {}\n/re/", new Node { }, new Options {ecmaVersion = 6});

            Test("(() => {}) + 2", new Node { }, new Options {ecmaVersion = 6});

            testFail("(x) => {} + 2", "Unexpected token (1:10)", new Options {ecmaVersion = 6});

            Test("function *f1() { function g() { return yield / 1 } }", new Node { }, new Options {ecmaVersion = 6});

            Test("class Foo {} /regexp/", new Node { }, new Options {ecmaVersion = 6});

            Test("(class Foo {} / 2)", new Node { }, new Options {ecmaVersion = 6});

            Test("1 <!--b", new Node
            {
                type = NodeType.Program,
                body = new List<Node>
                {
                    new Node
                    {
                        type = NodeType.ExpressionStatement,
                        expression = new Node
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
            Test("({super: 1})", new Node { }, new Options {ecmaVersion = 6});
            Test("import {super as a} from 'a'", new Node { }, new Options {ecmaVersion = 6, sourceType = "module"});
            Test("export {a as super}", new Node { }, new Options {ecmaVersion = 6, sourceType = "module"});
        }
    }
}
