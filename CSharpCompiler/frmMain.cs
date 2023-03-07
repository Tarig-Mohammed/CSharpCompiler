using CSharpCompiler.Modules;
using CSharpCompiler.Parsing;
using System;
using System.Windows.Forms;

namespace CSharpCompiler
{
    public partial class frmMain : Form
    {
        ucMembersGroub mmbrGroub = new ucMembersGroub();
        ucTasks MyTasks;
        public ucEditor MyEditor { get; private set; }
        private bool SetToolBarEnable
        {
            set
            {
                mnuCompile.Enabled = value;
                mnuSourceCode.Enabled = value;
            }
        }
        public frmMain()
        {
            InitializeComponent();
            InitTaskControl();
        }

        private void InitTaskControl()
        {
            MyEditor = new ucEditor();
            MyTasks = new ucTasks();
            MyEditor.Dock = DockStyle.Fill;
            MyTasks.Dock = DockStyle.Fill;

        }

        private void mnuCreateNewFile_Click(object sender, EventArgs e)
        {
            CreateNewFile cn = new CreateNewFile();
            if (cn.ShowDialog() == DialogResult.OK)
            {
                MyEditor = new ucEditor(cn.FileName);
                MyEditor.Dock = DockStyle.Fill;
                pnlWork.Controls.Clear();
                pnlWork.Controls.Add(MyEditor);
                SetToolBarEnable = true;
                mnucheckErrors.Enabled = true;
            }
        }

        private void mnuCompile_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            MyTasks.Confirm(MyEditor.SourceCode, 1);



            mnucheckErrors.Enabled = false;
            pnlWork.Controls.Clear();
            pnlWork.Controls.Add(MyTasks);
            Cursor = Cursors.Default;



        }


        private void mnuSourceCode_Click(object sender, EventArgs e)
        {
            mnucheckErrors.Enabled = true;
            pnlWork.Controls.Clear();
            pnlWork.Controls.Add(MyEditor);
        }

        private void mnucheckErrors_Click(object sender, EventArgs e)
        {

            //pnlWork.Controls.Clear();
            //pnlWork.Controls.Add(MyTasks);
            string sourceCode = MyEditor.SourceCode;
            if (!string.IsNullOrEmpty(sourceCode))
            {
                Cursor = Cursors.WaitCursor;
                MyEditor.CleanAllErrors();
                new Parser().Parsing(sourceCode);
                MyEditor.MarkErrors();
                Cursor = Cursors.Default;

                //pnlWork.Controls.Clear();
                //pnlWork.Controls.Add(SharedData.ucErrors);
                //MyTasks.Confirm(stm);

            }
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            mmbrGroub.Dock = DockStyle.Fill;
            pnlWork.Controls.Clear();
            pnlWork.Controls.Add(mmbrGroub);
        }
    }
}
