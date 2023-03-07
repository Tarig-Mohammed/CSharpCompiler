using CSharpCompiler.Classes;
using CSharpCompiler.Lexer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CSharpCompiler.Modules
{
    public partial class ucErrors : UserControl
    {
        public int ErrCount = 0;
        List<ErrorDetails> Errors = new List<ErrorDetails>();
        bool ErrsWindowMaximized;
        public ucErrors()
        {
            InitializeComponent();
            ErrsWindowMaximized = false;
        }
        public void AddError(string errString, int line, int clmn, string detailes)
        {
            int err = Errors.FindIndex(i => i.ErrText == errString && i.Line == line && i.Clmn == clmn);

            if (err == -1)
            {
                Errors.Add(new ErrorDetails(errString, line, clmn, detailes));
                dgvErrorsDetails.DataSource = Errors.ToList();
                ErrCount++;
                lblText.Text = $"({ErrCount}) Error List";
            }
            frmMain frm = FindForm() as frmMain;
            frm.MyEditor.MarkError(errString, line, clmn);
        }

        public void AddError(Token t, string detailes)
        {
            int line = t.Location.Line - 1;
            int clmn = t.Location.Column - 1;
            AddError(t.Value, line, clmn, detailes);
        }
        public void AddMultiErrors(List<Token> errs, string detailes)
        {
            foreach (var t in errs)
            {

                int line = t.Location.Line - 1;
                int clmn = t.Location.Column - 1;
                AddError(t.Value, line, clmn, detailes);
            }
        }
        public void AddRangeErrors(List<ErrorDetails> errors)
        {
            foreach (ErrorDetails err in errors)
            {
                int i = Errors.FindIndex(e => e.ErrText == err.ErrText && e.Line == err.Line && e.Clmn == err.Clmn);
                if (i == -1)
                {
                    Errors.Add(err);
                    ErrCount++;
                }
            }
            dgvErrorsDetails.DataSource = Errors.ToList();
            lblText.Text = $"({ErrCount}) Error List";
        }

        internal void Clear()
        {
            Errors.Clear();
            dgvErrorsDetails.DataSource = Errors.ToList();
            ErrCount = 0;
            lblText.Text = $"({ErrCount}) Error List";
        }

        internal void EncrementErrorLine(int lastline, int incrementBy)
        {
            List<ErrorDetails> errors = Errors.FindAll(x => x.Line >= lastline);
            foreach (var err in errors)
            {
                int i = Errors.FindIndex(e => e.ErrText == err.ErrText && e.Line == err.Line && e.Clmn == err.Clmn && e.Detailes == err.Detailes);
                Errors[i].Line += incrementBy;

            }
        }

        internal bool ContainsErrors()
        {
            return Errors.Count > 0;
        }

        internal void DecrementErrorLine(int lastline, int decrementBy)
        {
            List<ErrorDetails> errors = Errors.FindAll(x => x.Line >= lastline);
            foreach (var err in errors)
            {
                int i = Errors.FindIndex(e => e.ErrText == err.ErrText && e.Line == err.Line && e.Clmn == err.Clmn && e.Detailes == err.Detailes);
                Errors[i].Line -= decrementBy;

            }
        }

        public void RemoveError(int line, int clmn)
        {
            if (Errors.Remove(Errors.Find(i => i.Line == line && i.Clmn == clmn)))
            {
                dgvErrorsDetails.DataSource = Errors.ToList();
                ErrCount--;
                lblText.Text = $"({ErrCount}) Error List";
            }
        }

        private void dgvErrorsDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmMain frm = (frmMain)FindForm();
            string errString = dgvErrorsDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
            int line = Convert.ToInt32(dgvErrorsDetails.Rows[e.RowIndex].Cells[1].Value.ToString());
            int clmn = Convert.ToInt32(dgvErrorsDetails.Rows[e.RowIndex].Cells[2].Value.ToString());
            frm.MyEditor.SelectError(errString, line, clmn);
        }
        public bool IsErrorPosition(int line, int clmn)
        {
            if (Errors.Find(i => i.Line == line && (i.Clmn == clmn)) != null)
                return true;
            return false;
        }

        private void ucErrors_Resize(object sender, EventArgs e)
        {

        }
        public void ShowErrors()
        {
            this.Size = new Size(this.Width, lblText.Size.Height+ dgvErrorsDetails.Height);
            ErrsWindowMaximized = true;
        }
        private void lblText_Click(object sender, EventArgs e)
        {
            if (ErrsWindowMaximized)
            {
                this.Size = new Size(this.Width, lblText.Size.Height);

                ErrsWindowMaximized = false;
            }
            else
            {
                this.Size = new Size(this.Width, lblText.Size.Height+300 );
                ErrsWindowMaximized = true;

            }
        }

        internal IEnumerable<ErrorDetails> GetErrors()
        {
            return Errors;
        }
    }
}
