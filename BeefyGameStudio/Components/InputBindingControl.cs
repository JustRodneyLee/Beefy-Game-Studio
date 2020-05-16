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
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace BeefyGameStudio.Components
{
    public partial class InputBindingControl : UserControl
    {
        public BeefyInputBinding BIB { get; internal set; }
        List<BeefyScript> scripts;

        public InputBindingControl(InputComponent comp)
        {
            InitializeComponent();
            BIB = new BeefyInputBinding(Keys.A, null, InputCondition.Down);
            scripts = new List<BeefyScript>();
            foreach (IBeefyComponent ibc in comp.controller.Entity.Components)
            {
                if (ibc is BeefyScriptComponent)
                {

                }
            }
        }

        private void InputBinding_Load(object sender, EventArgs e)
        {
            triggerLabel.Text = "Untitled Action | No Input Mappings";
        }

        private void triggerLabel_Click(object sender, EventArgs e)
        {            
            InputEditor inputEditor = new InputEditor(BIB, scripts);
            inputEditor.ShowDialog();
        }

        private void enabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            BIB.Enabled = enabledCheckBox.Checked;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }
    }
}
