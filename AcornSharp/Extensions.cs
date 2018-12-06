using JetBrains.Annotations;

namespace AcornSharp
{
    internal static class Extensions
    {
        public static int CharCodeAt([NotNull] this string str, int index)
        {
            return str.Length <= index ? -1 : str[index];
        }
    }
}
