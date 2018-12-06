namespace AcornSharp
{
    internal sealed class DestructuringErrors
    {
        public int shorthandAssign = -1;
        public int trailingComma = -1;
        public int parenthesizedAssign = -1;
        public int parenthesizedBind = -1;
        public int doubleProto = -1;

        public void Reset()
        {
            shorthandAssign = -1;
            trailingComma = -1;
            parenthesizedAssign = -1;
            parenthesizedBind = -1;
            doubleProto = -1;
        }
    }
}
