using System;
using AcornSharp.Nodes;

namespace AcornSharp.TestRunner
{
    internal sealed class TestCase
    {
        public string code;
        public TestNode ast;
        public TestOptions options;
        public string error;
        public Func<BaseNode, string> assert;
    }
}