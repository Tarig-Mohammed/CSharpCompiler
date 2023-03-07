using CSharpCompiler.Lexer;
using System.Collections.Generic;
using System.ComponentModel;

namespace CSharpCompiler.Classes
{
    public class SymbolTable
    {
        public List<Field> Fields;

        List<Scope> Scopes = new List<Scope>();
        public SymbolTable()
        {
            Fields = new List<Field>();
        }


        internal Field GetField(Token varname)
        {
            int l = GetLastFieldIndex(varname);
            return l != -1 ? Fields[l] : null;

        }
        public string GetValueOf(string varName, int line, int clmn, out bool state)
        {
            return GetValueOf(new Token(TokenType.Identifier, varName, new TokenLocation(line + 1, clmn + 1)), out state);
        }
        internal string GetValueOf(Token varname, out bool state)
        {
            state = true;

            int l = GetLastFieldIndex(varname);
            if (l == -1)
            {
                state = false;
                return null;
            }

            return Fields[l].Value;
        }
        public void AddIdentifier(string name, string type, string value, int line, int clmn)
        {
            Token var = new Token(TokenType.Identifier, name, new TokenLocation(line + 1, clmn + 1));
            AddIdentifier(var, type, value);
        }
        public void AddIdentifier(Token name, string type, string value)
        {
            GetFieldScope(name, out Token newIdStartBlock, out Token newIdEndBlock);
            Field[] flds = Fields.FindAll(f => f is DeclareField && f.VarName.Value == name.Value && f.Scope.Start <= newIdStartBlock && (f.Scope.End == null || f.Scope.End > name)).ToArray();
            if (string.IsNullOrEmpty(value))
            {
                switch (type)
                {
                    case "int":
                        value = "0";
                        break;
                    case "float":
                        value = "0";
                        break;
                    case "double":
                        value = "0";
                        break;
                    case "bool":
                        value = "false";
                        break;
                    default:
                        break;
                }
            }
            if (flds.Length == 0)
            {
                Fields.Add(new DeclareField(name, type, value, new Scope(newIdStartBlock, newIdEndBlock)));
            }
            else
                SharedData.ucErrors.AddError(name, $"variable name is exist in line {flds[0].VarName.Location.Line - 1}");

            //if (flds.Length > 0)
            //    foreach (var f in flds)
            //    {
            //        if (newIdStartBlock == f.Scope.Start && !f.VarName.Equals(name))
            //        {
            //            SharedData.ucErrors.AddError(name, $"variable name is exist in line {f.VarName.Location.Line - 1}");
            //            return;
            //        }
            //    }
            //Fields.Add(new DeclareField(name, type, value, new Scope(newIdStartBlock, newIdEndBlock)));
        }
        void GetFieldScope(Token lineInScope, out Token startBlock, out Token endBlock)
        {
            startBlock = null;
            endBlock = null;
            List<Scope> scps = Scopes.FindAll(s => s.Start <= lineInScope && (s.End == null || s.End >= lineInScope));
            if (scps.Count == 0)
                return;
            Scope scp = scps[0];
            foreach (Scope s in scps)
            {
                if (s.Start > scp.Start && s.End < scp.End)
                {
                    scp = s;
                }
            }
            startBlock = scp.Start;
            endBlock = scp.End;
        }



        public int GetLastFieldIndex(Token name)
        {
            List<Field> flds = Fields.FindAll(f => f.VarName.Value == name.Value && f.VarName <= name && (f.Scope.End == null || f.Scope.End > name));
            if (flds.Count == 0)
                return -1;
            Field fld = flds[0];
            foreach (var f in flds)
            {
                if (f.VarName > fld.VarName && !f.VarName.Equals(name) && (f.Scope.End == null || f.Scope.End > name))
                {
                    fld = f;
                }
            }
            return Fields.IndexOf(fld);
        }

        internal void Clear()
        {
            Fields.Clear();
            Scopes.Clear();
        }


