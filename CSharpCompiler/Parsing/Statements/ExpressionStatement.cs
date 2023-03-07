using CSharpCompiler.Lexer;
using System.Collections.Generic;
using System.ComponentModel;

namespace CSharpCompiler.Parsing.Statements
{
    public class ExpressionStatement : StatementBase
    {
        [DisplayName("Expression")]
        public Expression Expression { set; get; }
        [DisplayName("EndSymbol")]
        public Token EndSymbol { set; get; }
        public ExpressionStatement(Expression expression, Token end)
        {
            Expression = expression;
            EndSymbol = end;
        }
        public ExpressionStatement(List<Token> tokens)
        {
            if (tokens[tokens.Count - 1].Type == TokenType.Semicolon)
            {
                EndSymbol = tokens[tokens.Count - 1];
                tokens.RemoveAt(tokens.Count - 1);
            }
            Expression = Expression.GetExpression(ref tokens);
        }

        public override string GetEndSymbol()
        {
            return EndSymbol.ToString();
        }
        public override string ToForString()
        {
            return Expression.ToForString()+EndSymbol.Value;
        }
        public override string ToString()
        {
            return "ExpressionStatement";
        }
    }
}
