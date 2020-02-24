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
            this.SuspendLayout();
            // 
            // scriptEditor
            // 
            this.scriptEditor.Location = new System.Drawing.Point(12, 12);
            this.scriptEditor.MouseHoverUpdatesOnly = false;
            this.scriptEditor.Name = "scriptEditor";
            this.scriptEditor.Size = new System.Drawing.Size(472, 384);
            this.scriptEditor.TabIndex = 0;
            this.scriptEditor.Text = "Script Editor";
            // 
            // ScriptEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 681);
            this.Controls.Add(this.scriptEditor);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScriptEditorForm";
            this.Text = "Beefy Game Studio - Script Editor";
            this.Load += new System.EventHandler(this.ScriptEditor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ScriptEditor.ScriptEditor scriptEditor;
    }
}