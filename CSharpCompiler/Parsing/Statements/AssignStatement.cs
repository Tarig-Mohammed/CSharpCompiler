using CSharpCompiler.Classes;
using CSharpCompiler.Lexer;
using System.Collections.Generic;
using System.ComponentModel;

namespace CSharpCompiler.Parsing.Statements
{
    public class AssignStmt : StatementBase
    {
        

        [DisplayName("EndSymbol")]
        public Token EndSymbol { set; get; }
        public override string GetEndSymbol()
        {
            if(EndSymbol!=null)
            return EndSymbol.Value;
            return "";
        }
    }
    public class IncrmtDecrmtByOneStmt : AssignStmt
    {
        [DisplayName("Operand")]
        public VarOperand Operand { private set; get; }
        [DisplayName("Assigne Symbol")]
        public Token AssignSymbol { private set; get; }
        bool isOperatorRight;
        public IncrmtDecrmtByOneStmt(Token left, Token right, Token end)
        {
            Token operand;
            if (left.Value == "++" || left.Value == "--")
            {
                isOperatorRight = false;
                operand = right;
                Operand = new VarOperand(right);
                AssignSymbol = left;
            }
            else
            {
                isOperatorRight = true;

                operand = left;
                Operand = new VarOperand(left);
                AssignSymbol = right;
            }
            if (operand.Type != TokenType.Identifier)
            {
                SharedData.ucErrors.AddError(left, $"can not using {AssignSymbol} symbol with  {operand.Type} \'{operand.Value}\'");
            }
            EndSymbol = end;

        }
        public override string ToForString()
        {
            return ToString();
        }
        public override string ToString()
        {
            return isOperatorRight ? Operand.Operand.Name + AssignSymbol : AssignSymbol + Operand.Operand.Name;

        }

    }
    public class AssignStatement : AssignStmt
    {
        [DisplayName("Left Operand")]
        public VarOperand LeftOperand { private set; get; }
        [DisplayName("Assigne Symbol")]
        public Token AssignSymbol { private set; get; }
        [DisplayName("Expression")]
        public Expression Expression { set; get; }
        public AssignStatement(Token left, Token assign, List<Token> tokens)
        {
            LeftOperand =new VarOperand(left);
            AssignSymbol = assign;
            if (left.Type != TokenType.Identifier)
            {
                SharedData.ucErrors.AddError(left, $"can not using assignment symbol with  {left.Type} \'{left.Value}\'");
            }
            if (tokens.Count > 0 && tokens[tokens.Count - 1].Type == TokenType.Semicolon)
            {
                EndSymbol = tokens[tokens.Count - 1];
                tokens.RemoveAt(tokens.Count - 1);
            }
            List<Token> newExp = new List<Token>(tokens.ToArray());
            if (assign.Type != TokenType.Equal && assign.Value.Length == 2)
            {
                string op = assign.Value.Substring(0, 1);
                Token top = Token.GetToken(op, assign.Location);

                newExp.Insert(0, top);
                newExp.Insert(0, left);
            }
            var exp = Expression.GetExpression(ref newExp);
            if (exp != null)
            {
                string value = exp.Evaluate();
                SharedData.SymbolTable.ChangeIdentifierValue(left, value);
            }

            Expression = Expression.GetExpression(ref tokens);
        }
        
        public override string ToString()
        {
            return "Assign Symbol";
        }
        public override string ToForString()
        {
            return LeftOperand.ToForString()+AssignSymbol+Expression.ToForString();
        }
    }
}
