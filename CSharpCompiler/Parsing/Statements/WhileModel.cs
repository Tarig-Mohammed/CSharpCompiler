using System.ComponentModel;

namespace CSharpCompiler.Parsing.Statements
{
    public class WhileStatement : StatementBase
    {
        [DisplayName("Heade")]
        public IfWhileHeade Heade { get; set; }

        [DisplayName("Body")]
        public Body Body { get; set; }



        public WhileStatement(IfWhileHeade heade, Body body)
        {
            Heade = heade;
            Body = body;
        }

        public override string ToString()
        {
            return "While Loop";
        }

        public override string GetEndSymbol()
        {
            return ";";
        }
    }
}
