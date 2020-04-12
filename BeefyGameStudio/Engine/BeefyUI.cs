using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeefyEngine
{
    public enum UIType
    {
        Label,
        Button,
        TextBox,
        CheckBox,
        ListBox,
    }

    public class BeefyUISystem : IBeefySystem
    {
        public BeefyEngine Core { get; }
        public BeefyObject FocusedObject { get; set; }

        public BeefyUISystem(BeefyEngine core)
        {
            Core = Core;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string Update(BeefyLevel Level)
        {
            foreach (BeefyObject bo in Level.UI)
            {
                BeefyUI ui = bo.GetComponent<BeefyUI>();
                //TODO
                if (ui.Collider.ContainsPoint(Input.MousePosition))
                {
                    if (ui.MouseInControl)
                        ui.Events.RunFunction("MouseEnter");
                    ui.MouseInControl = true;
                    if (Input.IsDown(MouseButton.Right))
                    {
                        ui.Events.RunFunction("MouseRightDown");
                    }
                    if (Input.IsDown(MouseButton.Left))
                    {
                        ui.Events.RunFunction("MouseLeftDown");
                        FocusedObject = bo;
                    }
                    if (Input.IsUp(MouseButton.Right))
                    {
                        ui.Events.RunFunction("MouseRightClicked");
                    }
                    if (Input.IsUp(MouseButton.Left))
                    {
                        ui.Events.RunFunction("MouseLeftClicked");
                    }            
                    if (Input.IsAnyKeyDown)
                    {                        
                        ui.Events.RunFunction("KeyPress", new ParameterCollection(Input.PressedKeys));
                    }
                    if (Input.MouseScroll != 0)
                    {
                        ui.Events.RunFunction("MouseScroll", new ParameterCollection(Input.MouseScroll));
                    }
                }
                else
                {
                    if (ui.MouseInControl)
                        ui.Events.RunFunction("MouseLeave");
                    ui.MouseInControl = false;
                }                
            }
            return null;
        }
    }

    public class BeefyUI : IBeefyComponent
    {
        public string ComponentID { get { return "BeefyUI"; } }
        public bool Enabled { get; internal set; }
        public BeefyObject Entity { get; set; }

        public BeefyShape Collider { get; set; }
        public UIType Type { get; set; }
        public string Text { get; set; }

        public bool MouseInControl { get; internal set; }  
        
        public BeefyScript Events { get; internal set; }

        public BeefyUI(BeefyObject parent)
        {
            Entity = parent;
        }

        public void SetCollider(BeefyShape shape)
        {
            Collider = shape;
        }

        public object Clone()
        {
            BeefyUI bui = new BeefyUI(Entity);
            return bui;
        }

        public void Disable()
        {
            Enabled = false;
        }

        public void Enable()
        {
            Enabled = true;
        }
    }

    public class UIEvent : BeefyScript
    {
        internal BeefyUI UI;

        public UIEvent(object obj) : base(obj)
        {
            UI = (BeefyUI)Parent;
        }

        public virtual void MouseEnter()
        {

        }

        public virtual void MouseLeave()
        {

        }

        public virtual void MouseRightDown()
        {

        }

        public virtual void MouseLeftDown()
        {

        }

        public virtual void MouseRightClicked()
        {

        }

        public virtual void MouseLeftClicked()
        {

        }

        public virtual void MouseScroll(int scrollValue)
        {

        }

        public virtual void KeyPress(TextInputEventArgs e)
        {

        }

        public virtual void KeyPress(BKey[] keys)
        {

        }        
    }

    public class TextBoxEvent : UIEvent
    {
        /// <summary>
        /// Position of the "I" cursor in the textbox
        /// </summary>
        public int CursorPos { get; set; }
        public bool Caps { get { if (Input.IsDown(Keys.LeftShift) || Input.IsDown(Keys.RightShift) || Input.CapsLocked) return true; else return false; } }

        public TextBoxEvent(object obj) : base(obj)
        {
            CursorPos = 0;
        }

        public override void MouseLeftDown()
        {
            //TODO : Move cursor
            base.MouseLeftDown();
        }

        public override void KeyPress(BKey[] keys)
        {            
            switch (keys[0].KeyCode)
            {
                case Keys.Back:
                    UI.Text = UI.Text.Substring(0, CursorPos - 1) + UI.Text.Substring(CursorPos, UI.Text.Length - CursorPos);
                    break;
                case Keys.Delete:
                    UI.Text = UI.Text.Substring(0, CursorPos) + UI.Text.Substring(CursorPos + 1, UI.Text.Length - CursorPos);
                    break;
                case Keys.Right:
                    CursorPos += 1;
                    break;
                case Keys.Left:
                    CursorPos -= 1;
                    break;
                default:
                    UI.Text = UI.Text.Substring(0, CursorPos) + Input.LastCharacterInput + UI.Text.Substring(CursorPos, UI.Text.Length - CursorPos);
                    break;
            }                
        }
    }

    public class BeefyUIObject : BeefyObject
    {
        public BeefyShape Control { get; set; }            

        public BeefyUIObject()
        {
            AddComponent(new BeefyUI(this));
            AddComponent(new BeefyRenderer2D(this));
            Control = GetComponent<BeefyUI>().Collider;
        }   
    }

    public class BeefyLabel : BeefyUIObject
    {
        public BeefyLabel()
        {
            GetComponent<BeefyUI>().Type = UIType.Label;
        }
    }

    public class BeefyButton : BeefyUIObject
    {
        public BeefyButton() : base()
        {
            GetComponent<BeefyUI>().Type = UIType.Button;
        }       
    }

    public class BeefyTextBox: BeefyUIObject
    {
        public BeefyTextBox() : base()
        {            
            GetComponent<BeefyUI>().Type = UIType.TextBox;
            GetComponent<BeefyUI>().Events = new TextBoxEvent(GetComponent<BeefyUI>());
        }
    }
}
