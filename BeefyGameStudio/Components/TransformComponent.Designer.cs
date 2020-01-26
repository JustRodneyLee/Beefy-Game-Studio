namespace BeefyGameStudio.Components
{
    partial class TransformComponent
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
            this.transformComponentGroupBox = new System.Windows.Forms.GroupBox();
            this.isAbstractLabel = new System.Windows.Forms.Label();
            this.coordYLabel = new System.Windows.Forms.Label();
            this.coordXLabel = new System.Windows.Forms.Label();
            this.scaleYLabel = new System.Windows.Forms.Label();
            this.scaleXLabel = new System.Windows.Forms.Label();
            this.rotationLabel = new System.Windows.Forms.Label();
            this.scaleLabel = new System.Windows.Forms.Label();
            this.positionLabel = new System.Windows.Forms.Label();
            this.valueBoxIsAbstract = new BeefyGameStudio.ValueBox();
            this.valueBoxRotation = new BeefyGameStudio.ValueBox();
            this.valueBoxScaleY = new BeefyGameStudio.ValueBox();
            this.valueBoxScaleX = new BeefyGameStudio.ValueBox();
            this.valueBoxCoordsY = new BeefyGameStudio.ValueBox();
            this.valueBoxCoordsX = new BeefyGameStudio.ValueBox();
            this.transformComponentGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // transformComponentGroupBox
            // 
            this.transformComponentGroupBox.Controls.Add(this.valueBoxIsAbstract);
            this.transformComponentGroupBox.Controls.Add(this.isAbstractLabel);
            this.transformComponentGroupBox.Controls.Add(this.valueBoxRotation);
            this.transformComponentGroupBox.Controls.Add(this.coordYLabel);
            this.transformComponentGroupBox.Controls.Add(this.coordXLabel);
            this.transformComponentGroupBox.Controls.Add(this.scaleYLabel);
            this.transformComponentGroupBox.Controls.Add(this.scaleXLabel);
            this.transformComponentGroupBox.Controls.Add(this.valueBoxScaleY);
            this.transformComponentGroupBox.Controls.Add(this.valueBoxScaleX);
            this.transformComponentGroupBox.Controls.Add(this.valueBoxCoordsY);
            this.transformComponentGroupBox.Controls.Add(this.valueBoxCoordsX);
            this.transformComponentGroupBox.Controls.Add(this.rotationLabel);
            this.transformComponentGroupBox.Controls.Add(this.scaleLabel);
            this.transformComponentGroupBox.Controls.Add(this.positionLabel);
            this.transformComponentGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transformComponentGroupBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transformComponentGroupBox.Location = new System.Drawing.Point(1, 1);
            this.transformComponentGroupBox.Name = "transformComponentGroupBox";
            this.transformComponentGroupBox.Size = new System.Drawing.Size(348, 177);
            this.transformComponentGroupBox.TabIndex = 0;
            this.transformComponentGroupBox.TabStop = false;
            this.transformComponentGroupBox.Text = "Transform";
            // 
            // isAbstractLabel
            // 
            this.isAbstractLabel.AutoSize = true;
            this.isAbstractLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isAbstractLabel.Location = new System.Drawing.Point(189, 114);
            this.isAbstractLabel.Name = "isAbstractLabel";
            this.isAbstractLabel.Size = new System.Drawing.Size(70, 16);
            this.isAbstractLabel.TabIndex = 19;
            this.isAbstractLabel.Text = "Is Abstract";
            // 
            // coordYLabel
            // 
            this.coordYLabel.AutoSize = true;
            this.coordYLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.coordYLabel.Location = new System.Drawing.Point(16, 81);
            this.coordYLabel.Name = "coordYLabel";
            this.coordYLabel.Size = new System.Drawing.Size(20, 16);
            this.coordYLabel.TabIndex = 11;
            this.coordYLabel.Text = "Y:";
            // 
            // coordXLabel
            // 
            this.coordXLabel.AutoSize = true;
            this.coordXLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.coordXLabel.Location = new System.Drawing.Point(16, 43);
            this.coordXLabel.Name = "coordXLabel";
            this.coordXLabel.Size = new System.Drawing.Size(19, 16);
            this.coordXLabel.TabIndex = 10;
            this.coordXLabel.Text = "X:";
            // 
            // scaleYLabel
            // 
            this.scaleYLabel.AutoSize = true;
            this.scaleYLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scaleYLabel.Location = new System.Drawing.Point(206, 81);
            this.scaleYLabel.Name = "scaleYLabel";
            this.scaleYLabel.Size = new System.Drawing.Size(20, 16);
            this.scaleYLabel.TabIndex = 9;
            this.scaleYLabel.Text = "Y:";
            // 
            // scaleXLabel
            // 
            this.scaleXLabel.AutoSize = true;
            this.scaleXLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scaleXLabel.Location = new System.Drawing.Point(206, 43);
            this.scaleXLabel.Name = "scaleXLabel";
            this.scaleXLabel.Size = new System.Drawing.Size(19, 16);
            this.scaleXLabel.TabIndex = 8;
            this.scaleXLabel.Text = "X:";
            // 
            // rotationLabel
            // 
            this.rotationLabel.AutoSize = true;
            this.rotationLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rotationLabel.Location = new System.Drawing.Point(6, 114);
            this.rotationLabel.Name = "rotationLabel";
            this.rotationLabel.Size = new System.Drawing.Size(56, 16);
            this.rotationLabel.TabIndex = 2;
            this.rotationLabel.Text = "Rotation";
            // 
            // scaleLabel
            // 
            this.scaleLabel.AutoSize = true;
            this.scaleLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scaleLabel.Location = new System.Drawing.Point(189, 22);
            this.scaleLabel.Name = "scaleLabel";
            this.scaleLabel.Size = new System.Drawing.Size(41, 16);
            this.scaleLabel.TabIndex = 1;
            this.scaleLabel.Text = "Scale";
            // 
            // positionLabel
            // 
            this.positionLabel.AutoSize = true;
            this.positionLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionLabel.Location = new System.Drawing.Point(6, 22);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(77, 16);
            this.positionLabel.TabIndex = 0;
            this.positionLabel.Text = "Coordinates";
            // 
            // valueBoxIsAbstract
            // 
            this.valueBoxIsAbstract.AutoSize = true;
            this.valueBoxIsAbstract.BooleanValue = false;
            this.valueBoxIsAbstract.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueBoxIsAbstract.DataType = BeefyGameStudio.ValueBox.ValueType.Boolean;
            this.valueBoxIsAbstract.Location = new System.Drawing.Point(235, 135);
            this.valueBoxIsAbstract.Margin = new System.Windows.Forms.Padding(13);
            this.valueBoxIsAbstract.MinimumSize = new System.Drawing.Size(30, 10);
            this.valueBoxIsAbstract.Name = "valueBoxIsAbstract";
            this.valueBoxIsAbstract.NumericValue = 0F;
            this.valueBoxIsAbstract.Size = new System.Drawing.Size(85, 25);
            this.valueBoxIsAbstract.StringValue = null;
            this.valueBoxIsAbstract.TabIndex = 20;
            this.valueBoxIsAbstract.Value = false;
            this.valueBoxIsAbstract.ValueChange += new System.EventHandler(this.valueBoxIsAbstract_ValueChange);
            // 
            // valueBoxRotation
            // 
            this.valueBoxRotation.AutoSize = true;
            this.valueBoxRotation.BooleanValue = false;
            this.valueBoxRotation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueBoxRotation.DataType = BeefyGameStudio.ValueBox.ValueType.Number;
            this.valueBoxRotation.Location = new System.Drawing.Point(43, 135);
            this.valueBoxRotation.Margin = new System.Windows.Forms.Padding(8);
            this.valueBoxRotation.MinimumSize = new System.Drawing.Size(80, 23);
            this.valueBoxRotation.Name = "valueBoxRotation";
            this.valueBoxRotation.NumericValue = 0F;
            this.valueBoxRotation.Size = new System.Drawing.Size(85, 25);
            this.valueBoxRotation.StringValue = null;
            this.valueBoxRotation.TabIndex = 18;
            this.valueBoxRotation.Value = 0F;
            this.valueBoxRotation.ValueChange += new System.EventHandler(this.valueBoxRotation_ValueChange);
            // 
            // valueBoxScaleY
            // 
            this.valueBoxScaleY.AutoSize = true;
            this.valueBoxScaleY.BooleanValue = false;
            this.valueBoxScaleY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueBoxScaleY.DataType = BeefyGameStudio.ValueBox.ValueType.Number;
            this.valueBoxScaleY.Location = new System.Drawing.Point(235, 81);
            this.valueBoxScaleY.Margin = new System.Windows.Forms.Padding(5);
            this.valueBoxScaleY.MinimumSize = new System.Drawing.Size(49, 15);
            this.valueBoxScaleY.Name = "valueBoxScaleY";
            this.valueBoxScaleY.NumericValue = 0F;
            this.valueBoxScaleY.Size = new System.Drawing.Size(85, 25);
            this.valueBoxScaleY.StringValue = "";
            this.valueBoxScaleY.TabIndex = 7;
            this.valueBoxScaleY.Value = 0F;
            this.valueBoxScaleY.ValueChange += new System.EventHandler(this.valueBoxScaleY_ValueChange);
            // 
            // valueBoxScaleX
            // 
            this.valueBoxScaleX.AutoSize = true;
            this.valueBoxScaleX.BooleanValue = false;
            this.valueBoxScaleX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueBoxScaleX.DataType = BeefyGameStudio.ValueBox.ValueType.Number;
            this.valueBoxScaleX.Location = new System.Drawing.Point(235, 43);
            this.valueBoxScaleX.Margin = new System.Windows.Forms.Padding(8);
            this.valueBoxScaleX.MinimumSize = new System.Drawing.Size(80, 23);
            this.valueBoxScaleX.Name = "valueBoxScaleX";
            this.valueBoxScaleX.NumericValue = 0F;
            this.valueBoxScaleX.Size = new System.Drawing.Size(85, 25);
            this.valueBoxScaleX.StringValue = "";
            this.valueBoxScaleX.TabIndex = 6;
            this.valueBoxScaleX.Value = 0F;
            this.valueBoxScaleX.ValueChange += new System.EventHandler(this.valueBoxScaleX_ValueChange);
            // 
            // valueBoxCoordsY
            // 
            this.valueBoxCoordsY.AutoSize = true;
            this.valueBoxCoordsY.BooleanValue = false;
            this.valueBoxCoordsY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueBoxCoordsY.DataType = BeefyGameStudio.ValueBox.ValueType.Number;
            this.valueBoxCoordsY.Location = new System.Drawing.Point(43, 81);
            this.valueBoxCoordsY.Margin = new System.Windows.Forms.Padding(8);
            this.valueBoxCoordsY.MinimumSize = new System.Drawing.Size(80, 23);
            this.valueBoxCoordsY.Name = "valueBoxCoordsY";
            this.valueBoxCoordsY.NumericValue = 0F;
            this.valueBoxCoordsY.Size = new System.Drawing.Size(85, 25);
            this.valueBoxCoordsY.StringValue = "";
            this.valueBoxCoordsY.TabIndex = 5;
            this.valueBoxCoordsY.Value = 0F;
            this.valueBoxCoordsY.ValueChange += new System.EventHandler(this.valueBoxCoordsY_ValueChange);
            // 
            // valueBoxCoordsX
            // 
            this.valueBoxCoordsX.AutoSize = true;
            this.valueBoxCoordsX.BooleanValue = false;
            this.valueBoxCoordsX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueBoxCoordsX.DataType = BeefyGameStudio.ValueBox.ValueType.Number;
            this.valueBoxCoordsX.Location = new System.Drawing.Point(43, 43);
            this.valueBoxCoordsX.Margin = new System.Windows.Forms.Padding(5);
            this.valueBoxCoordsX.MinimumSize = new System.Drawing.Size(49, 15);
            this.valueBoxCoordsX.Name = "valueBoxCoordsX";
            this.valueBoxCoordsX.NumericValue = 0F;
            this.valueBoxCoordsX.Size = new System.Drawing.Size(85, 25);
            this.valueBoxCoordsX.StringValue = "";
            this.valueBoxCoordsX.TabIndex = 3;
            this.valueBoxCoordsX.Value = 0F;
            this.valueBoxCoordsX.ValueChange += new System.EventHandler(this.valueBoxCoordsX_ValueChange);
            // 
            // TransformComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.transformComponentGroupBox);
            this.DoubleBuffered = true;
            this.Name = "TransformComponent";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(350, 179);
            this.Load += new System.EventHandler(this.TransformComponent_Load);
            this.Resize += new System.EventHandler(this.TransformComponent_Resize);
            this.transformComponentGroupBox.ResumeLayout(false);
            this.transformComponentGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox transformComponentGroupBox;
        private System.Windows.Forms.Label positionLabel;
        private System.Windows.Forms.Label rotationLabel;
        private System.Windows.Forms.Label scaleLabel;
        private ValueBox valueBoxCoordsX;
        private ValueBox valueBoxCoordsY;
        private ValueBox valueBoxScaleX;
        private ValueBox valueBoxScaleY;
        private System.Windows.Forms.Label coordYLabel;
        private System.Windows.Forms.Label coordXLabel;
        private System.Windows.Forms.Label scaleYLabel;
        private System.Windows.Forms.Label scaleXLabel;
        private ValueBox valueBoxRotation;
        private ValueBox valueBoxIsAbstract;
        private System.Windows.Forms.Label isAbstractLabel;
    }
}
