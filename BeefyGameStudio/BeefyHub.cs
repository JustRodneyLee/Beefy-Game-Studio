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
                gs.Show();
                gs.NewProject(np.ProjName, np.ProjPath);
                np.Dispose();
                DialogResult = DialogResult.Yes;
                Hide();
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
            gs.Show();
            gs.OpenProject(openFileDialog.FileName);
            DialogResult = DialogResult.Yes;
            Hide();
        }

        private void BeefyHub_Load(object sender, EventArgs e)
        {
            gs = new BGS(this);
        }
    }
}
