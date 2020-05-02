using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeefyGameStudio
{
    public partial class ProgressForm : Form
    {
        string actionTBD;     

        public ProgressForm(int maxVal, string stuffToDo)
        {
            InitializeComponent();
            progressBar.Maximum = maxVal;
            actionTBD = stuffToDo;
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            if (actionTBD == "")
            {
                label_Action.Visible = false;
                Height = 95;
            }
        }

        public void UpdateProgress(int increment, string processText)
        {
            if (processText != "")
            {                
                Height = 120;
                label_Action.Location = new Point(Width/2 - label_Action.Text.Length*4, label_Action.Location.Y);
                label_Action.Text = processText;
            }
            progressBar.Value += increment;
        }

        public void Finish()
        {
            Close();
            Dispose();
        }
    }
}
