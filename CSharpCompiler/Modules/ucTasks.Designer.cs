namespace CSharpCompiler.Modules
{
    partial class ucTasks
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
            this.tabcontrolTasks = new System.Windows.Forms.TabControl();
            this.tpLexer = new System.Windows.Forms.TabPage();
            this.tvLexer = new System.Windows.Forms.TreeView();
            this.tpParser = new System.Windows.Forms.TabPage();
            this.pgParsing = new System.Windows.Forms.PropertyGrid();
            this.lbParsing = new System.Windows.Forms.ListBox();
            this.tpSymbolTable = new System.Windows.Forms.TabPage();
            this.dgvSymbolTable = new System.Windows.Forms.DataGridView();
            this.tabcontrolTasks.SuspendLayout();
            this.tpLexer.SuspendLayout();
            this.tpParser.SuspendLayout();
            this.tpSymbolTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSymbolTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tabcontrolTasks
            // 
            this.tabcontrolTasks.Controls.Add(this.tpLexer);
            this.tabcontrolTasks.Controls.Add(this.tpParser);
            this.tabcontrolTasks.Controls.Add(this.tpSymbolTable);
            this.tabcontrolTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabcontrolTasks.Location = new System.Drawing.Point(0, 0);
            this.tabcontrolTasks.Name = "tabcontrolTasks";
            this.tabcontrolTasks.SelectedIndex = 0;
            this.tabcontrolTasks.Size = new System.Drawing.Size(466, 388);
            this.tabcontrolTasks.TabIndex = 0;
            this.tabcontrolTasks.SelectedIndexChanged += new System.EventHandler(this.tabcontrolTasks_SelectedIndexChanged);
            this.tabcontrolTasks.TabIndexChanged += new System.EventHandler(this.tabcontrolTasks_TabIndexChanged);
            // 
            // tpLexer
            // 
            this.tpLexer.Controls.Add(this.tvLexer);
            this.tpLexer.Location = new System.Drawing.Point(4, 25);
            this.tpLexer.Name = "tpLexer";
            this.tpLexer.Padding = new System.Windows.Forms.Padding(3);
            this.tpLexer.Size = new System.Drawing.Size(458, 359);
            this.tpLexer.TabIndex = 0;
            this.tpLexer.Text = "Lexer";
            this.tpLexer.UseVisualStyleBackColor = true;
            // 
            // tvLexer
            // 
            this.tvLexer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvLexer.Location = new System.Drawing.Point(3, 3);
            this.tvLexer.Name = "tvLexer";
            this.tvLexer.Size = new System.Drawing.Size(452, 353);
            this.tvLexer.TabIndex = 0;
            // 
            // tpParser
            // 
            this.tpParser.Controls.Add(this.pgParsing);
            this.tpParser.Controls.Add(this.lbParsing);
            this.tpParser.Location = new System.Drawing.Point(4, 25);
            this.tpParser.Name = "tpParser";
            this.tpParser.Padding = new System.Windows.Forms.Padding(3);
            this.tpParser.Size = new System.Drawing.Size(458, 359);
            this.tpParser.TabIndex = 1;
            this.tpParser.Text = "Parser";
            this.tpParser.UseVisualStyleBackColor = true;
            this.tpParser.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tpParser_PreviewKeyDown);
            // 
            // pgParsing
            // 
            this.pgParsing.CommandsActiveLinkColor = System.Drawing.Color.RosyBrown;
            this.pgParsing.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pgParsing.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pgParsing.Location = new System.Drawing.Point(3, 123);
            this.pgParsing.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.pgParsing.Name = "pgParsing";
            this.pgParsing.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgParsing.Size = new System.Drawing.Size(452, 233);
            this.pgParsing.TabIndex = 2;
            // 
            // lbParsing
            // 
            this.lbParsing.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbParsing.FormattingEnabled = true;
            this.lbParsing.ItemHeight = 16;
            this.lbParsing.Location = new System.Drawing.Point(3, 3);
            this.lbParsing.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.lbParsing.Name = "lbParsing";
            this.lbParsing.Size = new System.Drawing.Size(452, 100);
            this.lbParsing.TabIndex = 1;
            this.lbParsing.SelectedIndexChanged += new System.EventHandler(this.lbParsing_SelectedIndexChanged);
            // 
            // tpSymbolTable
            // 
            this.tpSymbolTable.Controls.Add(this.dgvSymbolTable);
            this.tpSymbolTable.Location = new System.Drawing.Point(4, 25);
            this.tpSymbolTable.Name = "tpSymbolTable";
            this.tpSymbolTable.Padding = new System.Windows.Forms.Padding(3);
            this.tpSymbolTable.Size = new System.Drawing.Size(458, 359);
            this.tpSymbolTable.TabIndex = 2;
            this.tpSymbolTable.Text = "Symbol Table";
            this.tpSymbolTable.UseVisualStyleBackColor = true;
            this.tpSymbolTable.Enter += new System.EventHandler(this.tpSymbolTable_Enter);
            this.tpSymbolTable.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tpSymbolTable_PreviewKeyDown);
            // 
            // dgvSymbolTable
            // 
            this.dgvSymbolTable.AllowUserToAddRows = false;
            this.dgvSymbolTable.AllowUserToDeleteRows = false;
            this.dgvSymbolTable.AllowUserToResizeRows = false;
            this.dgvSymbolTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvSymbolTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvSymbolTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSymbolTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSymbolTable.Location = new System.Drawing.Point(3, 3);
            this.dgvSymbolTable.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.dgvSymbolTable.MinimumSize = new System.Drawing.Size(0, 1);
            this.dgvSymbolTable.MultiSelect = false;
            this.dgvSymbolTable.Name = "dgvSymbolTable";
            this.dgvSymbolTable.ReadOnly = true;
            this.dgvSymbolTable.RowHeadersVisible = false;
            this.dgvSymbolTable.Size = new System.Drawing.Size(452, 353);
            this.dgvSymbolTable.TabIndex = 2;
            this.dgvSymbolTable.VirtualMode = true;
            // 
            // ucTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabcontrolTasks);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucTasks";
            this.Size = new System.Drawing.Size(466, 388);
            this.Enter += new System.EventHandler(this.ucTasks_Enter);
            this.tabcontrolTasks.ResumeLayout(false);
            this.tpLexer.ResumeLayout(false);
            this.tpParser.ResumeLayout(false);
            this.tpSymbolTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSymbolTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabcontrolTasks;
        private System.Windows.Forms.TabPage tpLexer;
        private System.Windows.Forms.TabPage tpParser;
        private System.Windows.Forms.TreeView tvLexer;
        private System.Windows.Forms.ListBox lbParsing;
        private System.Windows.Forms.PropertyGrid pgParsing;
        private System.Windows.Forms.TabPage tpSymbolTable;
        private System.Windows.Forms.DataGridView dgvSymbolTable;
    }
}
