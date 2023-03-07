using System.Windows.Forms;

namespace CSharpCompiler.Lexer
{
    public class Token
    {
        public TokenType Type { get; set; }

        public string Value { get; set; }

        public TokenLocation Location { get; set; }
        public int size { get; set; }
        public static Token GetToken(string value, TokenLocation location)
        {
            switch (value)
            {
                case "+":
                    return new Token(TokenType.Plus, value, location);
                case "-":
                    return new Token(TokenType.Minus, value, location);
                case "*":
                    return new Token(TokenType.Mul, value, location);
                case "/":
                    return new Token(TokenType.Div, value, location);
                case "%":
                    return new Token(TokenType.Mod, value, location);
                default:
                    return new Token(TokenType.UnKnown, value, location);
            }
        }
        public static bool operator >(Token left, Token right)
        {
            if (left == null)
                return false;
            else if (right == null)
                return true;
            int leftl = left.Location.Line;
            int leftc = left.Location.Column;
            int rightl = right.Location.Line;
            int rightc = right.Location.Column;
            return leftl > rightl || (leftl == rightl && leftc > rightc);
        }
        public static bool operator >=(Token left, Token right)
        {
            if (right == null)
                return true;
            if (left == null)
                return false;
            int leftl = left.Location.Line;
            int leftc = left.Location.Column;
            int rightl = right.Location.Line;
            int rightc = right.Location.Column;
            return leftl > rightl || (leftl == rightl && leftc >= rightc);

        }
        public static bool operator <=(Token left, Token right)
        {

            if (left == null)
                return true;
            if (right == null)
                return false;
            int leftl = left.Location.Line;
            int leftc = left.Location.Column;
            int rightl = right.Location.Line;
            int rightc = right.Location.Column;
            return leftl < rightl || (leftl == rightl && leftc <= rightc);
        }

        public static bool operator <(Token left, Token right)
        {

            if (left == null)
                return true;
            else if (right == null)
                return false;
            int leftl = left.Location.Line;
            int leftc = left.Location.Column;
            int rightl = right.Location.Line;
            int rightc = right.Location.Column;
            return leftl < rightl || (leftl == rightl && leftc < rightc);
        }


        public Token(TokenType type, string value, TokenLocation location)
        {
            Type = type;
            Value = value;
            Location = location;
        }
        public Token(TokenType type, string value, TokenLocation location, int size)
        {
            Type = type;
            Value = value;
            Location = location;
            this.size = size;

        }
        public override bool Equals(object obj)
        {
            if (obj is Token)
            {
                Token right = obj as Token;
                if (this == null && right == null)
                    return true;
                else if (this == null || right == null)
                    return false;
                int leftl = Location.Line;
                int leftc = Location.Column;
                int rightl = right.Location.Line;
                int rightc = right.Location.Column;
                if ("{}()".Contains(Value) && "{}()".Contains(right.Value))
                    return leftl == rightl && leftc == rightc;
                else
                    return leftl == rightl && leftc == rightc && Value == right.Value;

            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return Value;
        }
        public string[] ToArrayString()
        {
            return new string[] { Location.ToString(), Value, Type.ToString() };
        }
        public TreeNode ToNode()
        {
            TreeNode node = new TreeNode(Type.ToString());
            node.Nodes.Add(Location.ToString());
            node.Nodes.Add(Value);
            node.Nodes.Add(Type.ToString());
            return node;

        }
        public Token()
        {
        }
    }
}
