using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeefyGameEngine;

namespace BeefyGameStudio
{
    public partial class ScriptEditorForm : Form
    {
        BGS parent;
        int docs;

        public ScriptEditorForm(BGS gameStudio)
        {
            InitializeComponent();
            parent = gameStudio;            
        }

        public void LoadScript(string scriptName)
        {
            //scriptEditor.LoadScript(CurrentProject.AssetsPath + scriptName);
        }

        private void ScriptEditor_Load(object sender, EventArgs e)
        {
            docs = 0;
        }

        private void functionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*foreach (BeefyScript.BeefyFunction bf in ActiveMdiChild.Script)
            {
                string code =
            @"public class " + bf.FunctionName + @" : BeefyFunction
            {
                public " + bf.FunctionName + @"(string name) : base(name)
                {
                    FunctionName = name;
                }

                public override void Run(ParameterCollection pc = null)
                {

                }
            }";
            }
         */   
        }

        private void ifThenElseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void switchCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void forNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void whileDoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void doRepeatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void variableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void constantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptDocument doc = new ScriptDocument();  
            doc.MdiParent = this;
            doc.Text = "New Script Document " + docs++;
            doc.Show();            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ScriptEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.scriptEditorShown = false;
        }
    }
}
