namespace CSharpCompiler.Modules
{
    partial class ucEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSourceCode = new System.Windows.Forms.Panel();
            this.rtxtSource = new System.Windows.Forms.RichTextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.ucErrorList = new CSharpCompiler.Modules.ucErrors();
            this.pnlSourceCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSourceCode
            // 
            this.pnlSourceCode.Controls.Add(this.rtxtSource);
            this.pnlSourceCode.Controls.Add(this.lblFileName);
            this.pnlSourceCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSourceCode.Location = new System.Drawing.Point(0, 0);
            this.pnlSourceCode.Margin = new System.Windows.Forms.Padding(6);
            this.pnlSourceCode.Name = "pnlSourceCode";
            this.pnlSourceCode.Size = new System.Drawing.Size(1070, 693);
            this.pnlSourceCode.TabIndex = 9;
            // 
            // rtxtSource
            // 
            this.rtxtSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtSource.HideSelection = false;
            this.rtxtSource.Location = new System.Drawing.Point(0, 25);
            this.rtxtSource.Margin = new System.Windows.Forms.Padding(6);
            this.rtxtSource.Name = "rtxtSource";
            this.rtxtSource.Size = new System.Drawing.Size(1068, 635);
            this.rtxtSource.TabIndex = 1;
            this.rtxtSource.Text = " ";
            this.rtxtSource.TextChanged += new System.EventHandler(this.rtxtSource_TextChanged);
            this.rtxtSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtxtSource_KeyPress);
            this.rtxtSource.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rtxtSource_KeyUp);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFileName.Location = new System.Drawing.Point(0, 0);
            this.lblFileName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(120, 25);
            this.lblFileName.TabIndex = 0;
            this.lblFileName.Text = "FileName.cs";
            // 
            // ucErrorList
            // 
            this.ucErrorList.AccessibleRole = System.Windows.Forms.AccessibleRole.Dial;
            this.ucErrorList.AutoScroll = true;
            this.ucErrorList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucErrorList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucErrorList.Location = new System.Drawing.Point(0, 693);
            this.ucErrorList.Margin = new System.Windows.Forms.Padding(8);
            this.ucErrorList.MinimumSize = new System.Drawing.Size(2, 35);
            this.ucErrorList.Name = "ucErrorList";
            this.ucErrorList.Padding = new System.Windows.Forms.Padding(4);
            this.ucErrorList.Size = new System.Drawing.Size(1070, 131);
            this.ucErrorList.TabIndex = 10;
            // 
            // ucEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.ucErrorList);
            this.Controls.Add(this.pnlSourceCode);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ucEditor";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.Size = new System.Drawing.Size(1070, 844);
            this.Enter += new System.EventHandler(this.ucEditor_Enter);
            this.pnlSourceCode.ResumeLayout(false);
            this.pnlSourceCode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlSourceCode;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.RichTextBox rtxtSource;
        private ucErrors ucErrorList;
    }
}
