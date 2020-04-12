using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeefyGameStudio
{
    public partial class OutputConsole : Form
    {
        List<CompilerError> errors;

        public OutputConsole(List<CompilerError> compilerErrors)
        {
            InitializeComponent();
            errors = compilerErrors;
        }

        private void OutputConsole_Load(object sender, EventArgs e)
        {
            foreach (CompilerError error in errors)
            {
                string errTxt = error.ErrorText;
                string errFile = error.FileName;
                if (!error.IsWarning)
                    errFile = "Error:" + errFile;
                else                    
                    errFile = "Warning:" + errFile;
                int errLine = error.Line;
                int errColumn = error.Column;
                listViewMessages.Items.Add(new ListViewItem(new string[3] {errFile, errTxt, "Line " + errLine.ToString() + ", Column" + errColumn.ToString()}));
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
