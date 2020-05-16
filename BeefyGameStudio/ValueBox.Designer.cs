namespace BeefyGameStudio
{
    partial class ValueBox
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
            this.components = new System.ComponentModel.Container();
            this.decreaseBtn = new System.Windows.Forms.Button();
            this.increaseBtn = new System.Windows.Forms.Button();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.valueBoxPanel = new System.Windows.Forms.Panel();
            this.labelFocus = new System.Windows.Forms.Label();
            this.pressTickTimer = new System.Windows.Forms.Timer(this.components);
            this.valueBoxPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // decreaseBtn
            // 
            this.decreaseBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.decreaseBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.decreaseBtn.Location = new System.Drawing.Point(0, 0);
            this.decreaseBtn.Name = "decreaseBtn";
            this.decreaseBtn.Size = new System.Drawing.Size(35, 33);
            this.decreaseBtn.TabIndex = 0;
            this.decreaseBtn.Text = "<";
            this.decreaseBtn.UseVisualStyleBackColor = true;
            this.decreaseBtn.Visible = false;
            this.decreaseBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DecreaseBtn_MouseDown);
            this.decreaseBtn.MouseLeave += new System.EventHandler(this.DecreaseBtn_MouseLeave);
            this.decreaseBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DecreaseBtn_MouseUp);
            // 
            // increaseBtn
            // 
            this.increaseBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.increaseBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.increaseBtn.Location = new System.Drawing.Point(113, 0);
            this.increaseBtn.Name = "increaseBtn";
            this.increaseBtn.Size = new System.Drawing.Size(35, 33);
            this.increaseBtn.TabIndex = 1;
            this.increaseBtn.Text = ">";
            this.increaseBtn.UseVisualStyleBackColor = true;
            this.increaseBtn.Visible = false;
            this.increaseBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.IncreaseBtn_MouseDown);
            this.increaseBtn.MouseLeave += new System.EventHandler(this.IncreaseBtn_MouseLeave);
            this.increaseBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.IncreaseBtn_MouseUp);
            // 
            // valueTextBox
            // 
            this.valueTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valueTextBox.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.valueTextBox.Location = new System.Drawing.Point(35, 0);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(78, 35);
            this.valueTextBox.TabIndex = 2;
            this.valueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.valueTextBox.Click += new System.EventHandler(this.valueTextBox_Click);
            this.valueTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValueTextBox_KeyPress);
            this.valueTextBox.Leave += new System.EventHandler(this.ValueTextBox_Leave);
            this.valueTextBox.MouseEnter += new System.EventHandler(this.ValueTextBox_MouseEnter);
            this.valueTextBox.MouseLeave += new System.EventHandler(this.ValueTextBox_MouseLeave);
            // 
            // valueBoxPanel
            // 
            this.valueBoxPanel.Controls.Add(this.valueTextBox);
            this.valueBoxPanel.Controls.Add(this.decreaseBtn);
            this.valueBoxPanel.Controls.Add(this.increaseBtn);
            this.valueBoxPanel.Controls.Add(this.labelFocus);
            this.valueBoxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valueBoxPanel.Location = new System.Drawing.Point(0, 0);
            this.valueBoxPanel.Name = "valueBoxPanel";
            this.valueBoxPanel.Size = new System.Drawing.Size(148, 33);
            this.valueBoxPanel.TabIndex = 3;
            // 
            // labelFocus
            // 
            this.labelFocus.AutoSize = true;
            this.labelFocus.Location = new System.Drawing.Point(56, 12);
            this.labelFocus.Name = "labelFocus";
            this.labelFocus.Size = new System.Drawing.Size(0, 12);
            this.labelFocus.TabIndex = 3;
            // 
            // pressTickTimer
            // 
            this.pressTickTimer.Tick += new System.EventHandler(this.PressTickTimer_Tick);
            // 
            // ValueBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.valueBoxPanel);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(30, 10);
            this.Name = "ValueBox";
            this.Size = new System.Drawing.Size(148, 33);
            this.Load += new System.EventHandler(this.ValueBox_Load);
            this.EnabledChanged += new System.EventHandler(this.ValueBox_EnabledChanged);
            this.Resize += new System.EventHandler(this.ValueBox_Resize);
            this.valueBoxPanel.ResumeLayout(false);
            this.valueBoxPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button decreaseBtn;
        private System.Windows.Forms.Button increaseBtn;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.Panel valueBoxPanel;
        private System.Windows.Forms.Timer pressTickTimer;
        private System.Windows.Forms.Label labelFocus;
    }
}
