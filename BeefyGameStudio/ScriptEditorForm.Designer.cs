namespace BeefyGameStudio
{
    partial class ScriptEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptEditorForm));
            this.scriptEditor = new BeefyGameStudio.ScriptEditor.ScriptEditor();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.SuspendLayout();
            // 
            // scriptEditor
            // 
            this.scriptEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptEditor.Location = new System.Drawing.Point(0, 24);
            this.scriptEditor.MouseHoverUpdatesOnly = false;
            this.scriptEditor.Name = "scriptEditor";
            this.scriptEditor.Size = new System.Drawing.Size(496, 657);
            this.scriptEditor.TabIndex = 0;
            this.scriptEditor.Text = "Script Editor";
            // 
            // MainMenu
            // 
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(496, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // ScriptEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 681);
            this.Controls.Add(this.scriptEditor);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "ScriptEditorForm";
            this.Text = "Beefy Game Studio - Script Editor";
            this.Load += new System.EventHandler(this.ScriptEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScriptEditor.ScriptEditor scriptEditor;
        private System.Windows.Forms.MenuStrip MainMenu;
    }
}