using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using BeefyEngine;

namespace BeefyGameStudio.Components
{
    public partial class PhysicsComponent : UserControl, InspectorComponent
    {
        public PhysicsComponent()
        {
            InitializeComponent();
        }

        public PhysicsComponent(BeefyPhysics bp)
        {
            Name = "PhysicsComponent";
            InitializeComponent();
        }

        public void UpdateParameters()
        {
        }

        public void TransferParameters()
        {
        }

        private void TransformComponent_Load(object sender, EventArgs e)
        {
        }

        private void enabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
