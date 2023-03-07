namespace CSharpCompiler.Lexer

{
    public enum TokenType
    {
        Empty,
        EndOfFile,
        Error,
        UnKnown,
        #region Comments

        LineComment,
        BlockComment,

        #endregion

        #region WhiteSpace

        WhiteSpace,
        NewLine,

        #endregion WhiteSpace

        #region Constants

        Integer,//15
        CharValue, //'T'
        StringValue,//"Tariq"
        Float,//1.45

        #endregion Constants


        Identifier,// x, ali
        Keyword,//int main if


        #region Groupings

        LeftBracket, // {
        RightBracket, // }
        RightBrace, // ]
        LeftBrace, // [
        LeftParenthesis, // (
        RightParenthesis, // )

        #endregion Groupings

        #region Operators

        GreaterThanOrEqual, // >=
        GreaterThan, // >

        LessThan, // <
        LessThanOrEqual, // <=

        PlusEqual, // +=
        PlusPlus, // ++
        Plus, // +

        MinusEqual, // -=
        MinusMinus, // --
        Minus, // -

        Assignment, // =

        Not, // !
        NotEqual, // !=

        Mul, // *
        MulEqual, // *=

        Div, // /
        DivEqual, // /=
        UnaryAnd,//&
        UnaryOr,//|
        BooleanAnd, // &&
        BooleanOr, // ||
        BooleanValue,//true , false
        ModEqual, // %=
        Mod, // %

        Equal, // ==
        EqualEqual,

        #endregion Operators

        #region Punctuation

        Dot, //.
        Comma, //,
        Semicolon, //;
        Colon, //:

        #endregion Punctuation

    }
}