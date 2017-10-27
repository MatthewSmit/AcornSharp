using System;

namespace AcornSharp
{
    public sealed class TokContext
    {
        public static readonly TokContext b_stat = new TokContext("{", false);
        public static readonly TokContext b_expr = new TokContext("{", true);
        public static readonly TokContext b_tmpl = new TokContext("${", false);
        public static readonly TokContext p_stat = new TokContext("(", false);
        public static readonly TokContext p_expr = new TokContext("(", true);
        public static readonly TokContext q_tmpl = new TokContext("`", true, true, p => p.tryReadTemplateToken());
        public static readonly TokContext f_stat = new TokContext("function", false);
        public static readonly TokContext f_expr = new TokContext("function", true);
        public static readonly TokContext f_expr_gen = new TokContext("function", true, false, null, true);
        public static readonly TokContext f_gen = new TokContext("function", false, false, null, true);

        public TokContext(string token, bool isExpr, bool preserveSpace = false, Action<Parser> @override = null, bool generator = false)
        {
            Token = token;
            IsExpression = isExpr;
            PreserveSpace = preserveSpace;
            Override = @override;
            Generator = generator;
        }

        public string Token { get; }
        public bool IsExpression { get; }
        public bool PreserveSpace { get; }
        public Action<Parser> Override { get; }
        public bool Generator { get; }
    }
}