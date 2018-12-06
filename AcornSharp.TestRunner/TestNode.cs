using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.TestRunner
{
    internal sealed class TestNode
    {
        public readonly Dictionary<string, object> values = new Dictionary<string, object>();

        [CanBeNull]
        public object type
        {
            get
            {
                values.TryGetValue("type", out var v);
                return v;
            }
            set => values.Add("type", value);
        }

        public object body { set => values.Add("body", value); }
        public object expression { set => values.Add("expression", value); }
        public TestNode loc { set => values.Add("location", value); }
        public object start { set => values.Add("start", value); }
        public int line { set => values.Add("line", value); }
        public int column { set => values.Add("column", value); }
        public object end { set => values.Add("end", value); }
        public TestNode block { set => values.Add("block", value); }
        public TestNode handler { set => values.Add("handler", value); }
        public TestNode param { set => values.Add("param", value); }
        public TestNode finaliser { set => values.Add("finaliser", value); }
        public SourceType sourceType { set => values.Add("sourceType", value); }
        public TestNode id { set => values.Add("id", value); }
        public string name { set => values.Add("name", value); }
        public TestNode[] @params { set => values.Add("parameters", value); }
        public bool generator { set => values.Add("generator", value); }
        public bool async { set => values.Add("async", value); }
        public TestNode[] properties { set => values.Add("properties", value); }
        public bool method { set => values.Add("method", value); }
        public bool shorthand { set => values.Add("shorthand", value); }
        public bool computed { set => values.Add("computed", value); }
        public TestNode key { set => values.Add("key", value); }
        public PropertyKind kind { set => values.Add("kind", value); }
        public object value { set => values.Add("value", value); }
        public TestNode superClass { set => values.Add("superClass", value); }
        public bool @static { set => values.Add("static", value); }
        public TestNode declaration { set => values.Add("declaration", value); }
        public TestNode[] specifiers { set => values.Add("specifiers", value); }
        public object source { set => values.Add("source", value); }
        public TestNode callee { set => values.Add("callee", value); }
        public TestNode meta { set => values.Add("meta", value); }
        public TestNode imported { set => values.Add("imported", value); }
        public TestNode[] arguments { set => values.Add("arguments", value); }
        public TestNode argument { set => values.Add("argument", value); }
        public TestNode[] expressions { set => values.Add("expressions", value); }
        public TestNode[] quasis { set => values.Add("quasis", value); }
        public string raw { set => values.Add("raw", value); }
        public string cooked { set => values.Add("cooked", value); }
        public bool tail { set => values.Add("tail", value); }
        public TestNode tag { set => values.Add("tag", value); }
        public TestNode quasi { set => values.Add("quasi", value); }
        public string @operator { set => values.Add("operator", Parser.ConvertOperator(value)); }
        public TestNode left { set => values.Add("left", value); }
        public TestNode right { set => values.Add("right", value); }
        public TestNode[] declarations { set => values.Add("declarations", value); }
        public TestNode init { set => values.Add("init", value); }
        public TestNode[] elements { set => values.Add("elements", value); }
        public TestNode[] cases { set => values.Add("cases", value); }
        public TestNode @object { set => values.Add("object", value); }
        public TestNode property { set => values.Add("property", value); }
        public bool prefix { set => values.Add("prefix", value); }
        public bool @delegate { set => values.Add("delegate", value); }
        public object @await { set => values.Add("await", value); }
        public object consequent { set => values.Add("consequent", value); }
        public TestNode alternate { set => values.Add("alternate", value); }
        public string directive { set => values.Add("directive", value); }
        public string pattern { set => values.Add("pattern", value); }
        public string flags { set => values.Add("flags", value); }
        public TestNode discriminant { set => values.Add("discriminant", value); }
        public int[] range { set => values.Add("range", value); }
        public TestNode exported { set => values.Add("exported", value); }
        public TestNode local { set => values.Add("local", value); }
        public TestNode regex { set => values.Add("value", value); }
        public TestNode test { set => values.Add("test", value); }
        public TestNode update { set => values.Add("update", value); }
        public TestNode label { set => values.Add("label", value); }
    }
}