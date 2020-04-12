namespace BeefyGameStudio
{
    partial class ScriptDocument
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptDocument));
            this.scriptEditor = new BeefyGameStudio.ScriptEditor.ScriptEditor();
            this.SuspendLayout();
            // 
            // scriptEditor
            // 
            this.scriptEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptEditor.Location = new System.Drawing.Point(0, 0);
            this.scriptEditor.MouseHoverUpdatesOnly = false;
            this.scriptEditor.Name = "scriptEditor";
            this.scriptEditor.Script = null;
            this.scriptEditor.Size = new System.Drawing.Size(800, 450);
            this.scriptEditor.TabIndex = 0;
            this.scriptEditor.Text = "scriptDocument";
            this.scriptEditor.Click += new System.EventHandler(this.scriptEditor_Click);
            // 
            // ScriptDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.scriptEditor);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScriptDocument";
            this.ShowInTaskbar = false;
            this.Text = "New Script Document";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptDocument_FormClosing);
            this.Load += new System.EventHandler(this.ScriptDocument_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ScriptEditor.ScriptEditor scriptEditor;
    }
}