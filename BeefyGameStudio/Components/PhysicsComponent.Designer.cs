namespace BeefyGameStudio.Components
{
    partial class PhysicsComponent
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
            this.physicsComponentGroupBox = new System.Windows.Forms.GroupBox();
            this.enabledCheckBox = new System.Windows.Forms.CheckBox();
            this.physicsComponentGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // physicsComponentGroupBox
            //             
            this.physicsComponentGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.physicsComponentGroupBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.physicsComponentGroupBox.Location = new System.Drawing.Point(1, 1);
            this.physicsComponentGroupBox.Name = "physicsComponentGroupBox";
            this.physicsComponentGroupBox.Size = new System.Drawing.Size(348, 228);
            this.physicsComponentGroupBox.TabIndex = 0;
            this.physicsComponentGroupBox.TabStop = false;
            this.physicsComponentGroupBox.Text = "  Physics";
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
            // PhysicsComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.enabledCheckBox);
            this.Controls.Add(this.physicsComponentGroupBox);
            this.Name = "PhysicsComponent";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(350, 230);
            this.Load += new System.EventHandler(this.PhysicsComponent_Load);
            this.physicsComponentGroupBox.ResumeLayout(false);
            this.physicsComponentGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox physicsComponentGroupBox;
        private System.Windows.Forms.CheckBox enabledCheckBox;
    }
}
