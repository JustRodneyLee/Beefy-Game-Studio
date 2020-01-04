namespace BeefyGameStudio
{
    partial class ProjectSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectSettings));
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.projectNameLabel = new System.Windows.Forms.Label();
            this.projectNameTextBox = new System.Windows.Forms.TextBox();
            this.projectDeveloperLabel = new System.Windows.Forms.Label();
            this.projectDeveloperTextBox = new System.Windows.Forms.TextBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.fullContentLoadCheckBox = new System.Windows.Forms.CheckBox();
            this.versionMajorTextBox = new System.Windows.Forms.TextBox();
            this.versionMinorTextBox = new System.Windows.Forms.TextBox();
            this.versionBuildTextBox = new System.Windows.Forms.TextBox();
            this.versionRevisionTextBox = new System.Windows.Forms.TextBox();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.developerModeCheckBox = new System.Windows.Forms.CheckBox();
            this.startLevelComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.settingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logoPictureBox.Image = global::BeefyGameStudio.Properties.Resources.BGS;
            this.logoPictureBox.Location = new System.Drawing.Point(12, 12);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(128, 128);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPictureBox.TabIndex = 0;
            this.logoPictureBox.TabStop = false;
            this.logoPictureBox.DoubleClick += new System.EventHandler(this.logoPictureBox_DoubleClick);
            // 
            // projectNameLabel
            // 
            this.projectNameLabel.AutoSize = true;
            this.projectNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectNameLabel.Location = new System.Drawing.Point(146, 9);
            this.projectNameLabel.Name = "projectNameLabel";
            this.projectNameLabel.Size = new System.Drawing.Size(99, 18);
            this.projectNameLabel.TabIndex = 1;
            this.projectNameLabel.Text = "Project Name";
            // 
            // projectNameTextBox
            // 
            this.projectNameTextBox.Location = new System.Drawing.Point(149, 30);
            this.projectNameTextBox.Name = "projectNameTextBox";
            this.projectNameTextBox.Size = new System.Drawing.Size(130, 21);
            this.projectNameTextBox.TabIndex = 2;
            // 
            // projectDeveloperLabel
            // 
            this.projectDeveloperLabel.AutoSize = true;
            this.projectDeveloperLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectDeveloperLabel.Location = new System.Drawing.Point(146, 54);
            this.projectDeveloperLabel.Name = "projectDeveloperLabel";
            this.projectDeveloperLabel.Size = new System.Drawing.Size(126, 18);
            this.projectDeveloperLabel.TabIndex = 3;
            this.projectDeveloperLabel.Text = "Project Developer";
            // 
            // projectDeveloperTextBox
            // 
            this.projectDeveloperTextBox.Location = new System.Drawing.Point(149, 75);
            this.projectDeveloperTextBox.Name = "projectDeveloperTextBox";
            this.projectDeveloperTextBox.Size = new System.Drawing.Size(130, 21);
            this.projectDeveloperTextBox.TabIndex = 4;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLabel.Location = new System.Drawing.Point(146, 99);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(109, 18);
            this.VersionLabel.TabIndex = 5;
            this.VersionLabel.Text = "Project Version";
            // 
            // fullContentLoadCheckBox
            // 
            this.fullContentLoadCheckBox.AutoSize = true;
            this.fullContentLoadCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fullContentLoadCheckBox.Location = new System.Drawing.Point(6, 21);
            this.fullContentLoadCheckBox.Name = "fullContentLoadCheckBox";
            this.fullContentLoadCheckBox.Size = new System.Drawing.Size(109, 19);
            this.fullContentLoadCheckBox.TabIndex = 7;
            this.fullContentLoadCheckBox.Text = "Partial Loading";
            this.fullContentLoadCheckBox.UseVisualStyleBackColor = true;
            this.fullContentLoadCheckBox.CheckedChanged += new System.EventHandler(this.fullContentLoadCheckBox_CheckedChanged);
            // 
            // versionMajorTextBox
            // 
            this.versionMajorTextBox.Location = new System.Drawing.Point(149, 120);
            this.versionMajorTextBox.Name = "versionMajorTextBox";
            this.versionMajorTextBox.Size = new System.Drawing.Size(28, 21);
            this.versionMajorTextBox.TabIndex = 8;
            this.versionMajorTextBox.TextChanged += new System.EventHandler(this.versionMajorTextBox_TextChanged);
            // 
            // versionMinorTextBox
            // 
            this.versionMinorTextBox.Location = new System.Drawing.Point(183, 120);
            this.versionMinorTextBox.Name = "versionMinorTextBox";
            this.versionMinorTextBox.Size = new System.Drawing.Size(28, 21);
            this.versionMinorTextBox.TabIndex = 9;
            this.versionMinorTextBox.TextChanged += new System.EventHandler(this.versionMinorTextBox_TextChanged);
            // 
            // versionBuildTextBox
            // 
            this.versionBuildTextBox.Location = new System.Drawing.Point(217, 120);
            this.versionBuildTextBox.Name = "versionBuildTextBox";
            this.versionBuildTextBox.Size = new System.Drawing.Size(28, 21);
            this.versionBuildTextBox.TabIndex = 10;
            this.versionBuildTextBox.TextChanged += new System.EventHandler(this.versionBuildTextBox_TextChanged);
            // 
            // versionRevisionTextBox
            // 
            this.versionRevisionTextBox.Location = new System.Drawing.Point(251, 120);
            this.versionRevisionTextBox.Name = "versionRevisionTextBox";
            this.versionRevisionTextBox.Size = new System.Drawing.Size(28, 21);
            this.versionRevisionTextBox.TabIndex = 11;
            this.versionRevisionTextBox.TextChanged += new System.EventHandler(this.versionRevisionTextBox_TextChanged);
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.label1);
            this.settingsGroupBox.Controls.Add(this.startLevelComboBox);
            this.settingsGroupBox.Controls.Add(this.developerModeCheckBox);
            this.settingsGroupBox.Controls.Add(this.fullContentLoadCheckBox);
            this.settingsGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsGroupBox.Location = new System.Drawing.Point(12, 147);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Size = new System.Drawing.Size(268, 323);
            this.settingsGroupBox.TabIndex = 12;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "In-Game Settings";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(205, 476);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Enabled = false;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(124, 476);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 14;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // developerModeCheckBox
            // 
            this.developerModeCheckBox.AutoSize = true;
            this.developerModeCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.developerModeCheckBox.Location = new System.Drawing.Point(145, 21);
            this.developerModeCheckBox.Name = "developerModeCheckBox";
            this.developerModeCheckBox.Size = new System.Drawing.Size(117, 19);
            this.developerModeCheckBox.TabIndex = 8;
            this.developerModeCheckBox.Text = "Developer Mode";
            this.developerModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // startLevelComboBox
            // 
            this.startLevelComboBox.FormattingEnabled = true;
            this.startLevelComboBox.Location = new System.Drawing.Point(94, 293);
            this.startLevelComboBox.Name = "startLevelComboBox";
            this.startLevelComboBox.Size = new System.Drawing.Size(168, 24);
            this.startLevelComboBox.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 296);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Start-up Level";
            // 
            // ProjectSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 511);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.settingsGroupBox);
            this.Controls.Add(this.versionRevisionTextBox);
            this.Controls.Add(this.versionBuildTextBox);
            this.Controls.Add(this.versionMinorTextBox);
            this.Controls.Add(this.versionMajorTextBox);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.projectDeveloperTextBox);
            this.Controls.Add(this.projectDeveloperLabel);
            this.Controls.Add(this.projectNameTextBox);
            this.Controls.Add(this.projectNameLabel);
            this.Controls.Add(this.logoPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProjectSettings_FormClosing);
            this.Load += new System.EventHandler(this.ProjectSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label projectNameLabel;
        private System.Windows.Forms.TextBox projectNameTextBox;
        private System.Windows.Forms.Label projectDeveloperLabel;
        private System.Windows.Forms.TextBox projectDeveloperTextBox;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.CheckBox fullContentLoadCheckBox;
        private System.Windows.Forms.TextBox versionMajorTextBox;
        private System.Windows.Forms.TextBox versionMinorTextBox;
        private System.Windows.Forms.TextBox versionBuildTextBox;
        private System.Windows.Forms.TextBox versionRevisionTextBox;
        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox startLevelComboBox;
        private System.Windows.Forms.CheckBox developerModeCheckBox;
        private System.Windows.Forms.Label label1;
    }
}