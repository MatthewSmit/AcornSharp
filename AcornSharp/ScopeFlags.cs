using System;

namespace AcornSharp
{
    // Each scope gets a bitset that may contain these flags
    [Flags]
    internal enum ScopeFlags
    {
        Top = 1,
        Function = 2,
        Var = Top | Function,
        Async = 4,
        Generator = 8,
        Arrow = 16,
        SimpleCatch = 32,
        Super = 64,
        DirectSuper = 128
    }

    // Used in checkLVal and declareName to determine the type of a binding
    internal enum BindType
    {
        None = 0, // Not a binding
        Var = 1, // Var-style binding
        Lexical = 2, // Let- or const-style binding
        Function = 3, // Function declaration
        SimpleCatch = 4, // Simple (identifier pattern) catch binding
        Outside = 5 // Special case for function names as bound inside the function
    }
}