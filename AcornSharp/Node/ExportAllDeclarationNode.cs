using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public sealed class ExportAllDeclarationNode : BaseNode
    {
        internal ExportAllDeclarationNode([NotNull] Parser parser, Position start, Position end, ExpressionNode source) :
            base(parser, start, end)
        {
            Source = source;
        }

        public ExpressionNode Source { get; }
    }
}