using CSharpCompiler.Classes;
using CSharpCompiler.Lexer;
using System.Collections.Generic;
using System.ComponentModel;

namespace CSharpCompiler.Parsing.Statements
{
    public class DeclaringVarsStatement : StatementBase
    {
        [DisplayName("Type")]
        public string DataType { private set; get; }
        [DisplayName("Variables")]
        public Variable[] Variables { private set; get; }
        [DisplayName("EndSymbol")]
        public string EndSymbol { private set; get; }

        public DeclaringVarsStatement(List<Token> stmt)
        {
            string st = stmt[0].Value + " ";
            for (int i = 1; i < stmt.Count; i++)
            {
                st += stmt[i].Value;
            }
            DataType = stmt[0].Value;
            stmt.RemoveAt(0);

            if (stmt[stmt.Count - 1].Value == ";")
            {
                EndSymbol = stmt[stmt.Count - 1].Value;
                stmt.RemoveAt(stmt.Count - 1);
            }
            else
            {
                SharedData.ucErrors.AddError(stmt[stmt.Count - 1], "Missed \';\'");
            }

            AssigneVariables(stmt);
        }



        private void AssigneVariables(List<Token> stmt)
        {

            List<Variable> Vars = new List<Variable>();
            while (stmt.Count > 0)
            {
                if (stmt[0].Type == TokenType.Identifier)
                {
                    Token varr = stmt[0];
                    stmt.RemoveAt(0);
                    string value = null;
                    if (stmt.Count > 0)
                    {
                        if (stmt[0].Type == TokenType.Equal)
                        {
                            if (stmt.Count > 0)
                            {
                                stmt.RemoveAt(0);
                                Vars.Add(new Variable(varr.Value, ScanValue(ref stmt, out bool state)));
                                if (state)
                                    value = (Vars[Vars.Count - 1].Value as Expression).Evaluate();
                            }
                            else
                            {
                                SharedData.ucErrors.AddError(stmt[0], "No Value Assigned");
                                stmt.RemoveAt(0);
                            }
                            SharedData.SymbolTable.AddIdentifier(varr, DataType, value);
                        }
                        else if (stmt[0].Type == TokenType.Comma)
                        {
                            stmt.RemoveAt(0);
                            Vars.Add(new Variable(varr.Value, null));
                            SharedData.SymbolTable.AddIdentifier(varr, DataType, null);
                        }
                    }
                    else
                    {
                        Vars.Add(new Variable(varr.Value, null));
                        SharedData.SymbolTable.AddIdentifier(varr, DataType, null);
                    }
                }
                else
                {

                    SharedData.ucErrors.AddError(stmt[0], "Unknown Token");
                    stmt.RemoveAt(0);

                }
            }

            Variables = new Variable[Vars.Count];
            Variables = Vars.ToArray();
        }

        private List<Token> ScanValue(ref List<Token> stmt, out bool state)
        {
            List<Token> value = new List<Token>();
            state = true;
            while (stmt.Count > 0 && stmt[0].Type != TokenType.Comma)
            {
                value.Add(stmt[0]);
                if (RegularExp.IsAcceptValue(stmt[0].Value))
                {
                    if (stmt[0].Type == TokenType.Identifier)
                    {
                        Field fld = SharedData.SymbolTable.GetField(stmt[0]);
                        if (fld != null)
                        {
                            if (fld.Type != DataType && DataType != "string")
                            {
                                SharedData.ucErrors.AddError(stmt[0], $"can not cast {fld.Type} to {DataType}");
                                state = false;
                            }
                        }
                        else
                        {
                            SharedData.ucErrors.AddError(stmt[0], "Using Variable not declared");
                            state = false;
                        }
                    }
                    else if (stmt[0].Type == TokenType.StringValue && DataType != "string")
                    {
                        SharedData.ucErrors.AddError(stmt[0], $"can not cast \'{stmt[0].Type}\' to {DataType}");
                        state = false;
                    }
                    else if (stmt[0].Type == TokenType.CharValue && DataType != "char" && DataType != "string")
                    {
                        SharedData.ucErrors.AddError(stmt[0], $"can not cast \'{stmt[0].Type}\' to {DataType}");
                        state = false;
                    }
                    else if (RegularExp.IsDigitValue(stmt[0].Value) && (DataType == "char" || DataType == "bool"))
                    {
                        SharedData.ucErrors.AddError(stmt[0], $"can not cast \'{stmt[0].Type}\' to {DataType}");
                        state = false;
                    }
                    else if (stmt[0].Type == TokenType.BooleanValue && DataType != "bool" && DataType != "string")
                    {
                        SharedData.ucErrors.AddError(stmt[0], $"can not cast \'{stmt[0].Type}\' to {DataType}");
                    }

                    if (value.Count > 1 && !SharedData.IsOperator(value[value.Count - 2].Value) && value[value.Count - 2].Value != "(")
                    {
                        SharedData.ucErrors.AddError(value[value.Count - 2], $"Invalied Expression");
                        state = false;
                    }
                }
                else if (stmt[0].Type != TokenType.LeftParenthesis && stmt[0].Type != TokenType.RightParenthesis && !SharedData.IsOperator(stmt[0].Value))
                {
                    SharedData.ucErrors.AddError(stmt[0], $"UnKnow Token");
                    state = false;
                }
                stmt.RemoveAt(0);
            }
            if (stmt.Count > 0)
            {
                stmt.RemoveAt(0);
            }

            return value;
        }


        public override string ToString()
        {
            return "Declaring Variables";
        }

        public override string GetEndSymbol()
        {
            return EndSymbol;
        }
        public override string ToForString()
        {
            string tostr = DataType + " ";
            int index = 0;
            foreach (var v in Variables)
            {
                tostr += v.ToString();
                if (index < Variables.Length - 1)
                {
                    tostr += ",";
                }

                index++;
            }
            tostr += EndSymbol;
            return tostr;
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Variable
    {

        [DisplayName("Name")]

        public string Name { private set; get; }
        [DisplayName("Value")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public object Value { set; get; }
        public Variable(string name)
        {
            Name = name; Value = null;
        }

        public Variable(string name, object valu)
        {
            Name = name;
            if (valu is List<Token>)
            {
                List<Token> value = (List<Token>)valu;
                Value = Expression.GetExpression(ref value);
            }
            else
                Value = valu;
        }

        public override string ToString()
        {
            if (Value != null)
            {
                return Name + "=" + Value.ToString();
            }

            return Name;
        }
    }
}
