using CSharpCompiler.Lexer;
using System.Collections.Generic;
using System.ComponentModel;

namespace CSharpCompiler.Parsing.Statements
{
    public class ForStatement : StatementBase
    {
        public ForHeade Heade { set; get; }
        public Body Body { set; get; }

        public ForStatement(ForHeade heade, Body body)
        {
            Heade = heade;
            Body = body;
        }

        public override string ToString()
        {
            return "For Loob";
        }
        public override string GetEndSymbol()
        {
            return ";";
        }
    }
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ForHeade
    {
        [DisplayName("KeyWord")]
        public Token KeyWord { set; get; }

        [DisplayName("OpenParenthesis")]
        public Token OpenPs { get; set; }

        [DisplayName("InitializeCounter")]

        public StatementBase[] InitializeCounter { set; get; }
        [DisplayName("Condition")]

        public StatementBase Condition { set; get; }
        [DisplayName("Increment Part")]
        public StatementBase[] IncrmntPart { set; get; } = null;
        [DisplayName("CloseParenthesis")]
        public Token ClosePs { get; set; }
        private string toString = string.Empty;

        public ForHeade(Token keywrd,Token opP, List<StatementBase> initstmt, StatementBase cond, List<StatementBase> incrmtPart, Token clsP)
        {
            KeyWord = keywrd;
            OpenPs = opP;

            InitializeCounter = initstmt.ToArray();
            Condition = cond;
            IncrmntPart = incrmtPart.ToArray();
            ClosePs = clsP;
        }

        public override string ToString()
        {
            string tostr = KeyWord.Value+OpenPs;
            foreach (var i in InitializeCounter)
            {
                tostr += i.ToForString();
                if (string.IsNullOrEmpty(i.GetEndSymbol()))
                    tostr += ",";
            }
            tostr += Condition.ToForString();

            foreach (var i in IncrmntPart)
            {
                tostr += i.ToForString();
                if (string.IsNullOrEmpty(i.GetEndSymbol()))
                    tostr += "";
            }
            if (tostr.EndsWith(","))
                tostr = tostr.Substring(tostr.Length - 1);
            tostr += ClosePs;
            return tostr;
        }

    }
}
