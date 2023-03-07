using System.ComponentModel;

namespace CSharpCompiler.Classes
{
    public class ErrorDetails
    {
        [DisplayName("Error String")]
        public string ErrText { set; get; }
        [DisplayName("Line")]
        public int Line { set; get; }
        [DisplayName("Column")]
        public int Clmn { set; get; }
        [DisplayName("Detailes")]
        public string Detailes { set; get; }

        public ErrorDetails(string errText, int line, int clmn, string detailes)
        {
            ErrText = errText;
            Line = line;
            Clmn = clmn;
            Detailes = detailes;
        }
    }
}
