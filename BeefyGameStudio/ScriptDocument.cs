using System;
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
    public partial class ScriptDocument : Form
    {

        public ScriptDocument()
        {
            InitializeComponent();
            ActiveControl = scriptEditor;            
        }

        private void scriptEditor_Click(object sender, EventArgs e)
        {
            Focus();
        }

        private void ScriptDocument_Load(object sender, EventArgs e)
        {

        }

        private void ScriptDocument_FormClosing(object sender, FormClosingEventArgs e)
        {
            scriptEditor.Dispose(); //KEY PIECE OF CODE
        }
    }
}
