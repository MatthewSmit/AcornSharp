using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class WithStatementNode : BaseNode
    {
        internal WithStatementNode([NotNull] Parser parser, Position start, Position end, ExpressionNode @object, BaseNode body) :
            base(parser, start, end)
        {
            Object = @object;
            Body = body;
        }

        public ExpressionNode Object { get; }
        public BaseNode Body { get; }
    }
}