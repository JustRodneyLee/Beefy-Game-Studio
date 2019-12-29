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
        private BeefyInputEngine Input;

        public BeefyUISystem(BeefyEngine core)
        {
            Core = Core;
            Input = Core.BInput;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string Update(BeefyLevel Level)
        {
            foreach (BeefyObject bo in Level.UI)
            {
                //TODO
                if (bo.GetComponent<BeefyUI>().Collider.ContainsPoint(Input.MousePosition))
                {
                    bo.GetComponent<BeefyUI>().MouseInControl = true;
                    if (Input.IsDown(MouseButton.Right))
                    {
                        bo.GetComponent<BeefyUI>().Events.Invoke("MouseRightDown");
                    }
                    if (Input.IsDown(MouseButton.Left))
                    {
                        bo.GetComponent<BeefyUI>().Events.Invoke("MouseLeftDown");
                    }
                    if (Input.IsUp(MouseButton.Right))
                    {
                        bo.GetComponent<BeefyUI>().Events.Invoke("MouseRightClicked");
                    }
                    if (Input.IsUp(MouseButton.Left))
                    {
                        bo.GetComponent<BeefyUI>().Events.Invoke("MouseLeftClicked");
                    }            
                    if (Input.IsAnyKeyDown)
                    {
                        bo.GetComponent<BeefyUI>().Events.Invoke("KeyboardInput");
                    }
                }
                else
                {
                    bo.GetComponent<BeefyUI>().MouseInControl = false;
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

        public class ButtonEvent : BeefyScript
        {
            public ButtonEvent(IBeefyComponent ibc) : base(ibc)
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
        }

    }

    public class BeefyTextBox: BeefyUIObject
    {
        public BeefyTextBox() : base()
        {            
            GetComponent<BeefyUI>().Type = UIType.TextBox;
        }
    }
}
