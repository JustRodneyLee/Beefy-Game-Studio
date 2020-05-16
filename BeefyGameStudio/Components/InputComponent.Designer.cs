namespace BeefyGameStudio.Components
{
    partial class InputComponent
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
            this.inputComponentGroupBox = new System.Windows.Forms.GroupBox();
            this.addInputBindingButton = new System.Windows.Forms.Button();
            this.enabledCheckBox = new System.Windows.Forms.CheckBox();
            this.bindingsPanel = new System.Windows.Forms.Panel();
            this.inputComponentGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputComponentGroupBox
            // 
            this.inputComponentGroupBox.Controls.Add(this.bindingsPanel);
            this.inputComponentGroupBox.Controls.Add(this.addInputBindingButton);
            this.inputComponentGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputComponentGroupBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputComponentGroupBox.Location = new System.Drawing.Point(0, 0);
            this.inputComponentGroupBox.Name = "inputComponentGroupBox";
            this.inputComponentGroupBox.Size = new System.Drawing.Size(350, 230);
            this.inputComponentGroupBox.TabIndex = 0;
            this.inputComponentGroupBox.TabStop = false;
            this.inputComponentGroupBox.Text = "  Input Controller";
            // 
            // addInputBindingButton
            // 
            this.addInputBindingButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.addInputBindingButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addInputBindingButton.Location = new System.Drawing.Point(3, 22);
            this.addInputBindingButton.Name = "addInputBindingButton";
            this.addInputBindingButton.Size = new System.Drawing.Size(344, 31);
            this.addInputBindingButton.TabIndex = 1;
            this.addInputBindingButton.Text = "Add New Input Binding";
            this.addInputBindingButton.UseVisualStyleBackColor = true;
            this.addInputBindingButton.Click += new System.EventHandler(this.addInputBindingButton_Click);
            // 
            // enabledCheckBox
            // 
            this.enabledCheckBox.AutoSize = true;
            this.enabledCheckBox.Checked = true;
            this.enabledCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enabledCheckBox.Location = new System.Drawing.Point(3, 3);
            this.enabledCheckBox.Name = "enabledCheckBox";
            this.enabledCheckBox.Size = new System.Drawing.Size(15, 14);
            this.enabledCheckBox.TabIndex = 0;
            this.enabledCheckBox.UseVisualStyleBackColor = true;
            this.enabledCheckBox.CheckedChanged += new System.EventHandler(this.enabledCheckBox_CheckedChanged);
            // 
            // bindingsPanel
            // 
            this.bindingsPanel.AutoScroll = true;
            this.bindingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bindingsPanel.Location = new System.Drawing.Point(3, 53);
            this.bindingsPanel.Name = "bindingsPanel";
            this.bindingsPanel.Size = new System.Drawing.Size(344, 174);
            this.bindingsPanel.TabIndex = 2;
            // 
            // InputComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.enabledCheckBox);
            this.Controls.Add(this.inputComponentGroupBox);
            this.Name = "InputComponent";
            this.Size = new System.Drawing.Size(350, 230);
            this.Load += new System.EventHandler(this.InputComponent_Load);
            this.inputComponentGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox inputComponentGroupBox;
        private System.Windows.Forms.CheckBox enabledCheckBox;
        private System.Windows.Forms.Button addInputBindingButton;
        private System.Windows.Forms.Panel bindingsPanel;
    }
}
