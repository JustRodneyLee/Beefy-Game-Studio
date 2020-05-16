using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeefyGameEngine;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using MessageBox = System.Windows.Forms.MessageBox;

namespace BeefyGameStudio.Components
{
    public partial class InputEditor : Form
    {
        BeefyInputBinding bib;
        string name;
        IBeefyInput input;
        InputCondition condition;
        List<BeefyScript> bscripts;
        BeefyScript.IBeefyFunction func;
        BeefyScript script;

        public InputEditor(BeefyInputBinding _bib, List<BeefyScript> scripts)
        {
            InitializeComponent();
            bib = _bib;
            scriptComboBox.Items.Clear();
            bscripts = scripts;
            foreach (BeefyScript bs in bscripts)
            {
                scriptComboBox.Items.Add(bs.Name);
            }
        }

        private void InputEditor_Load(object sender, EventArgs e)
        {
            InitializeInputs();
            if (bib.Input is BKey)
            {                
                inputsTreeView.SelectedNode = inputsTreeView.Nodes.Find(((BKey)bib.Input).KeyCode.ToString(), true).FirstOrDefault();
                bib.Condition.ToString();
            }
            else if (bib.Input is BMouseBtn)
            {
                inputsTreeView.SelectedNode = inputsTreeView.Nodes.Find(((BMouseBtn)bib.Input).MouseButton.ToString(), true).FirstOrDefault();
                bib.Condition.ToString();
            }
            else if (bib.Input is BMouseMove)
            {
                inputsTreeView.SelectedNode = inputsTreeView.Nodes.Find(((BMouseMove)bib.Input).InputAxis.ToString(), true).FirstOrDefault();
                bib.Condition.ToString();
            }
            else
            {
                //Do nothing
            }
            SetConditionComboBox();
            name = bib.Name;
            inputNameTextBox.Text = name;            
        }

        private void InitializeInputs()
        {
            TreeNode keyboardNode = inputsTreeView.Nodes.Find("Keyboard", false).FirstOrDefault();
            TreeNode mouseNode = inputsTreeView.Nodes.Find("Mouse", false).FirstOrDefault();
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                keyboardNode.Nodes.Add(key.ToString(), key.ToString());
            }
            keyboardNode.Expand();            
            foreach (MouseButton mBtn in Enum.GetValues(typeof(MouseButton)))
            {
                mouseNode.Nodes.Add(mBtn.ToString(), mBtn.ToString() + " Button");
            }
            mouseNode.Nodes.Add("Scroll", "Scroll");
            mouseNode.Nodes.Add("X", "X Axis");
            mouseNode.Nodes.Add("Y", "Y Axis");
            mouseNode.Expand();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            bib.Name = inputNameTextBox.Text;
            bib.Input = input;
            bib.Condition = condition;
            //bib.Action += ;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {            
            Close();
        }

        void SetConditionComboBox()
        {
            if (inputsTreeView.SelectedNode.Name == "Keyboard" || inputsTreeView.SelectedNode.Name == "Mouse")
            {
                conditionComboBox.Items.Clear();
            }
            else if (inputsTreeView.SelectedNode.Parent.Name == "Keyboard")
            {
                conditionComboBox.Items.Clear();
                conditionComboBox.Items.Add("Press");
                conditionComboBox.Items.Add("Release");
                conditionComboBox.Items.Add("Hold");
                conditionComboBox.SelectedItem = conditionComboBox.Items[0];
                input = new BKey((Keys)Enum.Parse(typeof(Keys), inputsTreeView.SelectedNode.Name));
            }
            else if (inputsTreeView.SelectedNode.Name == "Scroll")
            {
                conditionComboBox.Items.Clear();
                conditionComboBox.Items.Add("Scroll");
                conditionComboBox.SelectedItem = conditionComboBox.Items[0];
                input = new BKey((Keys)Enum.Parse(typeof(Keys), inputsTreeView.SelectedNode.Name));
            }
            else if (inputsTreeView.SelectedNode.Name=="X Axis"|| inputsTreeView.SelectedNode.Name == "Y Axis")
            {
                conditionComboBox.Items.Clear();
                conditionComboBox.Items.Add("Move");
                conditionComboBox.SelectedItem = conditionComboBox.Items[0];
                input = new BMouseMove((MouseAxis)Enum.Parse(typeof(MouseAxis), inputsTreeView.SelectedNode.Name));
            }
            else if (inputsTreeView.SelectedNode.Name.Contains("Button") && inputsTreeView.SelectedNode.Parent.Name == "Mouse")
            {
                conditionComboBox.Items.Clear();
                conditionComboBox.Items.Add("Press");
                conditionComboBox.Items.Add("Release");
                conditionComboBox.Items.Add("Hold");
                input = new BMouseBtn((MouseButton)Enum.Parse(typeof(MouseButton), inputsTreeView.SelectedNode.Name));
            }
        }

        private void conditionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (conditionComboBox.SelectedItem.ToString())
            {
                case "Hold":
                    condition = InputCondition.Hold;
                    break;
                case "Press":
                    condition = InputCondition.Down;
                    break;
                case "Release":
                    condition = InputCondition.Up;
                    break;
                case "Scroll":
                    condition = InputCondition.Scroll;
                    break;
                case "Move":
                    condition = InputCondition.Move;
                    break;
            }
        }

        private void inputsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SetConditionComboBox();
        }

        private void inputNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(inputNameTextBox.Text, "^[a-zA-Z ]"))
            {
                MessageBox.Show("Invalid character input!");
                inputNameTextBox.Text = name;
            }
            else
            {
                name = inputNameTextBox.Text;
            }
        }

        private void scriptComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            script = bscripts.Find(x => x.Name == scriptComboBox.SelectedItem.ToString());
            if (script != null)
            {
                actionsListBox.Items.Clear();
                foreach(BeefyScript.IBeefyFunction ibf in script.Functions)
                {
                    actionsListBox.Items.Add(ibf.FunctionName);
                }
            }
        }

        private void actionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            func = script.Functions.Find(x => x.FunctionName==actionsListBox.SelectedItem.ToString());
        }
    }
}
