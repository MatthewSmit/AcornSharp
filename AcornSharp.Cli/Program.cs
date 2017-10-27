using System;
using System.Collections.Generic;

namespace AcornSharp.Cli
{
    internal static partial class Program
    {
        private static Driver driver;

        private static void Main()
        {
            driver = new Driver();

            Tests();
            TestsHarmony();
            TestsES7();
            TestsAsyncAwait();
            TestsTrailingCommasInFunc();
            TestsTemplateLiteralRevision();
            TestsDirective();

            void Group(string name)
            {
                Console.WriteLine(name);
            }

            void GroupEnd()
            {
            }

            void Log(string title, string message)
            {
                Console.WriteLine(title + ": " + message);
            }

            var modes = new Dictionary<string, Mode>
            {
                {"Normal", new Mode(Acorn.Parse, false, null)}
            };

            void Report(Stats stats, string state, string code, string message)
            {
                if (state != "ok")
                {
                    ++stats.failed;
                    Log(code, message);
                }
                ++stats.testsRun;
            }

            Group("Errors");

            foreach (var (name, mode) in modes)
            {
                Group(name);
                mode.stats = new Stats();
                var t0 = DateTime.Now;
                driver.RunTests(mode, Report);
                mode.stats.duration = DateTime.Now - t0;
                GroupEnd();
            }

            GroupEnd();

            void outputStats(string name, Stats argStats)
            {
                Console.WriteLine(name + ": " + argStats.testsRun + " tests run in " + argStats.duration + "ms; " + (argStats.failed != 0 ? argStats.failed + " failures." : "all passed."));
            }

            var total = new Stats();

            Group("Stats");

            foreach (var (name, mode) in modes)
            {
                var stats = mode.stats;
                outputStats(name + " parser", stats);
                total.duration += stats.duration;
                total.failed += stats.failed;
                total.testsRun += stats.testsRun;
            }

            outputStats("Total", total);

            GroupEnd();
        }

        private static void Test(string code, Node ast, Options options = null)
        {
            driver.Test(code, ast, options);
        }

        private static void testFail(string code, string message, Options options = null)
        {
            driver.TestFail(code, message, options);
        }
    }

    internal class Mode
    {
        public Func<string, Options, Node> parse;
        public bool loose;
        public Func<Test, bool> filter;
        public Stats stats;

        public Mode(Func<string, Options, Node> parse, bool loose, Func<Test, bool> filter)
        {
            this.parse = parse;
            this.loose = loose;
            this.filter = filter;
        }
    }

    internal class Stats
    {
        public int testsRun;
        public int failed;
        public TimeSpan duration;
    }

    internal sealed class Test
    {
        public string code;
        public Node ast;
        public string message;
        public string error;
        public string assert;
        public Options options;
    }

    internal sealed class Driver
    {
        private readonly List<Test> tests = new List<Test>();

        public void Test(string code, Node ast, Options options)
        {
            tests.Add(new Test {code = code, ast = ast, options = options});
        }

        public void TestFail(string code, string message, Options options)
        {
            tests.Add(new Test { code = code, error = message, options = options });
        }

