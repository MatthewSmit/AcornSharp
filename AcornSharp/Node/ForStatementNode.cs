using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ForStatementNode : BaseNode
    {
        internal ForStatementNode([NotNull] Parser parser, Position start, Position end, BaseNode init, ExpressionNode test, ExpressionNode update, BaseNode body) :
            base(parser, start, end)
        {
            Init = init;
            Test = test;
            Update = update;
            Body = body;
        }

        public BaseNode Init { get; }
        public ExpressionNode Test { get; }
        public ExpressionNode Update { get; }
        public BaseNode Body { get; }
    }
}