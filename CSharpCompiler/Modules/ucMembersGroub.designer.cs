
namespace CSharpCompiler.Modules
{
    partial class ucMembersGroub
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
            this.gbMemberGroub = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // gbMemberGroub
            // 
            this.gbMemberGroub.BackColor = System.Drawing.Color.Transparent;
            this.gbMemberGroub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMemberGroub.Location = new System.Drawing.Point(0, 0);
            this.gbMemberGroub.Margin = new System.Windows.Forms.Padding(5);
            this.gbMemberGroub.Name = "gbMemberGroub";
            this.gbMemberGroub.Padding = new System.Windows.Forms.Padding(5);
            this.gbMemberGroub.Size = new System.Drawing.Size(431, 379);
            this.gbMemberGroub.TabIndex = 0;
            this.gbMemberGroub.TabStop = false;
            this.gbMemberGroub.Text = "Author";
            this.gbMemberGroub.Paint += new System.Windows.Forms.PaintEventHandler(this.MembersGroub_Paint);
            // 
            // ucMembersGroub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.gbMemberGroub);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Blue;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ucMembersGroub";
            this.Size = new System.Drawing.Size(431, 379);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucMembersGroub_Paint);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbMemberGroub;
    }
}
