using System.Collections.Generic;

namespace AcornSharp
{
    public sealed partial class Parser
    {
        private sealed class Scope
        {
            public readonly HashSet<string> var = new HashSet<string>();
            public readonly HashSet<string> lexical = new HashSet<string>();
            public readonly HashSet<string> childVar = new HashSet<string>();
            public readonly HashSet<string> parentLexical = new HashSet<string>();
        }

        // The functions in this module keep track of declared variables in the current scope in order to detect duplicate variable names.
        private void enterFunctionScope()
        {
            // var: a hash of var-declared names in the current lexical scope
            // lexical: a hash of lexically-declared names in the current lexical scope
            // childVar: a hash of var-declared names in all child lexical scopes of the current lexical scope (within the current function scope)
            // parentLexical: a hash of lexically-declared names in all parent lexical scopes of the current lexical scope (within the current function scope)
            scopeStack.Push(new Scope());
        }

        private void exitFunctionScope()
        {
            scopeStack.Pop();
        }

        private void enterLexicalScope()
        {
            var parentScope = scopeStack.Peek();
            var childScope = new Scope();

            scopeStack.Push(childScope);
            foreach (var str in parentScope.parentLexical)
                childScope.parentLexical.Add(str);
            foreach (var str in parentScope.lexical)
                childScope.parentLexical.Add(str);
        }

        private void exitLexicalScope()
        {
            var childScope = scopeStack.Pop();
            var parentScope = scopeStack.Peek();

            foreach (var str in childScope.childVar)
                parentScope.childVar.Add(str);
            foreach (var str in childScope.var)
                parentScope.childVar.Add(str);
        }

        /**
         * A name can be declared with `var` if there are no variables with the same name declared with `let`/`const`
         * in the current lexical scope or any of the parent lexical scopes in this function.
         */
        private bool canDeclareVarName(string name)
        {
            var currentScope = scopeStack.Peek();
            return !currentScope.lexical.Contains(name) && !currentScope.parentLexical.Contains(name);
        }

        /**
         * A name can be declared with `let`/`const` if there are no variables with the same name declared with `let`/`const`
         * in the current scope, and there are no variables with the same name declared with `var` in the current scope or in
         * any child lexical scopes in this function.
         */
        private bool canDeclareLexicalName(string name)
        {
            var currentScope = scopeStack.Peek();
            return !currentScope.lexical.Contains(name) && !currentScope.var.Contains(name) && !currentScope.childVar.Contains(name);
        }

        private void declareVarName(string name)
        {
            scopeStack.Peek().var.Add(name);
        }

        private void declareLexicalName(string name)
        {
            scopeStack.Peek().lexical.Add(name);
        }
    }
}
