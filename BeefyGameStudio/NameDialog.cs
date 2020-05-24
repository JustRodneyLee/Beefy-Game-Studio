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
    public partial class NameDialog : Form
    {
        public string NameValue { get; set; }

        public NameDialog(string header = "Name Dialog")
        {
            InitializeComponent();
            Text = "Beefy Game Studio - " + header;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();            
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            NameValue = textBoxName.Text;
        }

        private void NameDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
