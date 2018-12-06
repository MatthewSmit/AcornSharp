using JetBrains.Annotations;

namespace AcornSharp.Nodes
{
    public struct TemplateValue
    {
        [NotNull]
        public string Raw { get; set; }

        [CanBeNull]
        public string Cooked { get; set; }
    }
}