        public void RemoveIdentifier(string name, int line, int clmn)
        {
            int fieldIndex = Fields.FindIndex(x => x.VarName.Value == name && x.VarName.Location.Line == line + 1 && x.VarName.Location.Column == clmn + 1);
            if (fieldIndex != -1)
            {
                Fields.RemoveAt(fieldIndex);
            }
        }
        public void ChangeIdentifierValue(string name, string value, int line, int clmn)
        {
            Token ft = new Token(TokenType.Identifier, name, new TokenLocation(line + 1, clmn + 1));
            ChangeIdentifierValue(ft, value);

        }
        public void ChangeIdentifierValue(Token name, string value)
        {
            int index = GetLastFieldIndex(name);
            Field fld = GetFieldDeclaring(name);

            if (index == -1 || fld == null)
            {
                SharedData.ucErrors.AddError(name, "Variable not Exists");
                return;
            }
            if (Fields[index] is AssignField)
                Fields[index].Scope.End = new Token(TokenType.Empty, "", new TokenLocation(name.Location.Line, name.Location.Column - 1));

            Fields.Add(new AssignField(name, fld.Type, value, new Scope(name, fld.Scope.End), index));
        }


        private Field GetFieldDeclaring(Token name)
        {
            Field fld = Fields.Find(f => (f is DeclareField) && f.VarName.Value == name.Value && f.Scope.Start <= name && (f.Scope.End == null || f.Scope.End > name));
            return fld;

        }

        public void EncrementFieldsLine(int startline, int incrementBy)
        {
            startline++;
            for (int i = 0; i < Fields.Count; i++)
            {
                if (Fields[i].VarName.Location.Line >= startline)
                {
                    Fields[i].VarName.Location.Line += incrementBy;
                }
                if (Fields[i].Scope.Start.Location.Line >= startline)
                {
                    Fields[i].Scope.Start.Location.Line += incrementBy;
                }
                if (Fields[i].Scope.End.Location.Line >= startline)
                {
                    Fields[i].Scope.End.Location.Line += incrementBy;
                }
            }

            for (int i = 0; i < Scopes.Count; i++)
            {
                if (Scopes[i].Start.Location.Line >= startline)
                    Scopes[i].Start.Location.Line += incrementBy;
                if (Scopes[i].End.Location.Line >= startline)
                    Scopes[i].End.Location.Line += incrementBy;
            }

        }


        internal void DecrementFiledLine(int startline, int decrementBy)
        {
            startline++;
            for (int i = 0; i < Fields.Count; i++)
            {
                if (Fields[i].VarName.Location.Line >= startline)
                {
                    Fields[i].VarName.Location.Line -= decrementBy;
                }
                if (Fields[i].Scope.Start.Location.Line >= startline)
                {
                    Fields[i].Scope.Start.Location.Line -= decrementBy;
                }
                if (Fields[i].Scope.End.Location.Line >= startline)
                {
                    Fields[i].Scope.End.Location.Line -= decrementBy;
                }
            }
            for (int i = 0; i < Scopes.Count; i++)
            {
                if (Scopes[i].Start.Location.Line >= startline)
                    Scopes[i].Start.Location.Line -= decrementBy;
                if (Scopes[i].End.Location.Line >= startline)
                    Scopes[i].End.Location.Line -= decrementBy;
            }

        }
        public bool IsExists(string name, int line, int clmn)
        {
            Token to = new Token(TokenType.Identifier, name, new TokenLocation(line + 1, clmn + 1));
            return IsExists(to);
        }

        internal bool IsExists(Token to)
        {
            return Fields.Exists(f => f.VarName.Value == to.Value && f.VarName <= to && (f.Scope.End == null || f.Scope.End > to));
        }

        private void RefreshIdentifiers()
        {
            for (int i = 0; i < Fields.Count; i++)
            {
                var f = Fields[i];
                if (f is DeclareField)
                {
                    GetFieldScope(f.VarName, out Token start, out Token end);
                    Fields[i].Scope = new Scope(start, end);
                }
                else
                {
                    Field dfld = GetFieldDeclaring(f.VarName);

                    if (dfld != null)
                        Fields[i].Scope.End = dfld.Scope.End;
                    int l = GetLastFieldIndex(f.VarName);
                    if (l != -1 && l != i && Fields[l] is AssignField)
                        Fields[l].Scope.End = new Token(TokenType.Empty, "", new TokenLocation(f.VarName.Location.Line, f.VarName.Location.Column - 1));


                }

            }
        }
        internal void AddEndScope(Token start, Token end)
        {

            int i = Scopes.FindIndex(s => s.Start == start);
            if (i != -1)
            {
                Token ed = Scopes[i].End;
                Scopes[i].End = end;
                if (ed != null && start.Type != TokenType.LeftParenthesis)
                    AddEndScope(ed);
            }
            else
                AddEndScope(end);
            RefreshIdentifiers();
        }


