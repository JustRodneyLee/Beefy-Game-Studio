using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeefyEngine
{   
    /// <summary>
    /// Keyboard Inputs are defined as BKey
    /// Mouse Button Inputs are defined as BMouseBtn
    /// </summary>

    public struct BeefyInputBinding
    {
        public IBeefyInput Input;
        public InputCondition Condition;
        public string Action;
        public float HoldTime;

        public BeefyInputBinding(Keys key, string action, InputCondition inputCondition, float holdTime = 0)
        {
            Input = new BKey(key);
            Condition = inputCondition;
            Action = action;
            HoldTime = holdTime;
        }

        public BeefyInputBinding(MouseButton button, string action, InputCondition inputCondition, float holdTime = 0)
        {
            Input = new BMouseBtn();
            Condition = inputCondition;
            Action = action;
            HoldTime = holdTime;
        }        

        //TODO : Mouse Move
        public BeefyInputBinding(MouseAxis mouseAxis, string action)
        {
            Input = new BMouseMove(mouseAxis);
            Condition = InputCondition.Move;
            Action = action;
            HoldTime = 0;
        }
        /*
        public T CodeToInput<T>(string InputCode)
        {
            string[] code = InputCode.Split('_');
            switch (code[1]) //Second Branch of Code
            {
                case "K":
                    return Enum.Parse(typeof(Keys), code[2]);
                case "M":
                    return Enum.Parse(typeof(MouseButton),code[2]);
            }
            return null;
        }
        */
    }

    public enum InputCondition
    {
        Move,
        Up,
        Down,
        Hold,
    }

    public enum InputDevice
    {
        Keyboard,
        Mouse,
    }

    public interface IBeefyInput
    {
        //string InputCode { get; }
        InputDevice InputDevice { get; }
    }

    public struct BMouseMove : IBeefyInput
    {
        public BMouseMove(MouseAxis axis)
        {
            InputDevice = InputDevice.Mouse;
            InputAxis = axis;            
            
        }

        public InputDevice InputDevice { get; }        
        public MouseAxis InputAxis { get; }
    }

    /// <summary>
    /// Beefy Keyboard Input
    /// </summary>
    public struct BKey : IBeefyInput
    {
        public BKey(Keys key)
        {
            KeyCode = key;
            KeyHeldTime = 0;
            KeyDown = false;
            KeyUp = false;
            InputDevice = InputDevice.Keyboard;
            //InputCode = "BIE_K_" + (int)key;            
        }
        public InputDevice InputDevice { get; }
        public Keys KeyCode { get; internal set; }        
        //public string InputCode { get; }
        public float KeyHeldTime { get; internal set; }
        public bool KeyDown { get; internal set; } //True when Key is pressed
        public bool KeyUp { get; internal set; } //True when Key is pressed and released    
    }

    #region Beefy Mouse

    public enum MouseButton
    {
        Left, //0
        Middle, //1
        Right, //2
    }

    public struct BMouseBtn : IBeefyInput
    {
        public BMouseBtn(MouseButton btn)
        {
            MouseButton = btn;
            BtnHoldTime = 0;
            BtnDown = false;
            BtnUp = false;
            InputDevice = InputDevice.Mouse;
            //InputCode = "BIE_M_" + (int)btn;
        }
        public InputDevice InputDevice { get; }
        public MouseButton MouseButton { get; internal set; }
        //public string InputCode { get; }
        public float BtnHoldTime { get; internal set; }
        public bool BtnDown { get; internal set; }
        public bool BtnUp { get; internal set; }        
    }

    public enum MouseAxis
    {
        X,
        Y,
    }
    #endregion

    /// <summary>
    /// Inputs and Control Bindings
    /// </summary>
    public class BeefyInputController : IBeefyComponent
    {
        public string ComponentID { get { return "InputController"; } }
        public bool Enabled { get; private set; }
        public BeefyObject Entity { get; set; }
        public List<BeefyInputBinding> Bindings;
        public BeefyScript ControllerScript { get; private set; }

        public BeefyInputController(BeefyObject parent)
        {
            Entity = parent;
        }

        /// <summary>
        /// Sets the Controller Script
        /// </summary>
        /// <param name="bs"></param>
        public void SetControllerScript(BeefyScript bs)
        {
            ControllerScript = bs;
        }

        /// <summary>
        /// Adds an Input Binding
        /// </summary>
        public void AddInputBinding(BeefyInputBinding bib)
        {
            Bindings.Add(bib);
        }

        public void AddInputBinding(Keys key, string action, InputCondition condition, float time = 0)
        {
            Bindings.Add(new BeefyInputBinding(key, action, condition, time));
        }

        public void AddInputBinding(MouseButton btn, string action, InputCondition condition, float time = 0)
        {
            Bindings.Add(new BeefyInputBinding(btn, action, condition, time));
        }

        public void AddInputBinding(MouseAxis axis, string action)
        {
            Bindings.Add(new BeefyInputBinding(axis, action));
        }
        /// <summary>
        /// Removes an Input Binding
        /// </summary>
        public void RemoveInputBinding(int inputIndex)
        {
            Bindings.RemoveAt(inputIndex);
        }

        /// <summary>
        /// Removes all Input Bindings
        /// </summary>
        public void ClearAllBindings()
        {
            Bindings.Clear();
        }

        public void Enable()
        {
            Enabled = true;
        }

        public void Disable()
        {
            Enabled = false;
        }

        public object Clone()
        {
            BeefyInputController bic = new BeefyInputController(Entity);
            foreach (BeefyInputBinding bib in Bindings)
            {
                bic.AddInputBinding(bib);
            }
            return bic;
        }
    }

    public class BeefyInputEngine : IBeefySystem
    {
        public BeefyEngineCore Core { get; }
        KeyboardState CurrentKeyboardState;
        KeyboardState LastKeyboardState;        
        MouseState CurrentMouseState;
        MouseState LastMouseState;
        List<BKey> BeefyKeys;
        float DeltaX;
        float DeltaY;
        BMouseBtn LeftMouseButton;
        BMouseBtn MiddleMouseButton;
        BMouseBtn RightMouseButton;

        public BeefyInputEngine(BeefyEngineCore core)
        {
            Core = core;
            BeefyKeys = new List<BKey>();
            LeftMouseButton = new BMouseBtn(MouseButton.Left);
            MiddleMouseButton = new BMouseBtn(MouseButton.Middle);
            RightMouseButton = new BMouseBtn(MouseButton.Right);
        }

        /// <summary>
        /// Checks if a key is pressed down
        /// </summary>
        /// <param name="targetKey"></param>
        /// <returns></returns>
        public bool IsDown(Keys targetKey)
        {
            if (BeefyKeys.Find(key => key.KeyCode == targetKey).KeyDown)
            {
                return true;
            }
            else { return false; }            
        }

        /// <summary>
        /// Checks if a Mouse Button is pressed down
        /// </summary>
        /// <param name="targetBtn"></param>
        /// <returns></returns>
        public bool IsDown(MouseButton targetBtn)
        {
            switch (targetBtn)
            {
                case MouseButton.Left:
                    return LeftMouseButton.BtnDown;
                case MouseButton.Middle:
                    return MiddleMouseButton.BtnDown;
                case MouseButton.Right:
                    return RightMouseButton.BtnDown;
            }
            return false;
        }

        /// <summary>
        /// Checks if a key is pressed and released
        /// </summary>
        /// <param name="targetKey"></param>
        /// <returns></returns>
        public bool IsUp(Keys targetKey)
        {
            if (BeefyKeys.Find(key => key.KeyCode == targetKey).KeyUp)
            {
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// Checks if a Mouse Button is pressed and released
        /// </summary>
        /// <param name="targetBtn"></param>
        /// <returns></returns>
        public bool IsUp(MouseButton targetBtn)
        {
            switch (targetBtn)
            {
                case MouseButton.Left:
                    return LeftMouseButton.BtnUp;
                case MouseButton.Middle:
                    return MiddleMouseButton.BtnUp;
                case MouseButton.Right:
                    return RightMouseButton.BtnUp;
            }
            return false;
        }

        public float GetHeldTime(Keys targetKey)
        {
            return BeefyKeys.Find(key => key.KeyCode == targetKey).KeyHeldTime;
        }

        public float GetHeldTime(MouseButton targetButton)
        {
            switch (targetButton)
            {
                case MouseButton.Left:
                    return LeftMouseButton.BtnHoldTime;
                case MouseButton.Middle:
                    return MiddleMouseButton.BtnHoldTime;
                case MouseButton.Right:
                    return RightMouseButton.BtnHoldTime;
            }
            return 0f;
        }

        protected BKey UpdateKey(Keys targetKey)
        {
            BKey aKey = new BKey(targetKey);
            aKey.KeyCode = targetKey;
            if (CurrentKeyboardState.IsKeyDown(targetKey))
            {
                aKey.KeyDown = true;
                aKey.KeyHeldTime += BeefyKeys.Find(key => key.KeyCode == targetKey).KeyHeldTime + Core.SecondsPerFrame;
            }
            else
            {
                if (LastKeyboardState.IsKeyDown(targetKey))
                {
                    aKey.KeyUp = true;
                    aKey.KeyHeldTime += BeefyKeys.Find(key => key.KeyCode == targetKey).KeyHeldTime;
                }
            }
            return aKey;
        }

        /// <summary>
        /// This method gets Keyboard and Mouse Inputs
        /// </summary>
        public void InternalUpdate()
        {
            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
            //Keyboard
            foreach (Keys e in Enum.GetValues(typeof(Keys)))
            {
                if (!BeefyKeys.Exists(key => key.KeyCode == e))
                {
                    BeefyKeys.Add(UpdateKey(e));
                }
                else
                {
                    BeefyKeys[BeefyKeys.FindIndex(key => key.KeyCode == e)] = UpdateKey(e);
                }                
            }
            //Mouse
            DeltaX = CurrentMouseState.X - LastMouseState.X;
            DeltaY = CurrentMouseState.Y - LastMouseState.Y;
            if (CurrentMouseState.LeftButton == ButtonState.Pressed)
                LeftMouseButton.BtnDown = true;
            if (CurrentMouseState.LeftButton == ButtonState.Released&&LastMouseState.LeftButton == ButtonState.Pressed)
            {
                LeftMouseButton.BtnUp = true;
            }
            else
            {
                LeftMouseButton.BtnUp = false;
            }

            if (CurrentMouseState.MiddleButton == ButtonState.Released && LastMouseState.MiddleButton == ButtonState.Pressed)
            {
                MiddleMouseButton.BtnUp = true;
            }
            else
            {
                MiddleMouseButton.BtnUp = false;
            }

            if (CurrentMouseState.RightButton == ButtonState.Released&&LastMouseState.RightButton == ButtonState.Released)
            {
                RightMouseButton.BtnUp = true;
            }
            else
            {
                RightMouseButton.BtnUp = false;
            }       
            LastKeyboardState = CurrentKeyboardState;
            LastMouseState = CurrentMouseState;            
        }

        public string Update(BeefyLevel Level)
        {            
            foreach(BeefyObject BO in Level.InputBO)
            {
                BeefyInputController BIC = BO.GetComponent<BeefyInputController>();
                foreach(BeefyInputBinding BIB in BIC.Bindings)
                {
                    switch (BIB.Input.InputDevice)
                    {
                        case InputDevice.Keyboard:
                            switch (BIB.Condition)
                            {
                                case InputCondition.Down:
                                    if (IsDown(((BKey)BIB.Input).KeyCode))
                                    {
                                        BIC.ControllerScript.Invoke(BIB.Action);
                                    }
                                    break;
                                case InputCondition.Up:
                                    if (IsUp(((BKey)BIB.Input).KeyCode))
                                    {
                                        BIC.ControllerScript.Invoke(BIB.Action);
                                    }
                                    break;
                                case InputCondition.Hold:
                                    if (GetHeldTime(((BKey)BIB.Input).KeyCode)==BIB.HoldTime)
                                    {
                                        BIC.ControllerScript.Invoke(BIB.Action);
                                    }
                                    break;
                            }
                            break;
                        case InputDevice.Mouse:
                            switch (BIB.Condition)
                            {
                                case InputCondition.Down:
                                    if (IsDown(((BMouseBtn)BIB.Input).MouseButton))
                                    {
                                        BIC.ControllerScript.Invoke(BIB.Action);
                                    }
                                    break;
                                case InputCondition.Up:
                                    if (IsUp(((BMouseBtn)BIB.Input).MouseButton))
                                    {
                                        BIC.ControllerScript.Invoke(BIB.Action);
                                    }
                                    break;
                                case InputCondition.Hold:
                                    if (GetHeldTime(((BMouseBtn)BIB.Input).MouseButton) == BIB.HoldTime)
                                    {
                                        BIC.ControllerScript.Invoke(BIB.Action);
                                    }
                                    break;
                                case InputCondition.Move:
                                    switch (((BMouseMove)BIB.Input).InputAxis)
                                    {
                                        case MouseAxis.X:
                                            if (DeltaX != 0)
                                                BIC.ControllerScript.Invoke(BIB.Action);
                                            break;
                                        case MouseAxis.Y:
                                            if (DeltaY != 0)
                                                BIC.ControllerScript.Invoke(BIB.Action);
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }                    
                }
            }
            return null;
        }

        public void Dispose()
        {
            BeefyKeys.Clear();
        }
    }
}
