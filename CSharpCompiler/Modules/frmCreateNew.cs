using System;
using System.Windows.Forms;

namespace CSharpCompiler.Modules
{
    public partial class CreateNewFile : Form
    {
        public string FileName;
        public CreateNewFile()
        {
            InitializeComponent();
            FileName = "noname";
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            FileName = txtFileName.Text;
            lblNotAcceptName.Visible = !RegularExp.IsAcceptFileName(FileName);
        }

        private void CreateNewFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = lblNotAcceptName.Visible;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lblNotAcceptName.Visible = false;

        }
    }
}
