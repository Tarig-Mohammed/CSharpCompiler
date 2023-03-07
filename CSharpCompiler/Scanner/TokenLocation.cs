namespace CSharpCompiler.Lexer
{
    public class TokenLocation
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public TokenLocation(int line, int column)
        {
            Line = line;
            Column = column;

        }
        public override string ToString()
        {
            return $"{Line}:{Column} ";
        }

    }
}
