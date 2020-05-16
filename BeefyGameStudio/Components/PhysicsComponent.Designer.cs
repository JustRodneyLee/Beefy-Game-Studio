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
            this.comboBoxPhysicsType = new System.Windows.Forms.ComboBox();
            this.scaleLabel = new System.Windows.Forms.Label();
            this.colliderLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.valueBoxAffectedByGravity = new BeefyGameStudio.ValueBox();
            this.valueBoxMass = new BeefyGameStudio.ValueBox();
            this.valueBoxGravityAcc = new BeefyGameStudio.ValueBox();
            this.massLabel = new System.Windows.Forms.Label();
            this.valueBox1 = new BeefyGameStudio.ValueBox();
            this.physicsComponentGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // physicsComponentGroupBox
            // 
            this.physicsComponentGroupBox.Controls.Add(this.valueBox1);
            this.physicsComponentGroupBox.Controls.Add(this.massLabel);
            this.physicsComponentGroupBox.Controls.Add(this.valueBoxGravityAcc);
            this.physicsComponentGroupBox.Controls.Add(this.label2);
            this.physicsComponentGroupBox.Controls.Add(this.valueBoxAffectedByGravity);
            this.physicsComponentGroupBox.Controls.Add(this.label1);
            this.physicsComponentGroupBox.Controls.Add(this.colliderLabel);
            this.physicsComponentGroupBox.Controls.Add(this.scaleLabel);
            this.physicsComponentGroupBox.Controls.Add(this.comboBoxPhysicsType);
            this.physicsComponentGroupBox.Controls.Add(this.valueBoxMass);
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
            // comboBoxPhysicsType
            // 
            this.comboBoxPhysicsType.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxPhysicsType.FormattingEnabled = true;
            this.comboBoxPhysicsType.Items.AddRange(new object[] {
            "Static",
            "Rigid Body"});
            this.comboBoxPhysicsType.Location = new System.Drawing.Point(216, 41);
            this.comboBoxPhysicsType.Name = "comboBoxPhysicsType";
            this.comboBoxPhysicsType.Size = new System.Drawing.Size(121, 24);
            this.comboBoxPhysicsType.TabIndex = 1;
            // 
            // scaleLabel
            // 
            this.scaleLabel.AutoSize = true;
            this.scaleLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scaleLabel.Location = new System.Drawing.Point(213, 22);
            this.scaleLabel.Name = "scaleLabel";
            this.scaleLabel.Size = new System.Drawing.Size(90, 16);
            this.scaleLabel.TabIndex = 2;
            this.scaleLabel.Text = "Physics Type:";
            // 
            // colliderLabel
            // 
            this.colliderLabel.AutoSize = true;
            this.colliderLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colliderLabel.Location = new System.Drawing.Point(6, 22);
            this.colliderLabel.Name = "colliderLabel";
            this.colliderLabel.Size = new System.Drawing.Size(175, 16);
            this.colliderLabel.TabIndex = 3;
            this.colliderLabel.Text = "Collider Shape (Click to Edit)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(213, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Affected by Gravity:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(213, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Gravity Acceleration:";
            // 
            // valueBoxAffectedByGravity
            // 
            this.valueBoxAffectedByGravity.AutoSize = true;
            this.valueBoxAffectedByGravity.BooleanValue = false;
            this.valueBoxAffectedByGravity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueBoxAffectedByGravity.DataType = BeefyGameStudio.ValueBox.ValueType.Boolean;
            this.valueBoxAffectedByGravity.Location = new System.Drawing.Point(216, 142);
            this.valueBoxAffectedByGravity.Margin = new System.Windows.Forms.Padding(8);
            this.valueBoxAffectedByGravity.MinimumSize = new System.Drawing.Size(80, 23);
            this.valueBoxAffectedByGravity.Name = "valueBoxAffectedByGravity";
            this.valueBoxAffectedByGravity.NumericValue = 0F;
            this.valueBoxAffectedByGravity.Size = new System.Drawing.Size(124, 24);
            this.valueBoxAffectedByGravity.StringValue = null;
            this.valueBoxAffectedByGravity.TabIndex = 5;
            this.valueBoxAffectedByGravity.Value = false;
            // 
            // valueBoxMass
            // 
            this.valueBoxMass.AutoSize = true;
            this.valueBoxMass.BooleanValue = false;
            this.valueBoxMass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueBoxMass.DataType = BeefyGameStudio.ValueBox.ValueType.Number;
            this.valueBoxMass.Location = new System.Drawing.Point(216, 89);
            this.valueBoxMass.Margin = new System.Windows.Forms.Padding(5);
            this.valueBoxMass.MinimumSize = new System.Drawing.Size(49, 15);
            this.valueBoxMass.Name = "valueBoxMass";
            this.valueBoxMass.NumericValue = 0F;
            this.valueBoxMass.Size = new System.Drawing.Size(124, 24);
            this.valueBoxMass.StringValue = null;
            this.valueBoxMass.TabIndex = 0;
            this.valueBoxMass.Value = 0F;
            // 
            // valueBoxGravityAcc
            // 
            this.valueBoxGravityAcc.AutoSize = true;
            this.valueBoxGravityAcc.BooleanValue = false;
            this.valueBoxGravityAcc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueBoxGravityAcc.DataType = BeefyGameStudio.ValueBox.ValueType.Number;
            this.valueBoxGravityAcc.Location = new System.Drawing.Point(216, 198);
            this.valueBoxGravityAcc.Margin = new System.Windows.Forms.Padding(8);
            this.valueBoxGravityAcc.MinimumSize = new System.Drawing.Size(80, 23);
            this.valueBoxGravityAcc.Name = "valueBoxGravityAcc";
            this.valueBoxGravityAcc.NumericValue = 0F;
            this.valueBoxGravityAcc.Size = new System.Drawing.Size(124, 24);
            this.valueBoxGravityAcc.StringValue = null;
            this.valueBoxGravityAcc.TabIndex = 7;
            this.valueBoxGravityAcc.Value = 0F;
            // 
            // massLabel
            // 
            this.massLabel.AutoSize = true;
            this.massLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.massLabel.Location = new System.Drawing.Point(213, 68);
            this.massLabel.Name = "massLabel";
            this.massLabel.Size = new System.Drawing.Size(40, 16);
            this.massLabel.TabIndex = 8;
            this.massLabel.Text = "Mass";
            // 
            // valueBox1
            // 
            this.valueBox1.AutoSize = true;
            this.valueBox1.BooleanValue = false;
            this.valueBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueBox1.DataType = BeefyGameStudio.ValueBox.ValueType.String;
            this.valueBox1.Location = new System.Drawing.Point(47, 160);
            this.valueBox1.Margin = new System.Windows.Forms.Padding(5);
            this.valueBox1.MinimumSize = new System.Drawing.Size(49, 15);
            this.valueBox1.Name = "valueBox1";
            this.valueBox1.NumericValue = 0F;
            this.valueBox1.Size = new System.Drawing.Size(124, 24);
            this.valueBox1.StringValue = null;
            this.valueBox1.TabIndex = 9;
            this.valueBox1.Value = null;
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox physicsComponentGroupBox;
        private System.Windows.Forms.CheckBox enabledCheckBox;
        private ValueBox valueBoxMass;
        private System.Windows.Forms.ComboBox comboBoxPhysicsType;
        private System.Windows.Forms.Label scaleLabel;
        private System.Windows.Forms.Label colliderLabel;
        private ValueBox valueBoxAffectedByGravity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ValueBox valueBoxGravityAcc;
        private ValueBox valueBox1;
        private System.Windows.Forms.Label massLabel;
    }
}
