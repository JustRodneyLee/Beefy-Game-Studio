using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeefyEngine;

namespace BeefyGameStudio
{
    public partial class ScriptEditorForm : Form
    {
        public ScriptEditorForm(BeefyScript script)
        {
            InitializeComponent();
            scriptEditor.LoadScript(script);
        }

        private void ScriptEditor_Load(object sender, EventArgs e)
        {

        }
    }
}
