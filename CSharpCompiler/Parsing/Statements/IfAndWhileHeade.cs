using CSharpCompiler.Classes;
using CSharpCompiler.Lexer;
using System.Collections.Generic;
using System.ComponentModel;

namespace CSharpCompiler.Parsing.Statements
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class IfWhileHeade
    {

        [DisplayName("KeyWord")]
        public string KeyWord { set; get; }

        [DisplayName("OpenParenthesis")]
        public string OpenPs { get; set; }

        [DisplayName("Condition")]
        public Expression ExpCondition { get; set; }

        [DisplayName("CloseParenthesis")]
        public string ClosePs { get; set; }
        private List<ErrorDetails> Errors = new List<ErrorDetails>();
        public IfWhileHeade() { }
        public IfWhileHeade(List<Token> heade)
        {

            KeyWord = heade[0].Value;
            heade.RemoveAt(0);
            if (heade.Count > 1 && heade[0].Type == TokenType.LeftParenthesis)
            {
                OpenPs = heade[0].Value;
                heade.RemoveAt(0);
                if (heade[heade.Count - 1].Type == TokenType.RightParenthesis)
                {
                    ClosePs = heade[heade.Count - 1].Value;
                    heade.RemoveAt(heade.Count - 1);
                }
                else
                {
                    SharedData.ucErrors.AddError(heade[heade.Count - 1], "expected \')\'");
                }
            }
            if (heade.Count > 0)
                ExpCondition = Expression.GetExpression(ref heade);
        }
        public override string ToString()
        {
            return KeyWord + OpenPs + ExpCondition + ClosePs;
        }
    }
}
