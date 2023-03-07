using CSharpCompiler.Parsing.Statements;
using System.ComponentModel;

namespace CSharpCompiler.Statements
{
    public class IfStmt : StatementBase
    {
        string Name { get; set; }

        [DisplayName("Heade")]
        public IfWhileHeade Heade { get; set; }

        [DisplayName("Body")]
        public Body Body { get; set; }
        [DisplayName("ElsePart")]
        public ElsePart ElsePart { get; set; }
        protected IfStmt() { }
        public IfStmt(IfWhileHeade heade, Body body)
        {

            Heade = heade;
            Body = body;

            ElsePart = null;
            Name = "UnMatchedIF";
        }
        public IfStmt(IfWhileHeade heade, Body body, ElsePart elsePart)
        {

            Heade = heade;
            Body = body;
            ElsePart = elsePart;
            Name = "MatchedIf_Else";

        }

        public override string ToString()
        {
            return Name;
        }

        public override string GetEndSymbol()
        {
            return ";";
        }

    }
    [TypeConverter(typeof(ExpandableObjectConverter))]

    public class ElsePart
    {
        [DisplayName("KeyWord")]

        public string KeyWord { get; set; }
        [DisplayName("Body")]

        public Body Body { get; set; }

        public ElsePart(Body body)
        {

            KeyWord = "else";
            Body = body;

        }
        public ElsePart()
        {
            KeyWord = null;
            Body = null;
        }

        public override string ToString()
        {
            return KeyWord + Body;
        }
    }
}
