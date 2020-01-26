namespace BeefyGameStudio.Components
{
    partial class Renderer2DComponent
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
            this.enabledCheckBox = new System.Windows.Forms.CheckBox();
            this.rendererComponentGroupBox = new System.Windows.Forms.GroupBox();
            this.pictureBoxTexture = new System.Windows.Forms.PictureBox();
            this.rendererComponentGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTexture)).BeginInit();
            this.SuspendLayout();
            // 
            // enabledCheckBox
            // 
            this.enabledCheckBox.AutoSize = true;
            this.enabledCheckBox.Checked = true;
            this.enabledCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enabledCheckBox.Location = new System.Drawing.Point(3, 3);
            this.enabledCheckBox.Name = "enabledCheckBox";
            this.enabledCheckBox.Size = new System.Drawing.Size(15, 14);
            this.enabledCheckBox.TabIndex = 1;
            this.enabledCheckBox.UseVisualStyleBackColor = true;
            this.enabledCheckBox.CheckedChanged += new System.EventHandler(this.enabledCheckBox_CheckedChanged);
            // 
            // rendererComponentGroupBox
            // 
            this.rendererComponentGroupBox.Controls.Add(this.pictureBoxTexture);
            this.rendererComponentGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rendererComponentGroupBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rendererComponentGroupBox.Location = new System.Drawing.Point(0, 0);
            this.rendererComponentGroupBox.Name = "rendererComponentGroupBox";
            this.rendererComponentGroupBox.Size = new System.Drawing.Size(350, 179);
            this.rendererComponentGroupBox.TabIndex = 2;
            this.rendererComponentGroupBox.TabStop = false;
            this.rendererComponentGroupBox.Text = "  Renderer 2D";
            // 
            // pictureBoxTexture
            // 
            this.pictureBoxTexture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxTexture.Location = new System.Drawing.Point(6, 23);
            this.pictureBoxTexture.Name = "pictureBoxTexture";
            this.pictureBoxTexture.Size = new System.Drawing.Size(128, 128);
            this.pictureBoxTexture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTexture.TabIndex = 0;
            this.pictureBoxTexture.TabStop = false;
            // 
            // Renderer2DComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.enabledCheckBox);
            this.Controls.Add(this.rendererComponentGroupBox);
            this.Name = "Renderer2DComponent";
            this.Size = new System.Drawing.Size(350, 179);
            this.Load += new System.EventHandler(this.Renderer2DComponent_Load);
            this.rendererComponentGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTexture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox enabledCheckBox;
        private System.Windows.Forms.GroupBox rendererComponentGroupBox;
        private System.Windows.Forms.PictureBox pictureBoxTexture;
    }
}
