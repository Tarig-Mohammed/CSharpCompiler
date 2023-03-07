using CSharpCompiler.Classes;
using CSharpCompiler.Parsing;
using CSharpCompiler.Parsing.Statements;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace CSharpCompiler.Modules
{
    public partial class ucEditor : UserControl
    {
        
        public static ucErrors ucErrors { get; private set; }
        public string SourceCode { get => rtxtSource.Text; }
        bool IsDoubleCoution = false;
        int OpenDoubleCoutionIndex;

        public ucEditor()
        {
            InitializeComponent();
            ucErrors = this.ucErrorList;
        }
        public ucEditor(string fileName)
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(fileName) || fileName.ToLower() == ".cs")
                fileName = fileName.Insert(0, "newFile");
            lblFileName.Text = fileName;
            if (!lblFileName.Text.ToLower().EndsWith(".cs"))
                lblFileName.Text += ".cs";
            ucErrors = ucErrorList;


        }


        private void rtxtSource_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            int index = rtxtSource.SelectionStart - 1;

            if (ch == '\b')
            {
                CheckErrRemoved(index + 1);
            }
            else if (ch == '\n')
            {
                int line = rtxtSource.GetLineFromCharIndex(index + 1);
                SharedData.SymbolTable.EncrementFieldsLine(line, 1);
                ucErrorList.EncrementErrorLine(line, 1);
            }
            else if (ch == '\"')
            {
                if (!IsDoubleCoution)
                {
                    IsDoubleCoution = true;
                    OpenDoubleCoutionIndex = index + 1;
                }
                else
                {
                    IsDoubleCoution = false;
                    //new Thread(() => CheckeEntring(index)).Start();
                    // rtxtSource.Text.CopyTo(OpenDoubleCoutionIndex, wrd , 0, index - OpenDoubleCoutionIndex);
                    new Thread(() =>
                    {
                        rtxtSource.BeginInvoke(new Action(() =>
                        {
                            string wrd = GetValue(OpenDoubleCoutionIndex);
                            CheckVariable(OpenDoubleCoutionIndex, wrd);
                        }));
                    }).Start();


                }
            }
            //else if (ch == ';')
            //{
            //    new Thread(() => CheckeStatement(index + 1)).Start();
            //}
            else if (SharedData.IsOperator(ch.ToString()) || (ch == '=' && "><=!+-/*".Contains(GetChar(index))))
            {
                string op = ch.ToString();
                if (("><=!+-/*".Contains(GetChar(index)) && ch == '=') || ("-+".Contains(GetChar(index)) && GetChar(index) == ch))
                {
                    op = (GetChar(index) + ch).ToString();
                    index--;
                }
                new Thread(() => CheckOperand(index, op)).Start();

            }
            else if (!Char.IsLetterOrDigit(ch) && ch != '_' && !IsDoubleCoution)
            {


                if (ch == '.' && char.IsDigit(GetChar(index)))
                    return;
                new Thread(() => CheckeEntring(index)).Start();

                //if (SharedData.IsLogicOperator(ch.ToString()) || SharedData.IsMathOperator(ch.ToString()) || ch == '=')
                //{
                //    string op = ch.ToString();
                //    if (("><=!+-/*".Contains(GetChar(index)) && ch == '=') || ("-+".Contains(GetChar(index)) && GetChar(index) == ch))
                //    {
                //        op = (GetChar(index) + ch).ToString();
                //        index--;
                //    }
                //    new Thread(() => CheckOperand(index, op)).Start();

                //}
                if (ch == '{')
                {
                    int l = rtxtSource.GetLineFromCharIndex(index + 1);
                    SharedData.SymbolTable.AddStartScope("{", l, index - rtxtSource.GetFirstCharIndexFromLine(l) + 1);

                }
                else if (ch == '(')
                {
                    string wrd = GetWord(index);
                    if (wrd == "for")
                    {
                        int l = rtxtSource.GetLineFromCharIndex(index + 1);
                        SharedData.SymbolTable.AddStartScope("(", l, index - rtxtSource.GetFirstCharIndexFromLine(l) + 1);
                    }
                }
                else if (ch == '}')
                {
                    int l = rtxtSource.GetLineFromCharIndex(index + 1);
                    // SharedData.SymbolTable.AddEndScope("}", l, index-rtxtSource.GetFirstCharIndexFromLine(l) + 1);
                    e.Handled = true;
                    rtxtSource.Text += ch;
                    new Parser().Parsing(rtxtSource.Text);
                }

            }
        }

        private void CheckeStatement(int index)
        {
            rtxtSource.BeginInvoke(new Action(() =>
            {
                char ch = GetChar(index--);
                string stmt = ch.ToString();

                ch = GetChar(index);

                while (!"\0;{}".Contains(ch))
                {
                    stmt = stmt.Insert(0, ch.ToString());
                    ch = GetChar(--index);
                }
                string prestmt = ch.ToString();
                if (ch == ';')
                {
                    ch = GetChar(--index);
                    while (!"\0;{}".Contains(ch))
                    {
                        prestmt = stmt.Insert(0, ch.ToString());
                        ch = GetChar(--index);
                    }
                }
                if (stmt.Contains("for") || prestmt.Contains("for"))
                    return;

                if (stmt.Length > 0)
                {
                    new Parser().RunTimeCheck(rtxtSource.Text);
                }

            }));

        }

        internal void MarkError(string errString, int line, int clmn)
        {
            if (line < rtxtSource.Lines.Length)
            {

                rtxtSource.HideSelection = true;
                int index = rtxtSource.SelectionStart;

                int start = rtxtSource.GetFirstCharIndexFromLine(line) + clmn;
                start = rtxtSource.Text.IndexOf(errString, start);
                if (start >= 0 && start + errString.Length < rtxtSource.TextLength)
                {
                    rtxtSource.Select(start, errString.Length);
                    rtxtSource.SelectionBackColor = Color.Red;
                }

                rtxtSource.SelectionStart = index;

                rtxtSource.HideSelection = false;
            }
        }

        private void CheckOperand(int index, string op)
        {
            rtxtSource.BeginInvoke(new Action(() =>
            {

                char first = GetFirstCharAndIndex(index, MyOrientation.BackWard, out int firstChIndex);
                while (")\n ".Contains(first))
                    first = GetFirstCharAndIndex(firstChIndex - 1, MyOrientation.BackWard, out firstChIndex);
                string operand = GetWord(firstChIndex, false, MyOrientation.BackWard);
                int line = 0;
                int clmn = 0;

                line = rtxtSource.GetLineFromCharIndex(index);
                clmn = firstChIndex - rtxtSource.GetFirstCharIndexFromLine(line);

                if (!RegularExp.IsAcceptValue(operand))
                {
                    GenerateError(firstChIndex - operand.Length + 1, operand, $"Can not use {op} with {operand}");
                }
                else if (RegularExp.IsAcceptIdentifier(operand) && !SharedData.SymbolTable.IsExists(operand, line, clmn))
                {
                    GenerateError(firstChIndex - operand.Length + 1, operand, $"variable {operand} not exist");
                }
            }));


        }
        private string GetValue(int startIndex)
        {
            string result = null;
            char ch = GetFirstCharAndIndex(startIndex, MyOrientation.ForWard, out int index);

            if (ch == '\'')
            {
                if (index + 2 < rtxtSource.TextLength && GetChar(index + 2) == ch)
                    result = (ch + GetChar(index + 1) + ch).ToString();
                else if (index + 1 < rtxtSource.TextLength && GetChar(index + 1) == ch)
                    result = (ch + ch).ToString();
                else
                    GenerateError(index, "\'", "Missing \'");
            }

            else if (ch == '\"')
            {
                result = ScanStringValue(index);
            }
            else if (char.IsDigit(ch))
            {
                result = ScanDigitValue(index);
            }
            else if (char.IsLetter(ch))
            {
                string val = GetWord(index, false, MyOrientation.ForWard);
                if (!SharedData.IsKeyWord(val))
                    result = val;
            }


            return result;


        }

        private string ScanDigitValue(int startIndex)
        {

            string val = string.Empty;
            int counter = startIndex;
            bool findpoint = false;
            while (counter < rtxtSource.TextLength && (Char.IsDigit(GetChar(counter)) || (GetChar(counter) == '.' && !findpoint)))
            {
                if (GetChar(counter) == '.')
                    findpoint = true;
                val += GetChar(counter);
                counter++;
            }
            if (counter < rtxtSource.TextLength && (Char.IsLetter(GetChar(counter)) || "_.".Contains(GetChar(counter))))
            {
                if ("_.".Contains(GetChar(counter)) || char.IsLetter(GetChar(counter)))
                {
                    while ("_.".Contains(GetChar(counter)) || char.IsLetter(GetChar(counter)))
                        val += GetChar(counter);
                }
                GenerateError(startIndex, val, "Value not valied");
                return null;
            }
            return val;
        }

        private string ScanStringValue(int startindex)
        {

            string val = "\"";
            int counter = startindex + 1;
            while (counter < rtxtSource.TextLength && (GetChar(counter) != '\"' || (GetChar(counter) == '\"' && GetChar(counter - 1) == '\\')))
            {
                val += GetChar(counter);
                counter++;

            }
            if (GetChar(counter) == '\"' && GetChar(counter - 1) != '\\')
                val += GetChar(counter);

            if (val.EndsWith("\"") && val.Length > 1 && !val.EndsWith("\\\""))
                return val;
            else
            {
                GenerateError(startindex, val, "Missing \"");
                return null;
            }

        }

        private void CheckErrRemoved(int index)
        {

            int line = rtxtSource.GetLineFromCharIndex(index);
            int clmn = index - rtxtSource.GetFirstCharIndexFromLine(line);
            if (ucErrorList.IsErrorPosition(line, clmn))
            {
                rtxtSource.SelectionBackColor = Color.White;
                ucErrorList.RemoveError(line, clmn);
                string wrd = GetWord(index, false, MyOrientation.ForWard);
                new Thread(() => CheckeEntring(index + wrd.Length)).Start();
            }
        }

        private void CheckeEntring(int lastIndex)
        {
            rtxtSource.BeginInvoke(new Action(() =>
            {

                string wrd = GetWord(lastIndex, false, MyOrientation.BackWard);
                if (wrd.Length < 1 || !char.IsLetterOrDigit(wrd[wrd.Length - 1]))
                    return;

                if (SharedData.IsKeyWord(wrd) && !";{}()".Contains(GetFirstChar(lastIndex - wrd.Length, MyOrientation.BackWard)))
                {
                    GenerateError(lastIndex, wrd, "expected ;");
                }
                else if (!SharedData.IsKeyWord(wrd))
                {
                    CheckVariable(lastIndex - wrd.Length + 1, wrd);
                }
                else
                    CleaneError(lastIndex, wrd);
                if (GetChar(lastIndex + 1) == ';')
                {

                }
            }));


        }

        public int IndexOf(char ch, int startIndex, MyOrientation orientation)
        {
            int index = -1;
            while (startIndex >= 0 && startIndex < rtxtSource.TextLength && rtxtSource.Text[startIndex] != ch)
            {
                if (orientation == MyOrientation.ForWard)
                    startIndex++;
                else
                    startIndex--;
            }
            if (startIndex >= 0 && startIndex < rtxtSource.TextLength)
                index = startIndex;
            return index;
        }
        private void CheckVariable(int index, string wrd)
        {
            string type = GetType(index - 1);
            // GetWord(index - wrd.Length, true, MyOrientation.BackWard);
            string val = null;
            int line = rtxtSource.GetLineFromCharIndex(index);
            int clmn = index - rtxtSource.GetFirstCharIndexFromLine(line);


            if (GetFirstCharAndIndex(index + wrd.Length + 1, MyOrientation.ForWard, out int i) == ',')
            {
                char op = GetFirstChar(index - wrd.Length, MyOrientation.BackWard);
                if (SharedData.IsOperator(op.ToString()) || op == '=')
                {
                    int ind = IndexOf('=', i, MyOrientation.BackWard);
                    if (ind != -1)
                    {
                        string name = GetWord(ind - 1, true, MyOrientation.BackWard);
                        string exp = Expression.GetExpression(rtxtSource.Text.Substring(ind + 1, i - ind)).Evaluate();
                        int nameline = rtxtSource.GetLineFromCharIndex(ind - 1);
                        int nameclmn = ind - name.Length - rtxtSource.GetFirstCharIndexFromLine(nameline);
                        bool nameIsExist = SharedData.SymbolTable.IsExists(name, nameline, nameclmn);
                        if (nameIsExist)
                        {
                            SharedData.SymbolTable.ChangeIdentifierValue(name, exp, nameline, nameclmn);
                        }





                    }
                }
                //if (!RegularExp.IsAcceptIdentifier(name))
                //{
                //    GenerateError(i - name.Length + 1, name, "variable name not valid");
                //}
                //else if (!nameIsExist && !SharedData.IsDataType(nameType))
                //{
                //    GenerateError(i - name.Length + 1, name, $"variable {name} not exist");
                //}
                //else if (SharedData.IsDataType(nameType))
                //{
                //    SharedData.SymbolTable.AddIdentifier(name, nameType, null, line, nameclmn);
                //    if (!nameIsExist)
                //        nameIsExist = SharedData.SymbolTable.IsExists(name, nameline, nameclmn);
                //}
                //if (RegularExp.IsAcceptIdentifier(wrd))
                //{
                //    bool wrdIsExist = SharedData.SymbolTable.IsExists(wrd, line, index);
                //    if (wrdIsExist)//&& (nameIsExist||SharedData.IsDataType(innerType))
                //    {
                //        string wrdval = SharedData.SymbolTable.GetValueOf(wrd, line, index);
                //        SharedData.SymbolTable.ChangeIdentifierValue(name, wrdval, line, index);

                //    }
                //    else
                //        GenerateError(index, wrd, "using variable not declared");
                //}
                //else if (nameIsExist && (RegularExp.IsStringValue(wrd) || RegularExp.IsCharValue(wrd) || RegularExp.IsDigitValue(wrd)))
                //{
                //    SharedData.SymbolTable.ChangeIdentifierValue(name, wrd, line, clmn);
                //}
                //else
                //    GenerateError(index, wrd, "unKnown value");
            }
            //else if (!RegularExp.IsAcceptIdentifier(wrd))
            //    GenerateError(index, wrd, "no sush variable name");
            else if (SharedData.IsDataType(type))
            {

                if (GetFirstCharAndIndex(index + wrd.Length, MyOrientation.ForWard, out int ii) == '=')
                {
                    val = GetValue(ii + 1);
                    if (val == null && ii + 1 < rtxtSource.TextLength)
                        GenerateError(ii, "=", "no value");
                }
                SharedData.SymbolTable.AddIdentifier(wrd, type, val, line, clmn);

            }
            else if (RegularExp.IsAcceptIdentifier(wrd) && !SharedData.SymbolTable.IsExists(wrd, line, clmn))
                GenerateError(index, wrd, "variable not declared");
            //else if (GetFirstCharAndIndex(index - 1, MyOrientation.BackWard, out int ii) == '\"')
            //{
            //    if (GetFirstCharAndIndex(ii - 1, MyOrientation.BackWard, out int iassign) == '=')
            //    {
            //        GetFirstCharAndIndex(iassign - 1, MyOrientation.BackWard, out int iname);
            //        string name = GetWord(iname, false, MyOrientation.BackWard);
            //        int nameline = rtxtSource.GetLineFromCharIndex(i);
            //        int nameclmn = i - name.Length + 1 - rtxtSource.GetFirstCharIndexFromLine(nameline);
            //        bool nameIsExist = SharedData.SymbolTable.IsExists(name, nameline, nameclmn);
            //        string nameType = GetType(iname - name.Length);
            //        if (nameIsExist)
            //        {
            //            SharedData.SymbolTable.ChangeIdentifierValue()
            //        }
            //    }
            //}
            else if (RegularExp.IsStringValue(wrd) || RegularExp.IsCharValue(wrd) || RegularExp.IsDigitValue(wrd))
            {

            }
            else
            {
                CleaneError(index, wrd);
            }
        }


        private string GetType(int startIndex)
        {
            string wrd = string.Empty;
            int counter;
            //if("-+%/*&|")
            if (GetFirstCharAndIndex(startIndex, MyOrientation.BackWard, out counter) != ',')
            {
                wrd = GetWord(counter, true, MyOrientation.BackWard);
                return wrd;
            }
            while (!SharedData.IsDataType(wrd))
            {

                wrd = string.Empty;
                char ch = GetChar(counter);

                if ("\0;{}".Contains(ch))
                    break;
                while (counter >= 0 && char.IsLetterOrDigit(ch))
                {
                    wrd = wrd.Insert(0, ch.ToString());
                    counter--;
                    ch = GetChar(counter);
                }

                if ("\0;{}".Contains(ch))
                    break;
                counter--;
            }
            return wrd;
        }

        private void CleaneError(int lastIndex, string wrd)
        {
            rtxtSource.HideSelection = true;
            int selectedIndex = rtxtSource.SelectionStart;
            int errStartIndex = lastIndex - wrd.Length + 1;
            rtxtSource.SelectionStart = lastIndex - wrd.Length + 1;
            if (rtxtSource.SelectionBackColor == Color.Red)
            {

                rtxtSource.SelectionLength = wrd.Length;
                rtxtSource.SelectionBackColor = Color.Black;
            }
            rtxtSource.SelectionStart = selectedIndex;
            rtxtSource.HideSelection = false;

        }

        private void GenerateError(int startIndex, string wrd, string detailes)
        {
            if (startIndex >= 0 && startIndex + wrd.Length < rtxtSource.TextLength)
            {
                rtxtSource.HideSelection = true;
                int selectedIndex = rtxtSource.SelectionStart;
                rtxtSource.Select(startIndex, wrd.Length);
                rtxtSource.SelectionBackColor = Color.Red;
                rtxtSource.SelectionStart = selectedIndex;
                rtxtSource.HideSelection = false;
                int line = rtxtSource.GetLineFromCharIndex(startIndex);
                int clmn = startIndex - rtxtSource.GetFirstCharIndexFromLine(line);
                SharedData.ucErrors.AddError(wrd, line, clmn, detailes);
            }

        }
        private Char GetFirstCharAndIndex(int startIndex, MyOrientation orientation, out int index)
        {
            index = -1;
            while (startIndex >= 0 && startIndex < rtxtSource.TextLength && Char.IsWhiteSpace(GetChar(startIndex)))
            {
                if (orientation == MyOrientation.ForWard)
                    startIndex++;
                else
                    startIndex--;
            }
            char ch = GetChar(startIndex);

            if (ch != '\0')
                index = startIndex;
            return ch;
        }
        private Char GetFirstChar(int startIndex, MyOrientation orientation)
        {

            while (startIndex >= 0 && startIndex < rtxtSource.TextLength && Char.IsWhiteSpace(GetChar(startIndex)))
            {
                if (orientation == MyOrientation.ForWard)
                    startIndex++;
                else
                    startIndex--;
            }
            return GetChar(startIndex);

        }

        private char GetChar(int index)
        {
            if (index >= 0 && index < rtxtSource.TextLength)
                return rtxtSource.Text[index];
            return '\0';
        }

        private string GetWord(int startIndex, bool skypSpace = false, MyOrientation orientation = MyOrientation.BackWard)
        {
            string wrd = string.Empty;
            int counter = startIndex;
            char ch;
            if (skypSpace)
                ch = GetFirstCharAndIndex(startIndex, orientation, out counter);
            ch = GetChar(counter);
            while (Char.IsLetterOrDigit(ch) || ch == '_' || ch == '.')
            {
                if (orientation == MyOrientation.BackWard)
                {
                    wrd = wrd.Insert(0, ch.ToString());
                    counter--;
                }
                else
                {
                    wrd += ch;
                    counter++;
                }
                ch = GetChar(counter);
            }
            if (wrd.Length > 1)
            {



                if (orientation == MyOrientation.BackWard)
                    counter += wrd.Length + 1;
                else
                    counter -= wrd.Length - 1;
                ch = GetChar(counter);

                while (char.IsLetterOrDigit(ch) || ch == '_' || ch == '.')
                {
                    if (orientation == MyOrientation.BackWard)
                    {
                        wrd += ch;

                        counter++;
                    }
                    else
                    {
                        wrd = wrd.Insert(0, ch.ToString());

                        counter--;
                    }
                    ch = GetChar(counter);
                }
            }
            if (wrd.Length < 1)
                return ch.ToString();

            return wrd;


        }
        private void ucEditor_Enter(object sender, EventArgs e)
        {
            rtxtSource.Focus();
        }
        private void rtxtSource_KeyUp(object sender, KeyEventArgs e)
        {
            bool iscontro = e.Modifiers == Keys.Control;
            if (iscontro && e.KeyCode == Keys.V)
            {
                new Parser().Parsing(rtxtSource.Text);
            }
            if (iscontro && e.KeyCode == Keys.S)
            {
                SaveFile();
                if (lblFileName.Text[0] == '*')
                    lblFileName.Text = lblFileName.Text.Remove(0, 1);
            }
        }
        private void SaveFile()
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.FileName = lblFileName.Text;
            sd.OpenFile();
        }

        public void MarkErrors()
        {
            rtxtSource.HideSelection = true;

            foreach (ErrorDetails err in ucErrors.GetErrors())
            {
                if (SelectError(err.ErrText, err.Line, err.Clmn))
                {
                    rtxtSource.SelectionBackColor = Color.Red;
                }
            }
            rtxtSource.HideSelection = false;

        }
        public void CleanAllErrors()
        {
            rtxtSource.SelectAll();
            rtxtSource.SelectionBackColor = Color.White;
            rtxtSource.SelectionStart = rtxtSource.TextLength - 1;
        }
        public bool SelectError(string wrd, int line, int clmn)
        {
            if (line < rtxtSource.Lines.Length)
            {
                int start = rtxtSource.GetFirstCharIndexFromLine(line) + clmn;
                start = rtxtSource.Text.IndexOf(wrd, start);
                if (start >= 0 && start + wrd.Length < rtxtSource.TextLength)
                    rtxtSource.Select(start, wrd.Length);
                return true;
            }
            return false;


        }

        private void rtxtSource_TextChanged(object sender, EventArgs e)
        {

            if (lblFileName.Text[0] != '*')
                lblFileName.Text = lblFileName.Text.Insert(0, "*");
        }
    }
}
