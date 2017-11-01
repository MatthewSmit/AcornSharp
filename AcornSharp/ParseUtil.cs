using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using AcornSharp.Node;
using JetBrains.Annotations;

namespace AcornSharp
{
    [SuppressMessage("ReSharper", "LocalVariableHidesMember")]
    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public sealed partial class Parser
    {
        private static readonly Regex literal = new Regex(@"^(?:'((?:\\.|[^'])*?)'|""((?:\\.|[^""])*?)""|;)");

        public bool strictDirective(int start)
        {
            while (true)
            {
                start += skipWhiteSpace.Match(input, start).Length;
                var match = literal.Match(input.Substring(start));
                if (!match.Success) return false;
                if (match.Groups[1].Value == "use strict")
                    return true;
                if (match.Groups[2].Value == "use strict")
                    return true;
                start += match.Groups[0].Length;
            }
        }

        // Predicate that tests whether the next token is of the given
        // type, and if yes, consumes it as a side effect.
        private bool eat(TokenType type)
        {
            if (this.type == type)
            {
                next();
                return true;
            }
            return false;
        }

        // Tests whether parsed token is a contextual keyword.
        private bool isContextual(string name)
        {
            return type == TokenType.name && (string)value == name;
        }

        // Consumes contextual keyword if possible.
        private bool eatContextual(string name)
        {
            return (string)value == name && eat(TokenType.name);
        }

        // Asserts that following token is given contextual keyword.
        private void expectContextual(string name)
        {
            if (!eatContextual(name))
            {
                raise(start, "Unexpected token");
            }
        }

        // Test whether a semicolon can be inserted at the current position.
        private bool canInsertSemicolon()
        {
            return type == TokenType.EOF ||
                   type == TokenType.braceR ||
                   lineBreak.IsMatch(input.Substring(lastTokEnd.Index, start.Index - lastTokEnd.Index));
        }

        private bool insertSemicolon()
        {
            if (canInsertSemicolon())
            {
                if (Options.onInsertedSemicolon != null)
                {
//                    options.onInsertedSemicolon(this.lastTokEnd, this.lastTokEndLoc);
                    throw new NotImplementedException();
                }
                return true;
            }
            return false;
        }

        // Consume a semicolon, or, failing that, see if we are allowed to
        // pretend that there is a semicolon at this position.
        private void semicolon()
        {
            if (!eat(TokenType.semi) && !insertSemicolon())
            {
                raise(start, "Unexpected token");
            }
        }

        private bool afterTrailingComma(TokenType tokType, bool notNext = false)
        {
            if (type == tokType)
            {
                if (Options.onTrailingComma != null)
                {
                    throw new NotImplementedException();
//          this.options.onTrailingComma(this.lastTokStart, this.lastTokStartLoc);
                }
                if (!notNext)
                    next();
                return true;
            }
            return false;
        }

        // Expect a token of a given type. If found, consume it, otherwise,
        // raise an unexpected token error.
        private void expect(TokenType type)
        {
            if (!eat(type))
            {
                raise(start, "Unexpected token");
            }
        }

        internal sealed class DestructuringErrors
        {
            public Position shorthandAssign;
            public Position trailingComma;
            public Position parenthesizedAssign;
            public Position parenthesizedBind;

            public void Reset()
            {
                shorthandAssign = default;
                trailingComma = default;
                parenthesizedAssign = default;
                parenthesizedBind = default;
            }
        }

        private static void checkPatternErrors([CanBeNull] DestructuringErrors refDestructuringErrors, bool isAssign)
        {
            if (refDestructuringErrors == null) return;
            if (refDestructuringErrors.trailingComma.Line > 0)
            {
                raiseRecoverable(refDestructuringErrors.trailingComma, "Comma is not permitted after the rest element");
            }
            var parens = isAssign ? refDestructuringErrors.parenthesizedAssign : refDestructuringErrors.parenthesizedBind;
            if (parens.Line > 0) raiseRecoverable(parens, "Parenthesized pattern");
        }

        private static bool checkExpressionErrors([CanBeNull] DestructuringErrors refDestructuringErrors, bool andThrow = false)
        {
            var pos = refDestructuringErrors?.shorthandAssign ?? default;
            if (!andThrow) return pos.Line > 0;
            if (pos.Line > 0) raise(pos, "Shorthand property assignments are valid only in destructuring patterns");
            return false;
        }

        private void checkYieldAwaitInDefaultParams()
        {
            if (yieldPos.Line > 0 && (awaitPos.Line == 0 || yieldPos.Index < awaitPos.Index))
                raise(yieldPos, "Yield expression cannot be a default value");
            if (awaitPos.Line > 0)
                raise(awaitPos, "Await expression cannot be a default value");
        }

        private static bool isSimpleAssignTarget(BaseNode expr)
        {
            if (expr is ParenthesisedExpressionNode)
                return isSimpleAssignTarget(expr.expression);
            return expr is IdentifierNode || expr is MemberExpressionNode;
        }
    }
}
