namespace CSharpCompiler
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCreateNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutUsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCompile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSourceCode = new System.Windows.Forms.ToolStripMenuItem();
            this.mnucheckErrors = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlWork = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mnuCompile,
            this.mnuSourceCode,
            this.mnucheckErrors,
            this.mnuAbout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(921, 30);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCreateNewFile,
            this.openFileToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.aboutUsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // mnuCreateNewFile
            // 
            this.mnuCreateNewFile.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.mnuCreateNewFile.Name = "mnuCreateNewFile";
            this.mnuCreateNewFile.Size = new System.Drawing.Size(196, 26);
            this.mnuCreateNewFile.Text = "Create &New File";
            this.mnuCreateNewFile.Click += new System.EventHandler(this.mnuCreateNewFile_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.openFileToolStripMenuItem.Text = "Open File";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.aboutToolStripMenuItem.Text = "Save";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // aboutUsToolStripMenuItem
            // 
            this.aboutUsToolStripMenuItem.Name = "aboutUsToolStripMenuItem";
            this.aboutUsToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.aboutUsToolStripMenuItem.Text = "About Us";
            // 
            // mnuCompile
            // 
            this.mnuCompile.CheckOnClick = true;
            this.mnuCompile.Enabled = false;
            this.mnuCompile.Name = "mnuCompile";
            this.mnuCompile.Size = new System.Drawing.Size(79, 26);
            this.mnuCompile.Text = "Compile";
            this.mnuCompile.Click += new System.EventHandler(this.mnuCompile_Click);
            // 
            // mnuSourceCode
            // 
            this.mnuSourceCode.CheckOnClick = true;
            this.mnuSourceCode.Enabled = false;
            this.mnuSourceCode.Name = "mnuSourceCode";
            this.mnuSourceCode.Size = new System.Drawing.Size(107, 26);
            this.mnuSourceCode.Text = "Source Code";
            this.mnuSourceCode.Click += new System.EventHandler(this.mnuSourceCode_Click);
            // 
            // mnucheckErrors
            // 
            this.mnucheckErrors.Name = "mnucheckErrors";
            this.mnucheckErrors.Size = new System.Drawing.Size(104, 26);
            this.mnucheckErrors.Text = "Check Errors";
            this.mnucheckErrors.Click += new System.EventHandler(this.mnucheckErrors_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(64, 26);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // pnlWork
            // 
            this.pnlWork.AutoSize = true;
            this.pnlWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWork.Location = new System.Drawing.Point(0, 30);
            this.pnlWork.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.pnlWork.MinimumSize = new System.Drawing.Size(921, 609);
            this.pnlWork.Name = "pnlWork";
            this.pnlWork.Size = new System.Drawing.Size(921, 658);
            this.pnlWork.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(921, 688);
            this.Controls.Add(this.pnlWork);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "frmMain";
            this.Text = "Main Form";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuCreateNewFile;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutUsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuCompile;
        private System.Windows.Forms.Panel pnlWork;
        private System.Windows.Forms.ToolStripMenuItem mnuSourceCode;
        private System.Windows.Forms.ToolStripMenuItem mnucheckErrors;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
    }
}

