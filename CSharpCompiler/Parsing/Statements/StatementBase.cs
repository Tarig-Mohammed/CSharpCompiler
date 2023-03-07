using System.Collections.Generic;
using System.ComponentModel;
using CSharpCompiler.Lexer;
using CSharpCompiler.Classes;

namespace CSharpCompiler.Parsing.Statements
{
    public enum StatementType { DeclaringVars, If, IfElse, For, While }
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class StatementBase
    {
        public virtual string GetEndSymbol()
        {
            return "";
        }
        public virtual string ToForString()
        {
            return "";
        }
    }

    public class EmptyStatement : StatementBase
    {
        [DisplayName("Symbol")]
        public Token EndSymbol { set; get; }

        public EmptyStatement(Token to)
        {
            EndSymbol = to;
        }
        public override string GetEndSymbol()
        {
            return EndSymbol.Value;
        }

        public override string ToForString()
        {
            return EndSymbol.Value;
        }
        public override string ToString()
        {
            return "Empty Statement";


        }
    }
    public class OtherStatement : StatementBase
    {
        [DisplayName("Statement")]
        public List<Token> stmt { set; get; }

        [DisplayName("Statement Token")]
        public Token Token { set; get; }
        public OtherStatement(Token to)
        {
            Token = to;
        }

        public OtherStatement(List<Token> stmt)
        {
            SharedData.ucErrors.AddMultiErrors(stmt, "UnKnown Token Error");
            this.stmt = stmt;
        }

        public override string ToString()
        {
            if (Token.Type == TokenType.LeftBracket)
                return "Open Block";

            if (Token.Type == TokenType.RightBracket)
                return "Close Block";

            return "UnKnown Statement";


        }
    }
}
