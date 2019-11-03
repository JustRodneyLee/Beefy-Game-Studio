using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeefyGameStudio
{
    public partial class ValueBox : UserControl
    {
        public enum ValueType
        {
            String,
            Number,
            Boolean
        }

        int decimalDigits = 2;
        float hold;
        string prefix;
        string suffix;
        bool inc;
        bool dec;

        public bool ActiveValueChange
        {
            get
            {
                switch (DataType)
                {
                    case ValueType.Boolean:
                        return BoolVal == _BoolVal;
                    case ValueType.Number:
                        return NumberVal == _NumberVal;
                    case ValueType.String:
                        return StringVal == _StringVal;
                }
                return false;
            }
        }

        string StringVal;
        string _StringVal;
        [Description("String Value Stored"), Category("Data")]
        public string StringValue { get { return StringVal; } set { StringVal = value; } }

        float NumberVal;
        float _NumberVal;
        [Description("Numeric Value Stored"), Category("Data")]
        public float NumericValue { get { return NumberVal; } set { NumberVal = value; } }

        bool BoolVal;
        bool _BoolVal;
        [Description("Boolean Value Stored"), Category("Data")]
        public bool BooleanValue { get { return BoolVal; } set { BoolVal = value; } }

        /*[Description("Value Stored"), Category("Data")]
        public object Value
        {
            get
            {
                switch (DataType)
                {
                    case ValueType.Boolean:
                        return BoolVal;
                    case ValueType.Number:
                        return NumberVal;
                    case ValueType.String:
                        return StringVal;
                }
                return null;
            }

            set
            {
                switch (DataType)
                {
                    case ValueType.String:
                        Value = value;
                        break;
                    case ValueType.Boolean:
                        if (value.ToString().ToLower() == "true" || value.ToString().ToLower() == "t" || value.ToString().ToLower() == "y" || value.ToString().ToLower() == "yes")
                        {
                            Value = true;
                        }
                        else if (value.ToString().ToLower() == "false" || value.ToString().ToLower() == "f" || value.ToString().ToLower() == "n" || value.ToString().ToLower() == "no")
                        {
                            Value = false;
                        }
                        else
                        {
                            MessageBox.Show("Invalid Input!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case ValueType.Number:
                        //Hexadecimal numbers are not accepted
                        if (!float.TryParse(value.ToString(), out _))
                        {
                            MessageBox.Show("Invalid Input!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            Value = (float)value;
                        }
                        break;
                }
            }
        }*/
        [Description("Type of Value Stored"), Category("Data")]
        public ValueType DataType { get; set; }

        public ValueBox()
        {
            InitializeComponent();
            _Refresh();
        }

        void _Refresh()
        {
            increaseBtn.Width = increaseBtn.Height;
            decreaseBtn.Width = decreaseBtn.Height;
            increaseBtn.Font = new Font("Microsoft YaHei", Height / 3.6f, FontStyle.Bold);
            decreaseBtn.Font = new Font("Microsoft YaHei", Height / 3.6f, FontStyle.Bold);
            valueTextBox.Font = new Font("Microsoft YaHei", Height / 2.22f, FontStyle.Regular);
        }

        private void ValueBox_Load(object sender, EventArgs e)
        {
            _Refresh();
            UpdateText();
        }

        /// <summary>
        /// Sets the reference object
        /// </summary>
        /// <param name="value"></param>
        ///
        public void SetValue(ref float value)
        {
            NumberVal = value;
            UpdateText();
        }

        public void SetValue(ref int value)
        {
            NumberVal = value;
            UpdateText();
        }

        public void SetValue(ref string value)
        {
            StringVal = value;
            UpdateText();
        }

        public void SetValue(ref bool value)
        {
            BoolVal = value;
            UpdateText();
        }

        private void ValueDecrease()
        {
            switch (DataType)
            {
                case ValueType.String:
                    //TODO
                    break;
                case ValueType.Number:
                    NumberVal -= 1f;
                    break;
                case ValueType.Boolean:
                    BoolVal = !BoolVal;
                    break;
            }
            UpdateText();
        }

        private void ValueIncrease()
        {
            
            switch (DataType)
            {
                case ValueType.String:
                    //TODO
                    break;
                case ValueType.Number:
                    NumberVal += 1f;
                    break;
                case ValueType.Boolean:
                    BoolVal = !BoolVal;
                    break;
            }
            UpdateText();
        }

        private void ValueTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateData();                
            }
            else if (e.KeyCode == Keys.Escape)
            {
                UpdateText();
            }
        }

        private void UpdateData()
        {
            switch (DataType)
            {
                case ValueType.String:
                    _StringVal = StringVal;
                    StringVal = valueTextBox.Text;
                    break;
                case ValueType.Boolean:
                    _BoolVal = BoolVal;
                    if (valueTextBox.Text.ToLower() == "true" || valueTextBox.Text.ToLower() == "t" || valueTextBox.Text.ToLower() == "y" || valueTextBox.Text.ToLower() == "yes")
                    {
                        BoolVal = true;
                    }
                    else if (valueTextBox.Text.ToLower() == "false" || valueTextBox.Text.ToLower() == "f" || valueTextBox.Text.ToLower() == "n" || valueTextBox.Text.ToLower() == "no")
                    {
                        BoolVal = false;
                    }
                    else
                    {
                        MessageBox.Show("Invalid Input!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateText();
                    }
                    break;
                case ValueType.Number:
                    _NumberVal = NumberVal;
                    //Hexadecimal numbers are not accepted (by now)
                    float temp;
                    if (!float.TryParse(valueTextBox.Text, out temp))
                    {
                        MessageBox.Show("Invalid Input!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateText();
                    }
                    else
                    {
                        NumberVal = temp;
                    }
                    break;
            }
        }

        private void UpdateText()
        {
            switch (DataType)
            {
                case ValueType.String:
                    valueTextBox.Text = prefix + StringVal + suffix;
                    break;
                case ValueType.Number:
                    valueTextBox.Text = prefix + Math.Round(NumberVal, decimalDigits).ToString() + suffix;
                    break;
                case ValueType.Boolean:
                    if (BoolVal)
                    {
                        valueTextBox.Text = "Yes";
                    }
                    else
                        valueTextBox.Text = "No";
                    break;
            }
        }

        public void SetDigits(int digits)
        {
            decimalDigits = digits;
        }

        public void SetPrefix(string s)
        {
            prefix = s;
        }

        public void SetSuffix(string s)
        {
            suffix = s;
        }

        private void CheckLeaveControl()
        {
            if (!ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                valueTextBox.SendToBack();
                increaseBtn.Visible = false;
                decreaseBtn.Visible = false;
            }
        }

        private void ValueBox_Resize(object sender, EventArgs e)
        {
            _Refresh();
        }
        
        private void ValueTextBox_MouseEnter(object sender, EventArgs e)
        {
            _Refresh();
            valueTextBox.BringToFront();
            increaseBtn.Visible = true;
            decreaseBtn.Visible = true;
        }

        private void ValueTextBox_MouseLeave(object sender, EventArgs e)
        {
            _Refresh();
            CheckLeaveControl();
        }

        private void DecreaseBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {                
                ValueDecrease();
                inc = false;
                dec = true;
                pressTickTimer.Start();
            }                
        }

        private void IncreaseBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ValueIncrease();
                inc = true;
                dec = false;
                pressTickTimer.Start();
            }                
        }

        private void DecreaseBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dec = false;
                hold = 0;
                pressTickTimer.Stop();
            }
        }

        private void IncreaseBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                inc = false;
                hold = 0;
                pressTickTimer.Stop();
            }
        }

        private void PressTickTimer_Tick(object sender, EventArgs e)
        {
            hold += pressTickTimer.Interval;
            if (inc && hold >= 500)
            {
                ValueIncrease();
            }
            else if (dec && hold >= 500)
            {
                ValueDecrease();
            }
        }

        private void DecreaseBtn_MouseLeave(object sender, EventArgs e)
        {
            dec = false;
            hold = 0;
            pressTickTimer.Stop();
            CheckLeaveControl();
        }

        private void IncreaseBtn_MouseLeave(object sender, EventArgs e)
        {
            inc = false;
            hold = 0;
            pressTickTimer.Stop();
            CheckLeaveControl();
        }

        private void ValueTextBox_Leave(object sender, EventArgs e)
        {
            UpdateData();
            //TODO Check change
        }
    }
}
