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
    public partial class PhysicsComponent : UserControl, InspectorComponent
    {
        BeefyPhysics physics;               

        public PhysicsComponent(BeefyPhysics bp)
        {
            Name = "PhysicsComponent";
            physics = bp;
            InitializeComponent();
        }

        public void UpdateParameters()
        {
            //physics.Velocity
        }

        public void TransferParameters()
        {
            //physics.Velocity = 
            
        }

        private void enabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (enabledCheckBox.Checked)
            {
                physics.Enable(); 
            }
            else
            {
                physics.Disable();                
            }
            physicsComponentGroupBox.Enabled = enabledCheckBox.Checked;
        }

        private void PhysicsComponent_Load(object sender, EventArgs e)
        {
            Width = Parent.Width;
        }
    }
}
