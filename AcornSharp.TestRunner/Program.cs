using System;
using System.Collections.Generic;
using AcornSharp.Nodes;
using JetBrains.Annotations;

namespace AcornSharp.TestRunner
{
    internal static class Program
    {
        private static readonly IList<TestCase> tests = new List<TestCase>();

        private static void Main()
        {
            Tests.Run();
            TestsAsyncAwait.Run();
            TestsAsyncIteration.Run();
            TestsAwaitTopLevel.Run();
            TestsDirective.Run();
            TestsEs7.Run();
            TestsHarmony.Run();
            TestsJsonSuperset.Run();
            TestsOptionalCatchBinding.Run();
            TestsRegExp.Run();
            TestsRegExp2018.Run();
            TestsRestSpreadProperties.Run();
            TestsTemplateLiteralRevision.Run();
            TestsTrailingCommasInFunc.Run();

            RunTests(null, Acorn.Parse);
//            RunTests(test =>
//            {
//                var options = test.options;
//                if (options != null)
//                {
//                    return options.loose;
//                }
//
//                return true;
//            }, (code, options) =>
//            {
//                throw new NotImplementedException();
//            });
        }

        private static void RunTests(Func<TestCase, bool> filter, Func<string, Options, ProgramNode> parse)
        {
//            for (var i = 0; i < tests.Count; i++)
            for (var i = 0; i < tests.Count; i++)
            {
                var test = tests[i];
                if (filter != null && !filter(test))
                {
                    continue;
                }

                var testOptions = test.options;
                if (testOptions == null)
                {
                    testOptions = new TestOptions
                    {
                        locations = true
                    };
                }

                if (testOptions.ecmaVersion == 0)
                {
                    testOptions.ecmaVersion = 5;
                }

                var options = new Options
                {
                    AllowAwaitOutsideFunction = testOptions.allowAwaitOutsideFunction,
                    AllowHashBang = testOptions.allowHashBang,
                    AllowReserved = testOptions.allowReserved,
                    EcmaVersion = testOptions.ecmaVersion,
                    Locations = testOptions.locations,
                    PreserveParens = testOptions.preserveParens,
                    Ranges = testOptions.ranges,
                    SourceFile = testOptions.sourceFile,
                    SourceType = testOptions.sourceType,
                    AllowReturnOutsideFunction = testOptions.allowReturnOutsideFunction,
                };

                var expectedComments = new List<TestNode>();
                if (testOptions.onComment != null)
                {
                    options.OnCommentList = new List<CommentToken>();
                    expectedComments.AddRange(testOptions.onComment);
                }

                var expectedTokens = new List<TestNode>();
                if (testOptions.onToken != null)
                {
                    options.OnTokenList = new List<Token>();
                    expectedTokens.AddRange(testOptions.onToken);
                }

                if (test.error != null)
                {
                    try
                    {
                        parse(test.code, options);
                    }
                    catch (SyntaxException e)
                    {
                        if (test.error[0] == '~' ? e.Message.IndexOf(test.error.Substring(1), StringComparison.Ordinal) <= -1 : e.Message != test.error)
                        {
                            //          callback("fail", test.code, "Expected error message: " + test.error + "\nGot error message: " + e.message);
                            throw new NotImplementedException();
                        }

                        continue;
                    }

                    //      if (config.loose) callback("ok", test.code);
                    //      else callback("fail", test.code, "Expected error message: " + test.error + "\nBut parsing succeeded.");
                    throw new NotImplementedException();
                }
                else if (test.assert != null)
                {
                    //    try {
                    var ast = parse(test.code, options);
                    //    } catch(e) {
                    //      if (!(e instanceof SyntaxError)) { console.log(e.stack); throw e; }
                    //      if (test.error) {
                    //        if (test.error.charAt(0) == "~" ? e.message.indexOf(test.error.slice(1)) > -1 : e.message == test.error)
                    //          callback("ok", test.code);
                    //        else
                    //          callback("fail", test.code, "Expected error message: " + test.error + "\nGot error message: " + e.message);
                    //      } else {
                    //        callback("error", test.code, e.message || e.toString());
                    //      }
                    //      continue
                    //    }

                    //      var error = test.assert(ast);
                    //      if (error) callback("fail", test.code, "\n  Assertion failed:\n " + error);
                    //      else callback("ok", test.code);
                    throw new NotImplementedException();
                }
                else
                {
                    //    try {
                    var ast = parse(test.code, options);
                    //    } catch(e) {
                    //      if (!(e instanceof SyntaxError)) { console.log(e.stack); throw e; }
                    //      if (test.error) {
                    //        if (test.error.charAt(0) == "~" ? e.message.indexOf(test.error.slice(1)) > -1 : e.message == test.error)
                    //          callback("ok", test.code);
                    //        else
                    //          callback("fail", test.code, "Expected error message: " + test.error + "\nGot error message: " + e.message);
                    //      } else {
                    //        callback("error", test.code, e.message || e.toString());
                    //      }
                    //      continue
                    //    }

                    var mis = Mismatch(test.ast, ast);
                    for (var j = 0; j < expectedComments.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(mis))
                        {
                            break;
                        }

                        mis = Mismatch(expectedComments[j], options.OnCommentList[j]);
                    }

                    for (var j = 0; j < expectedTokens.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(mis))
                        {
                            break;
                        }

                        mis = Mismatch(expectedTokens[j], options.OnTokenList[j]);
                    }

                    if (!string.IsNullOrEmpty(mis))
                    {
                        //      if (mis) callback("fail", test.code, mis);
                        throw new NotImplementedException();
                    }
                }
            }
        }

        [CanBeNull]
        private static string Mismatch([NotNull] TestNode testAst, [NotNull] object ast)
        {
            var mismatched = "";
            Mismatch(testAst, ast, ref mismatched);
            return mismatched;
        }

        private static void Mismatch([NotNull] TestNode testAst, [NotNull] object ast, ref string mismatched)
        {
            foreach (var (name, value) in testAst.values)
            {
                if (name == "type" && ast is BaseNode)
                {
                    if ((Type)value != ast.GetType())
                    {
                        throw new NotImplementedException();
                    }
                }
                else
                {
                    var astValue = ast.GetType().GetProperty(char.ToUpperInvariant(name[0]) + name.Substring(1)).GetValue(ast);
                    VerifyValue(value, astValue, ref mismatched);
                }
            }
        }

        private static void VerifyValue([CanBeNull] object value, [CanBeNull] object astValue, ref string mismatched)
        {
            if (value == null)
            {
                if (astValue != null)
                {
                    throw new NotImplementedException();
                }
            }
            else if (astValue == null)
            {
                throw new NotImplementedException();
            }
            else if (value is TestNode testNode && (astValue is BaseNode || astValue is SourceLocation || astValue is Position || astValue is RegExpValue || astValue is TemplateValue))
            {
                Mismatch(testNode, astValue, ref mismatched);
            }
            else if (value is TestNode[] valueArray
                     && astValue is IReadOnlyList<BaseNode> astList)
            {
                if (valueArray.Length != astList.Count)
                {
                    throw new NotImplementedException();
                }

                for (var i = 0; i < astList.Count; i++)
                {
                    VerifyValue(valueArray[i], astList[i], ref mismatched);
                }
            }
            else if (value is int[] ints && astValue is ValueTuple<int, int> tuple)
            {
                if (ints.Length != 2)
                {
                    throw new NotImplementedException();
                }

                if (ints[0] != tuple.Item1 || ints[1] != tuple.Item2)
                {
                    throw new NotImplementedException();
                }
            }
            else if (value is int i && astValue is double d)
            {
                if (i != d)
                {
                    throw new NotImplementedException();
                }
            }
            else if (value.GetType() == astValue.GetType())
            {
                if (!Equals(value, astValue))
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static void test(string code, TestNode ast, [CanBeNull] TestOptions options = null)
        {
            tests.Add(new TestCase
            {
                code = code,
                ast = ast,
                options = options
            });
        }
        public static void testFail(string code, string message, [CanBeNull] TestOptions options = null)
        {
            tests.Add(new TestCase
            {
                code = code,
                error = message,
                options = options
            });
        }

        public static void testAssert(string code, Func<BaseNode, string> assert, [CanBeNull] TestOptions options = null)
        {
            tests.Add(new TestCase
            {
                code = code,
                assert = assert,
                options = options
            });
        }
    }
}
