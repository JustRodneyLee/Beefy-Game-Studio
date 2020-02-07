using System;
using System.IO;
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
    public partial class NewProject : Form
    {
        public string Path { get; set; }

        public NewProject()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Replace(" ","")!="")
            {
                if (textBoxName.Text.Contains("|")||textBoxName.Text.Contains("\\")||textBoxName.Text.Contains("/") || textBoxName.Text.Contains("?") || textBoxName.Text.Contains("*") || textBoxName.Text.Contains(":") ||textBoxName.Text.Contains("<") || textBoxName.Text.Contains(">") || textBoxName.Text.Contains("\"")) //Illegal characters
                {
                    MessageBox.Show("Project name contains illegal characters!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (Directory.Exists(comboBoxPath.SelectedItem.ToString() + "\\" + textBoxName.Text))
                    {
                        MessageBox.Show("Folder already exists!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                        Path = comboBoxPath.SelectedItem.ToString() + "\\" + textBoxName.Text;
                        Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Project name cannot be empty!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void NewProject_Load(object sender, EventArgs e)
        {
            comboBoxPath.Items.Add("<Browse...>");
        }

        private void comboBoxPath_SelectedValueChanged(object sender, EventArgs e)
        {            
            if ((string)comboBoxPath.SelectedItem == "<Browse...>")
            {
                DialogResult dr = folderBrowserDialog.ShowDialog();
                if (dr == DialogResult.OK || dr == DialogResult.Yes)
                {
                    comboBoxPath.Items.Add(folderBrowserDialog.SelectedPath);
                    comboBoxPath.SelectedItem = folderBrowserDialog.SelectedPath;
                }
            }
        }
    }
}
