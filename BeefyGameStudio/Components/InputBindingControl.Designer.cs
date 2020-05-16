namespace BeefyGameStudio.Components
{
    partial class InputBindingControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.triggerLabel = new System.Windows.Forms.Label();
            this.enabledCheckBox = new System.Windows.Forms.CheckBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.topTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel.SuspendLayout();
            this.topTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // triggerLabel
            // 
            this.triggerLabel.AutoSize = true;
            this.triggerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggerLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.triggerLabel.Location = new System.Drawing.Point(22, 0);
            this.triggerLabel.Name = "triggerLabel";
            this.triggerLabel.Size = new System.Drawing.Size(259, 21);
            this.triggerLabel.TabIndex = 1;
            this.triggerLabel.Text = "Action Name | On ___ being ___ do ___";
            this.triggerLabel.Click += new System.EventHandler(this.triggerLabel_Click);
            // 
            // enabledCheckBox
            // 
            this.enabledCheckBox.AutoSize = true;
            this.enabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.enabledCheckBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enabledCheckBox.Location = new System.Drawing.Point(3, 3);
            this.enabledCheckBox.Name = "enabledCheckBox";
            this.enabledCheckBox.Size = new System.Drawing.Size(13, 15);
            this.enabledCheckBox.TabIndex = 2;
            this.enabledCheckBox.UseVisualStyleBackColor = true;
            this.enabledCheckBox.CheckedChanged += new System.EventHandler(this.enabledCheckBox_CheckedChanged);
            // 
            // removeButton
            // 
            this.removeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.removeButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeButton.Location = new System.Drawing.Point(3, 30);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(284, 22);
            this.removeButton.TabIndex = 3;
            this.removeButton.Text = "Remove Input Binding";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.topTableLayoutPanel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.removeButton, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(290, 55);
            this.tableLayoutPanel.TabIndex = 4;
            // 
            // topTableLayoutPanel
            // 
            this.topTableLayoutPanel.ColumnCount = 2;
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.042253F));
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 92.95775F));
            this.topTableLayoutPanel.Controls.Add(this.enabledCheckBox, 0, 0);
            this.topTableLayoutPanel.Controls.Add(this.triggerLabel, 1, 0);
            this.topTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.topTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.topTableLayoutPanel.Name = "topTableLayoutPanel";
            this.topTableLayoutPanel.RowCount = 1;
            this.topTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.topTableLayoutPanel.Size = new System.Drawing.Size(284, 21);
            this.topTableLayoutPanel.TabIndex = 5;
            // 
            // InputBindingControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "InputBindingControl";
            this.Size = new System.Drawing.Size(290, 55);
            this.Load += new System.EventHandler(this.InputBinding_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.topTableLayoutPanel.ResumeLayout(false);
            this.topTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label triggerLabel;
        private System.Windows.Forms.CheckBox enabledCheckBox;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel topTableLayoutPanel;
    }
}
