using CSharpCompiler.Lexer;
using System.Collections.Generic;
using System.ComponentModel;

namespace CSharpCompiler.Parsing.Statements
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Body
    {

    }
    public class SingleStatementBody : Body
    {

        [DisplayName("Statement")]
        public StatementBase Statement { set; get; }
        public SingleStatementBody(List<Token> stmt)
        {
            Parser parser = new Parser();
            Statement = parser.RunTimeCheck(stmt)[0];
        }

        public SingleStatementBody(StatementBase body)
        {
            Statement = body;
        }

        public override string ToString()
        {
            return "Single Statement";
        }
    }
    public class BlockBody : Body
    {
        [DisplayName("OpenBody")]

        public Token OpenBody { get; set; }

        [DisplayName("Statements")]
        public StatementBase[] Statements { set; get; }

        [DisplayName("CloseBody")]
        public Token CloseBody { get; set; }

        public BlockBody(List<Token> body)
        {
            if (body[0].Type == TokenType.LeftBracket)
            {
                OpenBody = body[0];
                body.RemoveAt(0);
            }

            if (body[body.Count - 1].Type == TokenType.RightBracket)
            {
                CloseBody = body[body.Count - 1];
                body.RemoveAt(body.Count - 1);
            }

            Parser parser = new Parser();
            Statements = parser.RunTimeCheck(body).ToArray();
        }
        public BlockBody(Token open, List<StatementBase> body, Token close)
        {
            OpenBody = open;
            Statements = body.ToArray();
            CloseBody = close;
        }
        public override string ToString()
        {
            return OpenBody + "  Block Statement..." + CloseBody;
        }
    }
}
