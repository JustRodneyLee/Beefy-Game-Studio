using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeefyGameEngine;

namespace BeefyGameStudio.Components
{
    public partial class LightingComponent : UserControl
    {
        BeefyLighting lighting;

        public LightingComponent(BeefyLighting cmp)
        {
            Name = "LightingComponent";
            lighting = cmp;
            InitializeComponent();
        }
    }
}
