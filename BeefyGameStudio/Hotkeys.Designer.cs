namespace BeefyGameStudio
{
    partial class Hotkeys
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hotkeys));
            this.listViewHotkeys = new System.Windows.Forms.ListView();
            this.Function = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hotkey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewHotkeys
            // 
            this.listViewHotkeys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Function,
            this.Hotkey});
            this.listViewHotkeys.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewHotkeys.HideSelection = false;
            this.listViewHotkeys.Location = new System.Drawing.Point(12, 12);
            this.listViewHotkeys.Name = "listViewHotkeys";
            this.listViewHotkeys.Size = new System.Drawing.Size(285, 437);
            this.listViewHotkeys.TabIndex = 0;
            this.listViewHotkeys.UseCompatibleStateImageBehavior = false;
            this.listViewHotkeys.View = System.Windows.Forms.View.Details;
            // 
            // Function
            // 
            this.Function.Text = "Function";
            this.Function.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Function.Width = 145;
            // 
            // Hotkey
            // 
            this.Hotkey.Text = "Hotkey";
            this.Hotkey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Hotkey.Width = 135;
            // 
            // Hotkeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 461);
            this.Controls.Add(this.listViewHotkeys);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Hotkeys";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Beefy Game Studio - Hotkeys";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewHotkeys;
        private System.Windows.Forms.ColumnHeader Function;
        private System.Windows.Forms.ColumnHeader Hotkey;
    }
}