using CSharpCompiler.Modules;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Classes
{
    public enum MyOrientation { ForWard, BackWard }
    public enum DataTypes { Tbyte, Tbool, Tint, Tshort, Tlong, Tfloat, Tdouble, Tstring, Tchar, NT }
    public class SharedData
    {
        private static List<ErrorDetails> Errors = new List<ErrorDetails>();
        public static ucErrors ucErrors = ucEditor.ucErrors;
        public static SymbolTable SymbolTable { set; get; } = new SymbolTable();
        private static string[] DataTypes = { "byte", "bool", "int", "short", "long", "float", "double", "string", "char" };
        private static string[] LogicOperators = { "&&", "||", "!", "==", ">", ">=", "<", "<=", "!=" };
        private static string[] MathOperators = { "+", "-", "*", "/", "%" };
        private static string[] KeyWords = { "int", "float", "if", "while", "do", "for", "else", "switch", "case", "break", "default", "main", "double", "char", "long", "cin", "cout", "return", "continue" };

        public static bool IsKeyWord(string wrd)
        {
            return KeyWords.Contains(wrd);
        }
        public static bool IsDataType(string wrd)
        {
            return DataTypes.Contains(wrd);
        }
        public static bool IsLogicOperator(string op)
        {
            return LogicOperators.Contains(op);
        }
        public static bool IsMathOperator(string op)
        {
            return MathOperators.Contains(op);
        }
        public static bool IsIncrmtDecrmtOperator(string op)
        {
            return new string[]{ "++","--"}.Contains(op);
        }
        public static bool IsOperator(string op)
        {
            return MathOperators.Contains(op) || LogicOperators.Contains(op);
        }

        public static List<ErrorDetails> GetErrors()
        {
            return Errors;
        }

    }
}
