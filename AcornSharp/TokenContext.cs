using System;
using System.Collections.Generic;

namespace AcornSharp
{
    public sealed partial class Parser
    {
        public static List<TokContext> initialContext()
        {
            return new List<TokContext> {TokContext.b_stat};
        }

        private bool braceIsBlock(TokenType prevType)
        {
            var parent = curContext();
            if (parent == TokContext.f_expr || parent == TokContext.f_stat)
                return true;
            if (prevType == TokenType.colon && (parent == TokContext.b_stat || parent == TokContext.b_expr))
                return !parent.IsExpression;

            // The check for `tt.name && exprAllowed` detects whether we are
            // after a `yield` or `of` construct. See the `updateContext` for
            // `tt.name`.
            if (prevType == TokenType._return || prevType == TokenType.name && exprAllowed)
                return lineBreak.IsMatch(input.Substring(lastTokEnd.Index, start.Index - lastTokEnd.Index));
            if (prevType == TokenType._else || prevType == TokenType.semi || prevType == TokenType.eof || prevType == TokenType.parenR || prevType == TokenType.arrow)
                return true;
            if (prevType == TokenType.braceL)
                return parent == TokContext.b_stat;
            if (prevType == TokenType._var || prevType == TokenType.name)
                return false;
            return !exprAllowed;
        }

        private bool inGeneratorContext()
        {
            for (var i = context.Count - 1; i >= 1; i--)
            {
                if (context[i].Token == "function")
                    return context[i].Generator;
            }
            return false;
        }

        private void updateContext(TokenType prevType)
        {
            Action<Parser, TokenType> update;
            if (TokenInformation.Types[type].Keyword != null && prevType == TokenType.dot)
                exprAllowed = false;
            else if ((update = TokenInformation.Types[type].UpdateContext) != null)
                update(this, prevType);
            else
                exprAllowed = TokenInformation.Types[type].BeforeExpression;
        }

        internal static void ParenBraceRUpdateContext(Parser parser, TokenType _)
        {
            if (parser.context.Count == 1)
            {
                parser.exprAllowed = true;
                return;
            }
            var @out = parser.context.Pop();
            if (@out == TokContext.b_stat && parser.curContext().Token == "function")
            {
                @out = parser.context.Pop();
            }
            parser.exprAllowed = !@out.IsExpression;
        }

        internal static void BraceLUpdateContext(Parser parser, TokenType prevType)
        {
            parser.context.Add(parser.braceIsBlock(prevType) ? TokContext.b_stat : TokContext.b_expr);
            parser.exprAllowed = true;
        }

        internal static void DollarBraceLUpdateContext(Parser parser, TokenType prevType)
        {
            parser.context.Add(TokContext.b_tmpl);
            parser.exprAllowed = true;
        }

        internal static void ParenLUpdateContext(Parser parser, TokenType prevType)
        {
            var statementParens = prevType == TokenType._if || prevType == TokenType._for || prevType == TokenType._with || prevType == TokenType._while;
            parser.context.Add(statementParens ? TokContext.p_stat : TokContext.p_expr);
            parser.exprAllowed = true;
        }

        internal static void IncDecUpdateContext(Parser parser, TokenType prevType)
        {
            // tokExprAllowed stays unchanged
        }

        internal static void FunctionClassUpdateContext(Parser parser, TokenType prevType)
        {
            if (TokenInformation.Types[prevType].BeforeExpression && prevType != TokenType.semi && prevType != TokenType._else &&
                !((prevType == TokenType.colon || prevType == TokenType.braceL) && parser.curContext() == TokContext.b_stat))
                parser.context.Add(TokContext.f_expr);
            else
                parser.context.Add(TokContext.f_stat);
            parser.exprAllowed = false;
        }

        internal static void BackQuoteUpdateContext(Parser parser, TokenType prevType)
        {
            if (parser.curContext() == TokContext.q_tmpl)
                parser.context.Pop();
            else
                parser.context.Add(TokContext.q_tmpl);
            parser.exprAllowed = false;
        }

        internal static void StarUpdateContext(Parser parser, TokenType prevType)
        {
            if (prevType == TokenType._function)
            {
                var index = parser.context.Count - 1;
                if (parser.context[index] == TokContext.f_expr)
                    parser.context[index] = TokContext.f_expr_gen;
                else
                    parser.context[index] = TokContext.f_gen;
            }
            parser.exprAllowed = true;
        }

        internal static void NameUpdateContext(Parser parser, TokenType prevType)
        {
            var allowed = false;
            if (parser.Options.ecmaVersion >= 6)
            {
                if ("of".Equals(parser.value) && !parser.exprAllowed ||
                    "yield".Equals(parser.value) && parser.inGeneratorContext())
                    allowed = true;
            }
            parser.exprAllowed = allowed;
        }
    }
}
