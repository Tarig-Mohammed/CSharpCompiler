namespace CSharpCompiler.Modules
{
    partial class ucErrors
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
            this.lblText = new System.Windows.Forms.Label();
            this.dgvErrorsDetails = new System.Windows.Forms.DataGridView();
            this.clErrString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clClmn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clErrDitailes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrorsDetails)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText.ForeColor = System.Drawing.Color.Red;
            this.lblText.Location = new System.Drawing.Point(4, 4);
            this.lblText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(114, 20);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "(0)Error List";
            this.lblText.Click += new System.EventHandler(this.lblText_Click);
            // 
            // dgvErrorsDetails
            // 
            this.dgvErrorsDetails.AllowUserToAddRows = false;
            this.dgvErrorsDetails.AllowUserToDeleteRows = false;
            this.dgvErrorsDetails.AllowUserToResizeRows = false;
            this.dgvErrorsDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvErrorsDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvErrorsDetails.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvErrorsDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErrorsDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clErrString,
            this.clLine,
            this.clClmn,
            this.clErrDitailes});
            this.dgvErrorsDetails.Location = new System.Drawing.Point(4, 4);
            this.dgvErrorsDetails.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.dgvErrorsDetails.MultiSelect = false;
            this.dgvErrorsDetails.Name = "dgvErrorsDetails";
            this.dgvErrorsDetails.ReadOnly = true;
            this.dgvErrorsDetails.RowHeadersVisible = false;
            this.dgvErrorsDetails.RowHeadersWidth = 51;
            this.dgvErrorsDetails.Size = new System.Drawing.Size(417, 141);
            this.dgvErrorsDetails.TabIndex = 1;
            this.dgvErrorsDetails.VirtualMode = true;
            this.dgvErrorsDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvErrorsDetails_CellDoubleClick);
            // 
            // clErrString
            // 
            this.clErrString.DataPropertyName = "ErrText";
            this.clErrString.HeaderText = "Error String";
            this.clErrString.MinimumWidth = 6;
            this.clErrString.Name = "clErrString";
            this.clErrString.ReadOnly = true;
            this.clErrString.Width = 102;
            // 
            // clLine
            // 
            this.clLine.DataPropertyName = "Line";
            this.clLine.HeaderText = "Line";
            this.clLine.MinimumWidth = 6;
            this.clLine.Name = "clLine";
            this.clLine.ReadOnly = true;
            this.clLine.Width = 61;
            // 
            // clClmn
            // 
            this.clClmn.DataPropertyName = "Clmn";
            this.clClmn.HeaderText = "Column";
            this.clClmn.MinimumWidth = 6;
            this.clClmn.Name = "clClmn";
            this.clClmn.ReadOnly = true;
            this.clClmn.Width = 81;
            // 
            // clErrDitailes
            // 
            this.clErrDitailes.DataPropertyName = "Detailes";
            this.clErrDitailes.HeaderText = "Detailes";
            this.clErrDitailes.MinimumWidth = 6;
            this.clErrDitailes.Name = "clErrDitailes";
            this.clErrDitailes.ReadOnly = true;
            this.clErrDitailes.Width = 86;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.dgvErrorsDetails);
            this.panel1.Location = new System.Drawing.Point(-1, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(422, 151);
            this.panel1.TabIndex = 2;
            // 
            // ucErrors
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dial;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblText);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucErrors";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(428, 182);
            this.Resize += new System.EventHandler(this.ucErrors_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrorsDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.DataGridView dgvErrorsDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn clErrString;
        private System.Windows.Forms.DataGridViewTextBoxColumn clLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn clClmn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clErrDitailes;
        private System.Windows.Forms.Panel panel1;
    }
}
