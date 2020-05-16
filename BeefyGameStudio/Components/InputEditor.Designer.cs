namespace BeefyGameStudio.Components
{
    partial class InputEditor
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Keyboard");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Mouse");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputEditor));
            this.inputsTreeView = new System.Windows.Forms.TreeView();
            this.conditionComboBox = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.inputNameTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.conditionLabel = new System.Windows.Forms.Label();
            this.inputLabel = new System.Windows.Forms.Label();
            this.actionLabel = new System.Windows.Forms.Label();
            this.actionsListBox = new System.Windows.Forms.ListBox();
            this.scriptComboBox = new System.Windows.Forms.ComboBox();
            this.scriptLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // inputsTreeView
            // 
            this.inputsTreeView.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputsTreeView.Location = new System.Drawing.Point(12, 30);
            this.inputsTreeView.Name = "inputsTreeView";
            treeNode1.Name = "Keyboard";
            treeNode1.Text = "Keyboard";
            treeNode2.Name = "Mouse";
            treeNode2.Text = "Mouse";
            this.inputsTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.inputsTreeView.Size = new System.Drawing.Size(171, 296);
            this.inputsTreeView.TabIndex = 0;
            this.inputsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.inputsTreeView_AfterSelect);
            // 
            // conditionComboBox
            // 
            this.conditionComboBox.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conditionComboBox.FormattingEnabled = true;
            this.conditionComboBox.Location = new System.Drawing.Point(190, 80);
            this.conditionComboBox.Name = "conditionComboBox";
            this.conditionComboBox.Size = new System.Drawing.Size(122, 25);
            this.conditionComboBox.TabIndex = 1;
            this.conditionComboBox.SelectedIndexChanged += new System.EventHandler(this.conditionComboBox_SelectedIndexChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(190, 296);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(121, 30);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmButton.Location = new System.Drawing.Point(190, 260);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(121, 30);
            this.confirmButton.TabIndex = 3;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // inputNameTextBox
            // 
            this.inputNameTextBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputNameTextBox.Location = new System.Drawing.Point(190, 30);
            this.inputNameTextBox.Name = "inputNameTextBox";
            this.inputNameTextBox.Size = new System.Drawing.Size(121, 26);
            this.inputNameTextBox.TabIndex = 4;
            this.inputNameTextBox.TextChanged += new System.EventHandler(this.inputNameTextBox_TextChanged);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(189, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(86, 18);
            this.nameLabel.TabIndex = 5;
            this.nameLabel.Text = "Input Name";
            // 
            // conditionLabel
            // 
            this.conditionLabel.AutoSize = true;
            this.conditionLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conditionLabel.Location = new System.Drawing.Point(189, 59);
            this.conditionLabel.Name = "conditionLabel";
            this.conditionLabel.Size = new System.Drawing.Size(111, 18);
            this.conditionLabel.TabIndex = 6;
            this.conditionLabel.Text = "Input Condition";
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputLabel.Location = new System.Drawing.Point(12, 9);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(105, 18);
            this.inputLabel.TabIndex = 7;
            this.inputLabel.Text = "Input Mapping";
            // 
            // actionLabel
            // 
            this.actionLabel.AutoSize = true;
            this.actionLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actionLabel.Location = new System.Drawing.Point(189, 157);
            this.actionLabel.Name = "actionLabel";
            this.actionLabel.Size = new System.Drawing.Size(87, 18);
            this.actionLabel.TabIndex = 8;
            this.actionLabel.Text = "Input Action";
            // 
            // actionsListBox
            // 
            this.actionsListBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actionsListBox.FormattingEnabled = true;
            this.actionsListBox.ItemHeight = 14;
            this.actionsListBox.Location = new System.Drawing.Point(190, 178);
            this.actionsListBox.Name = "actionsListBox";
            this.actionsListBox.Size = new System.Drawing.Size(120, 74);
            this.actionsListBox.TabIndex = 9;
            this.actionsListBox.SelectedIndexChanged += new System.EventHandler(this.actionsListBox_SelectedIndexChanged);
            // 
            // scriptComboBox
            // 
            this.scriptComboBox.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scriptComboBox.FormattingEnabled = true;
            this.scriptComboBox.Location = new System.Drawing.Point(190, 129);
            this.scriptComboBox.Name = "scriptComboBox";
            this.scriptComboBox.Size = new System.Drawing.Size(122, 25);
            this.scriptComboBox.TabIndex = 10;
            this.scriptComboBox.Text = "Choose Script...";
            this.scriptComboBox.SelectedIndexChanged += new System.EventHandler(this.scriptComboBox_SelectedIndexChanged);
            // 
            // scriptLabel
            // 
            this.scriptLabel.AutoSize = true;
            this.scriptLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scriptLabel.Location = new System.Drawing.Point(189, 108);
            this.scriptLabel.Name = "scriptLabel";
            this.scriptLabel.Size = new System.Drawing.Size(85, 18);
            this.scriptLabel.TabIndex = 11;
            this.scriptLabel.Text = "Input Script";
            // 
            // InputEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 338);
            this.Controls.Add(this.scriptLabel);
            this.Controls.Add(this.scriptComboBox);
            this.Controls.Add(this.actionsListBox);
            this.Controls.Add(this.actionLabel);
            this.Controls.Add(this.inputLabel);
            this.Controls.Add(this.conditionLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.inputNameTextBox);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.conditionComboBox);
            this.Controls.Add(this.inputsTreeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input Editor";
            this.Load += new System.EventHandler(this.InputEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView inputsTreeView;
        private System.Windows.Forms.ComboBox conditionComboBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.TextBox inputNameTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label conditionLabel;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.Label actionLabel;
        private System.Windows.Forms.ListBox actionsListBox;
        private System.Windows.Forms.ComboBox scriptComboBox;
        private System.Windows.Forms.Label scriptLabel;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}