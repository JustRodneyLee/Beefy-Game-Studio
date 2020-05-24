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
    public partial class BeefyHub : Form
    {
        BGS gs;

        public BeefyHub()
        {
            InitializeComponent();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            NewProject np = new NewProject();
            if (np.ShowDialog() == DialogResult.OK)
            {
                DialogResult = DialogResult.Yes;
                Hide();
                gs.NewProject(np.ProjName, np.ProjPath);
                np.Dispose();
            }            
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string attempt = gs.OpenProject(openFileDialog.FileName);
            if (attempt.Contains("OK"))
            {                
                DialogResult = DialogResult.Yes;
                Hide();
            }
            else
            {
                MessageBox.Show("Failed to open project! " + attempt, "Beefy Game Studio - Error", MessageBoxButtons.OK);
                CurrentProject.Reset();
            }
        }

        private void BeefyHub_Load(object sender, EventArgs e)
        {
            gs = new BGS(this);
        }

        private void BeefyHub_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
                if (DialogResult == DialogResult.Yes || DialogResult == DialogResult.OK)
                {
                    EditorSettings.Init();
                    gs.Show();
                }
                else
                {
                    Application.Exit();
                }
        }
    }
}
