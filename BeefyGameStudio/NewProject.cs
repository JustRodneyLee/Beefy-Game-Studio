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
        public string ProjPath { get; set; }
        public string ProjName { get; set; }

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
            CreateProject();
        }        

        private void NewProject_Load(object sender, EventArgs e)
        {
            comboBoxPath.Items.Add("<Browse...>");
            Focus();
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

        private void CreateProject()
        {
            if (textBoxName.Text.Replace(" ", "") != "")
            {
                if (textBoxName.Text.Contains("|") || textBoxName.Text.Contains("\\") || textBoxName.Text.Contains("/") || textBoxName.Text.Contains("?") || textBoxName.Text.Contains("*") || textBoxName.Text.Contains(":") || textBoxName.Text.Contains("<") || textBoxName.Text.Contains(">") || textBoxName.Text.Contains("\"")) //Illegal characters
                {
                    MessageBox.Show("Project name contains illegal characters!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (comboBoxPath.SelectedItem is null)
                        MessageBox.Show("Please select a valid path!");
                    else
                    {
                        try
                        {
                            Path.GetFullPath(comboBoxPath.SelectedItem.ToString());
                            if (Directory.Exists(comboBoxPath.SelectedItem.ToString() + "\\" + textBoxName.Text))
                            {
                                MessageBox.Show("Folder already exists!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DialogResult = DialogResult.OK;
                                ProjName = textBoxName.Text;
                                ProjPath = comboBoxPath.SelectedItem.ToString() + "\\" + ProjName;
                                Close();
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Please select a valid path!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }                    
                }
            }
            else
            {
                MessageBox.Show("Project name cannot be empty!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == (char)Keys.Enter)
                CreateProject();
        }

        private void comboBoxPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                CreateProject();
        }
    }
}
