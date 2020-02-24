using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeefyGameStudio
{
    public partial class ProjectSettings : Form
    {
        BeefyProject projClone;

        public ProjectSettings()
        {
            InitializeComponent();
            projClone = (BeefyProject)CurrentProject.Data.Clone();
        }

        private void ProjectSettings_Load(object sender, EventArgs e)
        {
            projectNameTextBox.Text = projClone.ProjectName;
            if (projClone.ProjectDevelopers.Count > 1)
            {
                projectDeveloperLabel.Text = "Project Developers";
            }
            else
            {
                projectDeveloperLabel.Text = "Project Developer";
            }
            foreach (string developer in projClone.ProjectDevelopers)
            {
                projectDeveloperTextBox.Text += developer + ";";
            }
            if (projClone.ProjectLogoPath=="")
                logoPictureBox.Image = Properties.Resources.BGS;
            else
                logoPictureBox.Image = Image.FromFile(projClone.ProjectLogoPath);         
            versionMajorTextBox.Text = projClone.ProjectVersion.Major.ToString();
            versionMinorTextBox.Text = projClone.ProjectVersion.Minor.ToString();
            versionRevisionTextBox.Text = projClone.ProjectVersion.Revision.ToString();
            versionBuildTextBox.Text = projClone.ProjectVersion.Build.ToString();
            partialLoadingCheckBox.Checked = projClone.PartialLoading;
            developerModeCheckBox.Checked = projClone.DeveloperMode;
        }

        private void logoPictureBox_DoubleClick(object sender, EventArgs e)
        {
            //TODO
        }

        private void fullContentLoadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            projClone.PartialLoading = partialLoadingCheckBox.Checked;
        }

        private void ProjectSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO
        }

        private void versionMajorTextBox_TextChanged(object sender, EventArgs e)
        {
            Regex.Replace(versionMajorTextBox.Text, "([a-zA-Z,_ ]+|(?<=[a-zA-Z ])[/-])", "");
        }

        private void versionMinorTextBox_TextChanged(object sender, EventArgs e)
        {
            Regex.Replace(versionMinorTextBox.Text, "([a-zA-Z,_ ]+|(?<=[a-zA-Z ])[/-])", "");
        }

        private void versionBuildTextBox_TextChanged(object sender, EventArgs e)
        {
            Regex.Replace(versionBuildTextBox.Text, "([a-zA-Z,_ ]+|(?<=[a-zA-Z ])[/-])", "");
        }

        private void versionRevisionTextBox_TextChanged(object sender, EventArgs e)
        {
            Regex.Replace(versionRevisionTextBox.Text, "([a-zA-Z,_ ]+|(?<=[a-zA-Z ])[/-])", "");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            projClone.Dispose();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            CurrentProject.SetProjectData(projClone);
            //TODO : Change Directories
        }
    }
}
