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
    public partial class ProjectSettings : Form
    {
        BeefyProject project;

        public ProjectSettings(BeefyProject proj)
        {
            InitializeComponent();
            project = proj;
        }

        private void ProjectSettings_Load(object sender, EventArgs e)
        {
            projectNameTextBox.Text = project.ProjectName;
            foreach (string developer in project.ProjectDevelopers)
            {
                projectDeveloperTextBox.Text += developer + ";";
            }
            projectDeveloperTextBox.Text.Trim(';');
            //projectDeveloperTextBox.Text;
        }

        private void logoPictureBox_DoubleClick(object sender, EventArgs e)
        {
            //TODO : Change Logo
        }

        private void fullContentLoadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            project.PartialLoading = fullContentLoadCheckBox.Checked;
        }
    }
}
