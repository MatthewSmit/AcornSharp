using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public sealed class LiteralNode : ExpressionNode
    {
        /// <inheritdoc />
        internal LiteralNode([NotNull] Parser parser, int start, Position startLocation, object value, string raw)
            : base(parser, start, startLocation)
        {
            Value = value;
            Raw = raw;
        }

        public bool IsString => Value is string;

        public object Value { get; }
        public string Raw { get; }
    }
}
