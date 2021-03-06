﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using BeefyGameEngine;

namespace BeefyGameStudio.Components
{
    public partial class InputComponent : UserControl
    {
        public BeefyInputController controller;

        public InputComponent(BeefyInputController bic)
        {
            Name = "InputComponent";
            controller = bic;
            InitializeComponent();            
        }

        private void enabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (enabledCheckBox.Checked)
            {
                controller.Enable();                
            }
            else
            {
                controller.Disable();
            }
            inputComponentGroupBox.Enabled = enabledCheckBox.Checked;
        }

        private void InputComponent_Load(object sender, EventArgs e)
        {
            Width = Parent.Width - 2 * Parent.Margin.Horizontal;
            bindingsPanel.HorizontalScroll.Enabled = false;
        }

        private void AddInputBinding()
        {
            InputBindingControl inpBinding = new InputBindingControl(this);
            inpBinding.Dock = DockStyle.Top;
            controller.AddInputBinding(inpBinding.BIB);
            bindingsPanel.Controls.Add(inpBinding);
        }

        private void addInputBindingButton_Click(object sender, EventArgs e)
        {
            AddInputBinding();
        }
    }
}
