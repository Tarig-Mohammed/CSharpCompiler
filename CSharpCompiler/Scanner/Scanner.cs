using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CSharpCompiler.Lexer

{
    public class Scanner
    {

        public string SourceCode { get; set; }

        private string[] _keywords = { "byte", "bool", "int", "short", "long", "float", "double", "string", "char", "if", "while", "do", "for", "else", "switch", "case", "break", "default", "main", "double", "char", "long", "cin", "cout", "return", "continue" };
        private int _line, _column, _index;
        private char _ch;
        private StringBuilder _builder;

        public Scanner(string sourceCode)
        {
            SourceCode = sourceCode + '\0';

            _line = _column = 1;
            _index = 0;
            _builder = new StringBuilder();
            _ch = SourceCode[_index];
        }


        public List<Token> Scan()
        {
            var tokens = new List<Token>();
            while (!IsEOF())
            {
                Token token = GetToken();
                tokens.Add(token);
            }

            return tokens;
        }


        private Token GetToken()
        {
            if (IsNewLine())
            {
                return ScanNewLine();
            }
            else if (IsWhiteSpace())
            {
                return ScanWhiteSpace();
            }
            else if (IsDigit())
            {
                return ScanNumber();
            }
            else if (IsComment())
            {
                return ScanComment();
            }
            else if (char.IsLetter(_ch) || _ch == '_')
            {
                return ScanIdentifier();
            }
            else if (IsPunctuation())
            {
                return ScanPunctuation();
            }
            else if (_ch == '\"' || _ch == '\'')
            {
                return ScanStringValue();
            }
            else
            {
                return ScanWord();
            }
        }

        private Token ScanStringValue()
        {
            if (_ch == '\'')
            {
                Append();
                while (!IsEOF() && _ch != '\'')
                {
                    Append();
                }
                if (!IsEOF())
                {
                    Append();
                    if (_builder.Length < 4)
                        return CreateToken(TokenType.CharValue);
                }
            }
            else
            {
                Append();
                while (!IsEOF() && (_ch != '\"' || (_ch == '\"' && _builder.ToString().EndsWith("\\"))))
                {
                    Append();
                }
                if (!IsEOF())
                {
                    Append();
                    return CreateToken(TokenType.StringValue);
                }
            }
            return CreateToken(TokenType.Error);


        }

        private Token ScanPunctuation()
        {
            int count = 0;
            switch (_ch)
            {
                case '+':
                    Append();

                    if (_ch == '+')
                    {
                        Append();
                        return CreateToken(TokenType.PlusPlus);

                    }
                    else if (_ch == '=')
                    {
                        Append();
                        return CreateToken(TokenType.PlusEqual);
                    }

                    return CreateToken(TokenType.Plus);
                case '-':
                    Append();

                    if (_ch == '-')
                    {
                        Append();
                        return CreateToken(TokenType.MinusMinus);

                    }
                    else if (_ch == '=')
                    {
                        Append();
                        return CreateToken(TokenType.MinusEqual);
                    }
                    return CreateToken(TokenType.Minus);
                case '*':
                    Append();

                    if (_ch == '=')
                    {
                        Append();
                        return CreateToken(TokenType.MulEqual);

                    }
                    return CreateToken(TokenType.Mul);
                case '/':
                    Append();

                    if (_ch == '/')
                    {
                        Append();
                        return CreateToken(TokenType.LineComment);
                    }
                    else if (_ch == '*')
                    {
                        Append();
                        while (!IsEOF())
                        {
                            Append();
                            if (_builder.ToString().EndsWith("*/"))
                                break;
                        }
                        return CreateToken(TokenType.BlockComment);
                    }

                    if (_ch == '=')
                    {
                        Append();
                        return CreateToken(TokenType.DivEqual);
                    }
                    return CreateToken(TokenType.Div);
                case '%':
                    Append();
                    if (_ch == '=')
                    {
                        Append();
                        return CreateToken(TokenType.ModEqual);
                    }
                    return CreateToken(TokenType.Mod);
                case '&':
                    if (GetNext() == '&')
                    {
                        Append();
                        Append();
                        return CreateToken(TokenType.BooleanAnd);

                    }
                    Append();
                    return CreateToken(TokenType.UnaryAnd);
                case '|':
                    if (GetNext() == '|')
                    {
                        Append();
                        Append();
                        return CreateToken(TokenType.BooleanOr);

                    }
                    Append();
                    return CreateToken(TokenType.UnaryOr);
                case '>':
                    if (GetNext() == '=')
                    {
                        Append();
                        Append();
                        return CreateToken(TokenType.GreaterThanOrEqual);

                    }
                    Append();
                    return CreateToken(TokenType.GreaterThan);
                case '<':
                    if (GetNext() == '=')
                    {
                        Append();
                        Append();
                        return CreateToken(TokenType.LessThanOrEqual);

                    }
                    Append();
                    return CreateToken(TokenType.LessThan);
                case '=':
                    if (GetNext() == '=')
                    {
                        Append();
                        Append();
                        return CreateToken(TokenType.EqualEqual);

                    }
                    Append();
                    return CreateToken(TokenType.Equal);
                case '!':
                    if (GetNext() == '=')
                    {
                        Append();
                        Append();
                        return CreateToken(TokenType.NotEqual);

                    }
                    Append();
                    return CreateToken(TokenType.Not);
                case ';':
                    Append();
                    return CreateToken(TokenType.Semicolon);
                case '{':
                    Append();
                    return CreateToken(TokenType.LeftBracket);
                case '}':
                    Append();
                    return CreateToken(TokenType.RightBracket);
                case '[':
                    Append();
                    return CreateToken(TokenType.LeftBrace);
                case ']':
                    Append();
                    return CreateToken(TokenType.RightBrace);
                case '(':
                    Append();
                    return CreateToken(TokenType.LeftParenthesis);
                case ')':
                    Append();
                    return CreateToken(TokenType.RightParenthesis);
                case ',':
                    Append();
                    return CreateToken(TokenType.Comma);

                default:
                    Append();
                    return CreateToken(TokenType.UnKnown);


            }


        }

        private bool IsPunctuation()
        {
            return "<>{}()[]!%&*+-=/.,;:|".Contains(_ch);
        }

        private Token ScanIdentifier()
        {
            while (IsIdentifier())
            {
                Append();
            }
            if (IsKeyword())
            {
                return CreateToken(TokenType.Keyword);
            }
            else if (_builder.ToString() == "true" || _builder.ToString() == "false")
                return CreateToken(TokenType.BooleanValue);

            return CreateToken(TokenType.Identifier);
        }

        private bool IsKeyword()
        {
            return _keywords.Contains(_builder.ToString());
        }

        private bool IsIdentifier()
        {
            return _ch == '_' || char.IsLetterOrDigit(_ch);
        }

        private Token ScanComment()
        {
            while (!IsEOF() && !IsNewLine())
            {
                Append();
            }

            return CreateToken(TokenType.LineComment);
        }

        private bool IsComment()
        {
            return _ch == '/' && GetNext() == '/';
        }

        private char GetNext()
        {
            return SourceCode[_index + 1];
        }

        private Token ScanWord(string message = "Unexpected Token '{0}'")
        {
            while (!IsEOF() && !IsWhiteSpace())
            {
                Append();
            }
            return CreateToken(TokenType.Error);
        }

        private Token ScanNumber()
        {
            bool findPoint = false;
            while (IsDigit() || (_ch == '.' && !findPoint))
            {
                if (_ch == '.')
                    findPoint = true;
                Append();
            }

            if (IsIdentifier() || _ch == '.')
            {
                while (IsIdentifier() || _ch == '.')
                {
                    Append();
                }
                return CreateToken(TokenType.Error);
            }
            if (findPoint)
                return CreateToken(TokenType.Float);
            return CreateToken(TokenType.Integer);
        }

        private bool IsDigit()
        {
            //return "1234567890".Contains(_ch);
            return char.IsDigit(_ch);
        }

        private Token ScanWhiteSpace()
        {
            while (IsWhiteSpace())
            {
                Append();
            }
            return CreateToken(TokenType.WhiteSpace);
        }

        private bool IsWhiteSpace()
        {
            return char.IsWhiteSpace(_ch) && !IsNewLine();
        }

        private Token ScanNewLine()
        {
            Append();

            _line++;
            _column = 1;

            return CreateToken(TokenType.NewLine);
        }

        private Token CreateToken(TokenType type)
        {
            string value = _builder.ToString();
            var location = new TokenLocation(_line, _column - value.Length);

            _builder.Clear();

            return new Token(type, value, location);
        }

        private void Append()
        {
            _builder.Append(_ch);
            Advance();
        }
        private void Advance()
        {
            _index++;
            _column++;
            _ch = SourceCode[_index];
        }

        private bool IsNewLine()
        {
            return _ch == '\n';
        }

        private bool IsEOF()
        {
            return _ch == '\0';
        }
    }
}