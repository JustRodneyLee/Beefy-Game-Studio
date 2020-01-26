using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeefyEngine;

namespace BeefyGameStudio.Components
{
    public partial class Renderer2DComponent : UserControl, InspectorComponent
    {
        BeefyRenderer2D renderer;

        public Renderer2DComponent(BeefyRenderer2D br2d)
        {
            Name = "Renderer2DComponent";
            renderer = br2d;
            InitializeComponent();
        }

        public void TransferParameters()
        {
            
        }

        public void UpdateParameters()
        {
            enabledCheckBox.Checked = renderer.Enabled;
            rendererComponentGroupBox.Enabled = renderer.Enabled;
        }

        private void Renderer2DComponent_Load(object sender, EventArgs e)
        {
            //pictureBoxTexture.Image. = renderer.Texture;
        }

        private void enabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (enabledCheckBox.Checked)
            {
                renderer.Enable();
            }
            else
            {
                renderer.Disable();
            }
            rendererComponentGroupBox.Enabled = enabledCheckBox.Checked;
        }
    }
}
