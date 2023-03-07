using CSharpCompiler.Classes;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSharpCompiler
{
    public class RegularExp
    {

        static string startDeclaringVars = "(char|string|int|flaot|double|byte|bool)\\s+";
        static string declaringVar = startDeclaringVars + Identifier + $"\\s*(,\\s*{Identifier}\\s*)*;";
        static string startId = @"(\p{Ll}|\p{Lu}|_)";
        static string Identifier = $"{startId}\\w*";
        static Regex rgIdentifier;
        static Regex rgStringValue;
        static Regex rgCharValue;
        static Regex rgDigitValue;
        static Regex rgDeclaringVar;
        static Regex rgExpression;
        static Regex rgForHeader;
        static Regex rgFileName;


        static RegularExp()
        {
            rgFileName = new Regex("^" + @"\w*(\.cs)?$", RegexOptions.ECMAScript);
            rgIdentifier = new Regex("^" + Identifier + "$", RegexOptions.ECMAScript);
            rgStringValue = new Regex("^" + "\".*\"" + "$", RegexOptions.ECMAScript);
            rgCharValue = new Regex("^" + "\'.\'" + "$", RegexOptions.ECMAScript);
            rgDigitValue = new Regex("^" + @"[\+-]?\d+(\.\d+)?" + "$", RegexOptions.ECMAScript);
            string id = rgIdentifier.ToString().Remove(0, 1).Remove(rgIdentifier.ToString().Length - 2);
            id += "\\s*(=\\s*(" + id + "|\".*\"|\'.\'|[\\+-]?\\d+(\\.\\d+)?))?";
            rgDeclaringVar = new Regex("^" + startDeclaringVars + id + "\\s*(,\\s*" + id + @"\s*)*;$", RegexOptions.ECMAScript);
            id = id.Remove(id.Length - 1);
            string Operand = @"\s*(" + Identifier + ")|\".*\"|\'.\'|[\\+-]?\\d+(\\.\\d+)?\\s*";
            string Operator = @"\s*((&&)|(\|\|)|(!=)|>|(>=)|<|(<=)|(!=)|(==)|\-|\+|\*|\/)\s*";
            string exp = "((" + Operator + ")\\s*(" + Operand + "))*";//"(("+Identifier+ "|\".*\"|\'.\'|[\\+-]?\\d+(\\.\\d+)?)" + @"((&&|(\|\|)|!=|>|>=|<|<=|!=|==|\-|\+|\*|\/)"+ "(" + Identifier + "|\".*\"|\'.\'|[\\+-]?\\d+(\\.\\d+)?))*)?;";
            rgExpression = new Regex("^" + Operand + exp + ";$", RegexOptions.ECMAScript);

            rgForHeader = new Regex("^" + @"(for)\s*\(\s*(" + startDeclaringVars + id + ")?\\s*;\\s*(" + "((" + Operand + ")\\s*(" + Operator + "))*" + Operand + ");" + @".*\)$", RegexOptions.ECMAScript);

        }


        internal static bool IsAssignSymbol(string text)
        {
            bool r = Regex.IsMatch(text, @"^(\+|\-|\*|/|%)?=$", RegexOptions.ECMAScript);
            return text == "=" || ("+-/*%".Contains(text[0]) && text.EndsWith("="));
        }
        internal static bool IsAcceptForHeade(string text)
        {
            return rgForHeader.IsMatch(text);
        }
        internal static bool IsAcceptFileName(string text)
        {
            return rgFileName.IsMatch(text);
        }
        public static bool IsAcceptValue(string wrd)
        {
            if (string.IsNullOrEmpty(wrd))
                return true;
            return IsAcceptIdentifier(wrd) || IsStringValue(wrd) || IsCharValue(wrd) || IsDigitValue(wrd);
        }
        public static bool IsStringValue(string wrd)
        {
            if (!string.IsNullOrEmpty(wrd))
                return rgStringValue.IsMatch(wrd);
            return true;
        }
        public static bool IsCharValue(string wrd)
        {
            if (!string.IsNullOrEmpty(wrd))
                return rgCharValue.IsMatch(wrd);
            return true;
        }
        public static bool IsDigitValue(string wrd)
        {
            if (!string.IsNullOrEmpty(wrd))
                return rgDigitValue.IsMatch(wrd);
            return false;
        }
        public static bool IsAcceptIdentifier(string wrd)
        {

            return rgIdentifier.IsMatch(wrd) && !SharedData.IsKeyWord(wrd);
        }

        public static bool IsAcceptDeclaringVariables(string stmt)
        {
            return rgDeclaringVar.IsMatch(stmt);
        }

        internal static bool IsBooleanValue(string wrd)
        {
            return wrd == "true" || wrd == "false";
        }
    }
}
