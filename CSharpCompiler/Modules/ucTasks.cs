using CSharpCompiler.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CSharpCompiler.Lexer;
using CSharpCompiler.Parsing;
using CSharpCompiler.Parsing.Statements;

namespace CSharpCompiler.Modules
{
    public partial class ucTasks : UserControl
    {
        Scanner scanner;
        Parser parser;
        private static string SourceCode = string.Empty;
        public ucTasks()
        {
            InitializeComponent();


            /// pgParsing.ExpandAllGridItems();

        }

        private void tabcontrolTasks_TabIndexChanged(object sender, EventArgs e)
        {

        }

        internal void Confirm(string sourceCode, int flag)
        {
            int cmp = string.Compare(sourceCode, SourceCode);
            if (cmp != 0)
            {
                tvLexer.Nodes.Clear();
                lbParsing.Items.Clear();
                SourceCode = sourceCode;
                scanner = new Scanner(SourceCode);
                tvLexer.Nodes.Clear();
                List<Token> tokens = scanner.Scan();
                AddTokensToTreeView(tokens);
                parser = new Parser();
                List<StatementBase> stmts = parser.Parsing(SourceCode);
                lbParsing.Items.AddRange(stmts.ToArray());
                dgvSymbolTable.DataSource = SharedData.SymbolTable.Fields.ToArray();
            }


        }
        internal void Confirm(List<StatementBase> stmts)
        {
            lbParsing.Items.AddRange(stmts.ToArray());
        }

        private void AddTokensToTreeView(List<Token> tokens)
        {

            foreach (var item in tokens)
            {
                tvLexer.Nodes.Add(item.ToNode());
            }
        }

        private void lbParsing_SelectedIndexChanged(object sender, EventArgs e)
        {
            pgParsing.PropertySort = PropertySort.NoSort;
            pgParsing.SelectedObject = lbParsing.SelectedItem;
        }

        private void tabcontrolTasks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tpSymbolTable_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            dgvSymbolTable.DataSource = SharedData.SymbolTable.Fields.ToList();

        }

        private void tpParser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void ucTasks_Enter(object sender, EventArgs e)
        {
        }

        private void tpSymbolTable_Enter(object sender, EventArgs e)
        {
            dgvSymbolTable.DataSource = SharedData.SymbolTable.Fields.ToList();

        }
    }
}