        //  exports.testAssert = function(code, assert, options) {
        //    tests.push({code: code, assert: assert, options: options});
        //  };
        //
        public void RunTests(Mode config, Action<Stats, string, string, string> callback)
        {
            var parse = config.parse;

            for (var i = 0; i < tests.Count; ++i)
            {
                var test = tests[i];
                if (config.filter != null && !config.filter(test))
                    continue;

                var testOpts = test.options ?? new Options();
                if (testOpts.ecmaVersion == 0) testOpts.ecmaVersion = 5;

                Node ast;
                if (test.error != null)
                {
                    try
                    {
                        ast = parse(test.code, testOpts);
                    }
                    catch (SyntaxError e)
                    {
                        if (test.error[0] == '~' ? e.Message.IndexOf(test.error.Substring(1), StringComparison.Ordinal) > -1 : e.Message == test.error)
                        {
                            callback(config.stats, "ok", test.code, null);
                        }
                        else
                        {
                            throw;
//                            callback("fail", test.code, "Expected error message: " + test.error + "\nGot error message: " + e.Message);
                        }
                        continue;
                    }
                }
                else
                {
                    ast = parse(test.code, testOpts);
                }

                //try {
//                var ast = parse(test.code, testOpts);
                //} catch(e) {
                //  if (!(e instanceof SyntaxError)) { console.log(e.stack); throw e; }
                //  if (test.error) {
                //    if (test.error.charAt(0) == "~" ? e.message.indexOf(test.error.slice(1)) > -1 : e.message == test.error)
                //      callback("ok", test.code);
                //    else
                //      callback("fail", test.code, "Expected error message: " + test.error + "\nGot error message: " + e.message);
                //  } else {
                //    callback("error", test.code, e.message || e.toString());
                //  }
                //  continue
                //}

                if (test.error != null)
                {
                    throw new NotImplementedException();
//                    if (config.loose) callback("ok", test.code, null);
//                    else callback("fail", test.code, "Expected error message: " + test.error + "\nBut parsing succeeded.");
                }
                else if (test.assert != null)
                {
                    throw new NotImplementedException();
                    //  var error = test.assert(ast);
                    //  if (error) callback("fail", test.code, "\n  Assertion failed:\n " + error);
                    //  else callback("ok", test.code);
                }
                else
                {
                    var mis = !test.ast.TestEquals(ast);
                    if (mis)
                        throw new NotImplementedException();
                    //  for (var name in expected) {
                    //    if (mis) break;
                    //    if (expected[name]) {
                    //      mis = misMatch(expected[name], testOpts[name]);
                    //      testOpts[name] = expected[name];
                    //    }
                    //  }
                    if (mis) callback(config.stats, "fail", test.code, null);
                    else callback(config.stats, "ok", test.code, null);
                }
            }
        }

        //  function ppJSON(v) { return v instanceof RegExp ? v.toString() : JSON.stringify(v, null, 2); }
        //  function addPath(str, pt) {
        //    if (str.charAt(str.length-1) == ")")
        //      return str.slice(0, str.length-1) + "/" + pt + ")";
        //    return str + " (" + pt + ")";
        //  }
        //
        //  public void MisMatch(object exp, object act) {
        //    if (!exp || !act || (typeof exp != "object") || (typeof act != "object")) {
        //      if (exp !== act && typeof exp != "function")
        //        return ppJSON(exp) + " !== " + ppJSON(act);
        //    } else if (exp instanceof RegExp || act instanceof RegExp) {
        //      var left = ppJSON(exp), right = ppJSON(act);
        //      if (left !== right) return left + " !== " + right;
        //    } else if (exp.splice) {
        //      if (!act.slice) return ppJSON(exp) + " != " + ppJSON(act);
        //      if (act.length != exp.length) return "array length mismatch " + exp.length + " != " + act.length;
        //      for (var i = 0; i < act.length; ++i) {
        //        var mis = misMatch(exp[i], act[i]);
        //        if (mis) return addPath(mis, i);
        //      }
        //    } else {
        //      for (var prop in exp) {
        //        var mis = misMatch(exp[prop], act[prop]);
        //        if (mis) return addPath(mis, prop);
        //      }
        //    }
        //  };
        //
        //  private void Mangle(object ast) {
        //    if (typeof ast != "object" || !ast) return;
        //    if (ast.slice) {
        //      for (var i = 0; i < ast.length; ++i) mangle(ast[i]);
        //    } else {
        //      var loc = ast.start && ast.end && {start: ast.start, end: ast.end};
        //      if (loc) { delete ast.start; delete ast.end; }
        //      for (var name in ast) if (ast.hasOwnProperty(name)) mangle(ast[name]);
        //      if (loc) ast.loc = loc;
        //    }
        //  }
        //
        //public void PrintTests() {
        //    var out = "";
        //    for (var i = 0; i < tests.length; ++i) {
        //      if (tests[i].error) continue;
        //      mangle(tests[i].ast);
        //      out += "test(" + JSON.stringify(tests[i].code) + ", " + JSON.stringify(tests[i].ast, null, 2) + ");\n\n";
        //    }
        //    document.body.innerHTML = "";
        //    document.body.appendChild(document.createElement("pre")).appendChild(document.createTextNode(out));
        //  };
    }
}
