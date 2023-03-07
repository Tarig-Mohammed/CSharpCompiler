using CSharpCompiler.Classes;
using CSharpCompiler.Lexer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CSharpCompiler.Parsing.Statements
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Expression
    {
        public Expression() { }
        public static Expression GetExpression(string source)
        {
            List<Token> so = new List<Token>();
            foreach (var t in new Scanner(source).Scan())
            {
                if (t.Type == TokenType.WhiteSpace || t.Type == TokenType.NewLine)
                    continue;
                so.Add(t);
            }
            return GetExpression(ref so);
        }
        public static Expression GetExpression(ref List<Token> source)
        {
            if (source.Count > 0)
            {
                if (source[0].Type == TokenType.LeftParenthesis)
                {
                    List<Token> tokens = new List<Token>();
                    tokens.Add(source[0]);
                    source.RemoveAt(0);
                    if (source.Count == 0 || (source[0].Type != TokenType.Identifier && source[0].Type != TokenType.RightParenthesis && source[0].Type != TokenType.LeftParenthesis && source[0].Type != TokenType.Integer && source[0].Type != TokenType.Float && source[0].Type != TokenType.StringValue && source[0].Type != TokenType.BooleanValue && source[0].Type != TokenType.CharValue))
                    {
                        SharedData.ucErrors.AddError(tokens[0], "Invalied Expression");
                    }
                    else
                        tokens.AddRange(ScanTo(ref source));
                    ExpressionWithParenthesis ewp = new ExpressionWithParenthesis(tokens);
                    if (source.Count > 0)
                    {
                        Token op = source[0];

                        if (SharedData.IsOperator(op.Value))
                        {
                            source.RemoveAt(0);
                            return new BinaryExperssion(ewp, op, GetExpression(ref source));
                        }
                        else
                        {
                            SharedData.ucErrors.AddError(op, "Expression error");
                            return new BinaryExperssion(ewp, GetExpression(ref source));
                        }
                    }
                    else
                        return ewp;
                }
                else if ("!+-".Contains(source[0].Value))
                {
                    Token op = source[0];
                    source.RemoveAt(0);
                    return new ExpressionWithUnaryOperator(op, GetExpression(ref source));
                }
                //else if (source.Count == 2)
                //{
                //    if(source[0].Value=="++"||source[0].Value)
                //}
                else
                {
                    Token to = source[0];
                    source.RemoveAt(0);
                    if (source.Count == 0)
                        return GetOperand(to);
                    else
                    {
                        Token op = source[0];
                        if (SharedData.IsOperator(op.Value))
                        {
                            source.RemoveAt(0);
                            return new BinaryExperssion(GetOperand(to), op, GetExpression(ref source));
                        }
                        else
                        {
                            SharedData.ucErrors.AddError(op, "Expression error");
                            return new BinaryExperssion(GetOperand(to), GetExpression(ref source));
                        }
                    }
                }




            }
            return null;
        }

        public static Operand GetOperand(Token to)
        {

            if (to.Type == TokenType.Identifier)
                return new VarOperand(to);
            else
                return new ValueOperand(to);
        }
        protected static List<Token> ScanTo(ref List<Token> source)
        {
            List<Token> tokens = new List<Token>();
            while (source.Count > 0 && source[0].Type != TokenType.RightParenthesis)
            {
                if (source[0].Type == TokenType.LeftParenthesis)
                {
                    tokens.Add(source[0]);
                    source.RemoveAt(0);
                    tokens.AddRange(ScanTo(ref source));
                    continue;
                }
                tokens.Add(source[0]);
                source.RemoveAt(0);
            }

            if (source.Count > 0)
            {
                tokens.Add(source[0]);
                source.RemoveAt(0);
            }
            return tokens;
        }

        public virtual string Evaluate()
        {
            return null;
        }
        public virtual string ToForString()
        {
            return "";
        }
        protected static string EvaluateBinaryValue(string left, Token op, string right)
        {

            switch (op.Value)
            {
                case "+":
                    if (RegularExp.IsDigitValue(left) && RegularExp.IsDigitValue(right))
                    {
                        double l = Convert.ToDouble(left);
                        double r = Convert.ToDouble(right);
                        return (l + r).ToString();
                    }
                    if (RegularExp.IsStringValue(left) || RegularExp.IsStringValue(right) || RegularExp.IsCharValue(left) || RegularExp.IsCharValue(right))
                    {
                        if (RegularExp.IsDigitValue(left) || RegularExp.IsBooleanValue(left))
                            return "\"" + left + right.Remove(right.Length - 1, 1).Remove(0, 1) + "\"";
                        if (RegularExp.IsDigitValue(right) || RegularExp.IsBooleanValue(right))
                            return "\"" + left.Remove(left.Length - 1, 1).Remove(0, 1) + right + "\"";


                        return "\"" + left.Remove(left.Length - 1, 1).Remove(0, 1) + right.Remove(right.Length - 1, 1).Remove(0, 1) + "\"";
                    }
                    SharedData.ucErrors.AddError(op, $"Can not use operator \'{op.Value}\' with non digit or string operands");
                    return null;

                case "-":
                    if (RegularExp.IsDigitValue(left) && RegularExp.IsDigitValue(right))
                    {
                        double l = Convert.ToDouble(left);
                        double r = Convert.ToDouble(right);
                        return (l - r).ToString();
                    }
                    SharedData.ucErrors.AddError(op, $"Can not use operator \'{op.Value}\' with non digit operand");

                    return null;
                case "*":
                    if (RegularExp.IsDigitValue(left) && RegularExp.IsDigitValue(right))
                    {
                        double l = Convert.ToDouble(left);
                        double r = Convert.ToDouble(right);
                        return (l * r).ToString();
                    }
                    SharedData.ucErrors.AddError(op, $"Can not use operator \'{op.Value}\' with non digit operand");

                    return null;
                case "/":
                    if (RegularExp.IsDigitValue(left) && RegularExp.IsDigitValue(right))
                    {
                        double l = Convert.ToDouble(left);
                        double r = Convert.ToDouble(right);
                        return (l / r).ToString();
                    }
                    SharedData.ucErrors.AddError(op, $"Can not use operator \'{op.Value}\' with non digit operand");

                    return null;
                case "%":
                    if (RegularExp.IsDigitValue(left) && RegularExp.IsDigitValue(right))
                    {
                        int l = Convert.ToInt32(left);
                        int r = Convert.ToInt32(right);
                        return (l & r).ToString();
                    }
                    SharedData.ucErrors.AddError(op, $"Can not use operator \'{op.Value}\' with non digit operand");

                    return null;
                case "<":
                    if (RegularExp.IsDigitValue(left) && RegularExp.IsDigitValue(right))
                    {
                        double l = Convert.ToDouble(left);
                        double r = Convert.ToDouble(right);
                        return (l < r).ToString();
                    }
                    SharedData.ucErrors.AddError(op, $"Can not use operator \'{op.Value}\' with non digit operand");

                    return null;
                case "<=":
                    if (RegularExp.IsDigitValue(left) && RegularExp.IsDigitValue(right))
                    {
                        double l = Convert.ToDouble(left);
                        double r = Convert.ToDouble(right);
                        return (l <= r).ToString();
                    }

                    SharedData.ucErrors.AddError(op, $"Can not use operator \'{op.Value}\' with non digit operand");
                    return null;
                case ">":
                    if (RegularExp.IsDigitValue(left) && RegularExp.IsDigitValue(right))
                    {
                        double l = Convert.ToDouble(left);
                        double r = Convert.ToDouble(right);
                        return (l > r).ToString();
                    }
                    SharedData.ucErrors.AddError(op, $"Can not use operator \'{op.Value}\' with non digit operand");

                    return null;
                case ">=":
                    if (RegularExp.IsDigitValue(left) && RegularExp.IsDigitValue(right))
                    {
                        double l = Convert.ToDouble(left);
                        double r = Convert.ToDouble(right);
                        return (l >= r).ToString();
                    }
                    SharedData.ucErrors.AddError(op, $"Can not use operator \'{op.Value}\' with non digit operand");


                    return null;
                case "==":
                    if (RegularExp.IsDigitValue(left) && RegularExp.IsDigitValue(right))
                    {
                        double l = Convert.ToDouble(left);
                        double r = Convert.ToDouble(right);
                        return (l == r).ToString();
                    }
                    return (left == right).ToString();

                case "!=":
                    if (RegularExp.IsDigitValue(left) && RegularExp.IsDigitValue(right))
                    {
                        double l = Convert.ToDouble(left);
                        double r = Convert.ToDouble(right);
                        return (l != r).ToString();
                    }
                    return (left != right).ToString();
                case "&&":
                    if (RegularExp.IsBooleanValue(left) && RegularExp.IsBooleanValue(right))
                    {
                        bool l = Convert.ToBoolean(left);
                        bool r = Convert.ToBoolean(right);
                        return (l && r).ToString();
                    }
                    SharedData.ucErrors.AddError(op, $"Can not use operator \'{op.Value}\' with non bool operand or value");
                    return null;
                case "||":
                    if (RegularExp.IsBooleanValue(left) && RegularExp.IsBooleanValue(right))
                    {
                        bool l = Convert.ToBoolean(left);
                        bool r = Convert.ToBoolean(right);
                        return (l || r).ToString();
                    }
                    SharedData.ucErrors.AddError(op, $"Can not use operator \'{op.Value}\' with non bool operand or value");
                    return null;

                default:
                    SharedData.ucErrors.AddError(op, $"UnKnown operator \'{op.Value}\' has used");
                    return null;
            }
        }
        public static string EvaluateUnaryValue(Token op, Expression exp)
        {
            string expres = exp.Evaluate();
            switch (op.Value)
            {
                case "+":
                    if (RegularExp.IsDigitValue(expres))
                    {
                        return (+Convert.ToDouble(expres)).ToString();
                    }
                    SharedData.ucErrors.AddError(op, $"Can not use operator \'{op.Value}\' with value {expres}");
                    return null;
                case "-":
                    if (RegularExp.IsDigitValue(expres))
                    {
                        return (-Convert.ToDouble(expres)).ToString();
                    }
                    SharedData.ucErrors.AddError(op, $"Can not use operator \'{op.Value}\' with value {expres}");
                    return null;
                case "!":
                    if (RegularExp.IsBooleanValue(expres))
                    {
                        return (expres != "true").ToString();
                    }
                    SharedData.ucErrors.AddError(op, $"Can not use operator \'{op.Value}\' with value {expres}");
                    return null;
                default:
                    SharedData.ucErrors.AddError(op, $"UnKnown operator \'{op.Value}\' has used");
                    return null;
            }
        }
    }
    public class ExpressionWithParenthesis : Expression
    {

        [DisplayName("OpenParenthesis")]
        public string Open { get; set; }
        [DisplayName("Expression")]
        public Expression Expression { get; set; }
        [DisplayName("Close Parenthesis")]
        public string Close { get; set; }

        public ExpressionWithParenthesis(List<Token> tokens)
        {


            Open = tokens[0].Value;
            tokens.RemoveAt(0);
            if (tokens.Count > 0 && tokens[tokens.Count - 1].Type == TokenType.RightParenthesis)
            {
                Close = ")";
                tokens.RemoveAt(tokens.Count - 1);
            }
            else
            {
                SharedData.ucErrors.AddError(tokens[tokens.Count - 1], "Messed \')\'");
            }

            Expression = GetExpression(ref tokens);


        }
        public override string Evaluate()
        {
            return Expression.Evaluate();
        }
        public override string ToString()
        {
            return Open + " " + Expression.ToString() + " " + Close;
        }
        public override string ToForString()
        {
            return Open+Expression.ToForString()+Close;
        }
    }
    public class ExpressionWithUnaryOperator : Expression
    {
        [DisplayName("Operator")]
        public Token Operator { get; set; }

        [DisplayName("LeftOperand")]
        public Expression Expression { get; set; }
        public ExpressionWithUnaryOperator(Token oprtor, Expression expression)
        {
            Operator = oprtor;
            Expression = expression;
        }

        public override string Evaluate()
        {
            return Expression.EvaluateUnaryValue(Operator, Expression);
        }
        public override string ToForString()
        {
            return Operator.Value+Expression.ToForString();
        }
    }
    public class BinaryExperssion : Expression
    {
        [DisplayName("LeftOperand")]
        public Expression LeftOperand { get; set; }
        [DisplayName("Operator")]
        public Token Operator { get; set; }
        [DisplayName("RightOperand")]
        public Expression RightOperand { get; set; }


        public BinaryExperssion(Expression left, Token oprtor, Expression right)
        {
            LeftOperand = left;
            Operator = oprtor;
            RightOperand = right;

        }

        public BinaryExperssion(Expression left, Expression right)
        {
            LeftOperand = left;
            Operator = null;
            RightOperand = right;

        }

        public override string Evaluate()
        {
            if (LeftOperand is Operand && RightOperand is BinaryExperssion)
            {
                BinaryExperssion right = (BinaryExperssion)RightOperand;
                if (right.LeftOperand is Operand && right.RightOperand is Operand &&
                    (RegularExp.IsStringValue((right.RightOperand as Operand).GetOperandValue())
                    || "*/%".Contains(Operator.Value)))
                {
                    string leftres = Expression.EvaluateBinaryValue(LeftOperand.Evaluate(), Operator, right.LeftOperand.Evaluate());
                    return Expression.EvaluateBinaryValue(leftres, right.Operator, right.RightOperand.Evaluate());
                }
            }

            return Expression.EvaluateBinaryValue(LeftOperand.Evaluate(), Operator, RightOperand.Evaluate());
        }
        public override string ToForString()
        {
            return LeftOperand.ToForString()+Operator+RightOperand.ToForString();
        }
        public override string ToString()
        {
            return LeftOperand.ToString() + Operator + RightOperand.ToString();
        }
    }

    public class Operand : Expression
    {

        protected Operand(Token op)
        {
            if (this is VarOperand)
            {
                Field f = SharedData.SymbolTable.GetField(op);
                if (f == null)
                    SharedData.ucErrors.AddError(op, "no sush vaiable name or using variable not declared");
                else if (f.Value != null)
                {
                    string[] digitTypes = { "int", "float", "float", "double" };
                    if (digitTypes.Contains(f.Type) && !RegularExp.IsDigitValue(f.Value))
                        SharedData.ucErrors.AddError(op, "value not excepted");
                    else if (f.Type == "string" && !RegularExp.IsStringValue(f.Value))
                        SharedData.ucErrors.AddError(op, "value not excepted");
                    else if (f.Type == "char" && !RegularExp.IsCharValue(f.Value))
                        SharedData.ucErrors.AddError(op, "value not excepted");
                }

            }
            else if (op.Type != TokenType.BooleanValue && op.Type != TokenType.StringValue && op.Type != TokenType.CharValue && op.Type != TokenType.Integer && op.Type != TokenType.Float)
                SharedData.ucErrors.AddError(op, "unKnown Value");
        }
        public virtual string GetOperandValue()
        {
            return null;
        }

        public virtual string GetOperandType()
        {
            return null;
        }
        public virtual string GetOperandName()
        {
            return null;
        }

    }

    public class VarOperand : Operand
    {

        [DisplayName("Operand")]
        public Variable Operand { get; set; }
        [DisplayName("Operand Typ")]
        public string OperandType { get; set; }
        public VarOperand(Token op) : base(op)
        {
            Field fld = SharedData.SymbolTable.GetField(op);
            if (fld != null)
            {
                Operand = new Variable(op.Value, fld.Value);
                OperandType = fld.Type;

            }
            else
            {
                Operand = new Variable(op.Value);
                OperandType = null;
            }

        }
        public override string GetOperandType()
        {
            return OperandType;
        }
        public override string GetOperandName()
        {
            return Operand.Name;
        }
        public override string GetOperandValue()
        {
            if(Operand.Value!=null)
            return Operand.Value.ToString();
            return "";
        }
        public override string Evaluate()
        {
            if (Operand.Value is Expression && !(Operand.Value is Operand))
                return (Operand.Value as Expression).Evaluate();
            //else if (Operand.Value is Operand)
            //    return Operand.Value.ToString();
            if(Operand.Value!=null )
            return Operand.Value.ToString();
            return "";
        }
        public override string ToForString()
        {
            return Operand.Name;
        }
        public override string ToString()
        {
            return Operand.Name;
        }

    }

    public class ValueOperand : Operand
    {

        [DisplayName("Value")]
        public string OperandValue { get; set; }
        [DisplayName("value Typ")]
        public string OperandType { get; set; }
        public ValueOperand(Token op) : base(op)
        {
            OperandValue = op.Value;
            OperandType = op.Type.ToString();
        }

        public override string GetOperandValue()
        {
            return OperandValue;
        }
        public override string ToString()
        {
            return OperandValue;
        }
        public override string ToForString()
        {
            return OperandValue;
        }
        public override string GetOperandType()
        {
            return OperandType;
        }
        public override string Evaluate()
        {
            return OperandValue;
        }

    }
}
