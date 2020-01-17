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
    public partial class Renderer2DComponent : UserControl
    {
        BeefyRenderer2D renderer;

        public Renderer2DComponent(BeefyRenderer2D br2d)
        {
            Name = "Renderer2DComponent";
            renderer = br2d;
            InitializeComponent();
        }
    }
}
