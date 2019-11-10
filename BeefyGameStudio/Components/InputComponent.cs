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
    public partial class InputComponent : UserControl
    {
        BeefyInputController controller;

        public InputComponent(BeefyInputController bic)
        {
            Name = "InputComponent";
            controller = bic;
            InitializeComponent();
        }
    }
}