        internal void AddEndScope(Token end)
        {
            Token start = null;
            //Scope Forst = null;
            foreach (var s in Scopes)
            {
                if (s.Start > start && s.Start < end && (s.End == null || s.End > end))
                {
                    //if (s.Start.Type == TokenType.LeftParenthesis && s.End == null)
                    //    Forst = s;
                    start = s.Start;
                }
            }

            if (start != null)
            {
                //if (Forst != null)
                //{
                //    //if ( Forst.End == null && (Forst.Start.Location.Line == start.Location.Line || Forst.Start.Location.Line + 1 == start.Location.Line))
                //    //    AddEndScope(Forst.Start, end);
                //     if ( Forst.End == null)
                //        AddEndScope(Forst.Start, new Token(TokenType.Semicolon, ";", new TokenLocation(Forst.Start.Location.Line + 1, 1000)));
                //}

                AddEndScope(start, end);
            }
            else
                SharedData.ucErrors.AddError(end, "no open Braket for this");
        }
        internal void AddEndScope(string scopesymbol, int line, int clmn)
        {
            AddEndScope(new Token(TokenType.RightBracket, scopesymbol, new TokenLocation(line + 1, clmn + 1)));
        }


        internal void AddStartScope(Token start)
        {
            int index = -1;
            Token comp = null;
            for (int i = 0; i < Scopes.Count; i++)
            {
                if (Scopes[i].Start < start && Scopes[i].Start > comp && (Scopes[i].End == null || Scopes[i].End > start))
                {
                    comp = Scopes[i].Start;
                    index = i;
                }
            }
            if (index == -1 || (!Scopes[index].Start.Equals(start) && (Scopes[index].End == null)))
            {
                Scopes.Add(new Scope(start));
            }
            else if (!Scopes[index].Start.Equals(start))
            {

                if (Scopes.Find(s => s.Start.Equals(start)) != null)
                {
                    int i = Scopes.FindIndex(s => s.Start.Equals(start));
                    Scopes[i].End = Scopes[index].End;
                }
                else
                    Scopes.Add(new Scope(start, Scopes[index].End));
                AddStartScope(Scopes[index].Start);
            }
            RefreshIdentifiers();
        }
        internal void AddStartScope(string scopesymbol, int line, int clmn)
        {
            TokenType tt = scopesymbol == "(" ? TokenType.LeftParenthesis : TokenType.LeftBracket;
            AddStartScope(new Token(tt, scopesymbol, new TokenLocation(line + 1, clmn + 1)));
        }
    }

    public class Scope
    {
        public Token Start { set; get; }
        public Token End { set; get; }

        public Scope(Token st, Token end)
        {
            Start = st;
            End = end;
        }
        public Scope(Token st)
        {
            Start = st;
            End = null;
        }

        public override string ToString()
        {
            if (Start == null)
                return "";
            string str = "Start " + Start.Location.Line + " : End ";
            if (End != null)
                str += End.Location.Line;
            else
                str += "-1";
            return str;
        }
    }

    public class Field
    {
        [DisplayName("Name")]
        public Token VarName { set; get; }
        [DisplayName("Type")]
        public string Type { set; get; }
        [DisplayName("Value")]
        public string Value { set; get; }
        [DisplayName("Scope")]
        public Scope Scope { set; get; }
    }
    public class DeclareField : Field
    {
        public DeclareField(Token name, string type, string value, Scope scope)
        {
            VarName = name;
            Type = type;
            Value = value;
            Scope = scope;
        }

    }
    public class AssignField : Field
    {
        [DisplayName("Declaring Address")]
        public int Address { set; get; }
        public AssignField(Token name, string type, string value, Scope scope, int addrs)
        {
            VarName = name;
            Type = type;
            Value = value;
            Scope = scope;
            Address = addrs;
        }

    }
}
