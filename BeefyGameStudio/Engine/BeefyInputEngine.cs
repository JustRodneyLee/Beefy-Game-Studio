using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeefyGameEngine
{   
    /// <summary>
    /// Keyboard Inputs are defined as BKey
    /// Mouse Button Inputs are defined as BMouseBtn
    /// </summary>

    public class BeefyInputBinding
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public IBeefyInput Input { get; set; }
        public InputCondition Condition { get; set; }
        public event EventHandler Action;
        public float HoldTime { get; set; }

        public BeefyInputBinding(Keys key, EventHandler action, InputCondition inputCondition, float holdTime = 0)
        {
            Enabled = true;
            Input = new BKey(key);
            Condition = inputCondition;
            Action = action;
            HoldTime = holdTime;
        }

        public BeefyInputBinding(MouseButton button, EventHandler action, InputCondition inputCondition, float holdTime = 0)
        {
            Enabled = true;
            Input = new BMouseBtn();
            Condition = inputCondition;
            Action = action;
            HoldTime = holdTime;
        }        

        //TODO : Mouse Move
        public BeefyInputBinding(MouseAxis mouseAxis, EventHandler action)
        {
            Enabled = true;
            Input = new BMouseMove(mouseAxis);
            Condition = InputCondition.Move;
            Action = action;
            HoldTime = 0;
        }

        public void DoAction()
        {
            Action?.Invoke(this, new EventArgs());            
        }
    }

    public enum InputCondition
    {
        Move,
        Up,
        Down,
        Hold,
        Scroll,
    }

    public enum InputDevice
    {
        Keyboard,
        Mouse,
    }

    public interface IBeefyInput
    {
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
            Bindings = new List<BeefyInputBinding>();
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

        public void AddInputBinding(Keys key, EventHandler action, InputCondition condition, float time = 0)
        {
            Bindings.Add(new BeefyInputBinding(key, action, condition, time));
        }

        public void AddInputBinding(MouseButton btn, EventHandler action, InputCondition condition, float time = 0)
        {
            Bindings.Add(new BeefyInputBinding(btn, action, condition, time));
        }

        public void AddInputBinding(MouseAxis axis, EventHandler action)
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

    public static class Input
    {
        static KeyboardState CurrentKeyboardState;
        static KeyboardState LastKeyboardState;
        static MouseState CurrentMouseState;
        static MouseState LastMouseState;
        static List<BKey> BeefyKeys;        
        static BMouseBtn LeftMouseButton;
        static BMouseBtn MiddleMouseButton;
        static BMouseBtn RightMouseButton;

        /// <summary>
        /// Movement of Mouse
        /// </summary>
        public static float DeltaX;
        public static float DeltaY;

        /// <summary>
        /// Gets or Sets the In-Game Mouse Position
        /// </summary>
        public static Point MousePosition { get { return CurrentMouseState.Position; } set { Mouse.SetPosition(value.X, value.Y); } }

        /// <summary>
        /// Get the Mouse Scroll value of the last frame
        /// </summary>
        public static int MouseScroll { get; internal set; }

        /// <summary>
        /// Checks if any Key is pressed
        /// </summary>
        public static bool IsAnyKeyDown { get; internal set; }

        public static Keys[] PressedKeys { get { return CurrentKeyboardState.GetPressedKeys(); } }

        public static char LastCharacterInput { get; internal set; }

        public static bool CapsLocked { get; }

        public static void Initialize(BeefyEngine Core)
        {
            LeftMouseButton = new BMouseBtn(MouseButton.Left);
            MiddleMouseButton = new BMouseBtn(MouseButton.Middle);
            RightMouseButton = new BMouseBtn(MouseButton.Right);
            //Initialize Key Array
            //TODO : Can be optimized to only check binded inputs
            BeefyKeys = new List<BKey>();
            foreach (Keys e in Enum.GetValues(typeof(Keys)))
            {
                if (!BeefyKeys.Exists(key => key.KeyCode == e))
                {
                    BeefyKeys.Add(UpdateKey(e, Core.GameTickTime));
                }
            }
            Core.Window.TextInput += TextInputEvent;
        }

        public static void TextInputEvent(object sender, TextInputEventArgs e)
        {
            LastCharacterInput = e.Character;
        }

        /// <summary>
        /// Checks if a key is pressed down
        /// </summary>
        /// <param name="targetKey"></param>
        /// <returns></returns>
        public static bool IsDown(Keys targetKey)
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
        public static bool IsDown(MouseButton targetBtn)
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
        public static bool IsUp(Keys targetKey)
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
        public static bool IsUp(MouseButton targetBtn)
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

        public static float GetHeldTime(Keys targetKey)
        {
            return BeefyKeys.Find(key => key.KeyCode == targetKey).KeyHeldTime;
        }

        public static float GetHeldTime(MouseButton targetButton)
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

        internal static BKey UpdateKey(Keys targetKey, float time)
        {
            BKey aKey = new BKey(targetKey);
            aKey.KeyCode = targetKey;
            if (CurrentKeyboardState.IsKeyDown(targetKey))
            {
                IsAnyKeyDown = true;
                aKey.KeyDown = true;
                aKey.KeyHeldTime += BeefyKeys.Find(key => key.KeyCode == targetKey).KeyHeldTime + time; // + (float)time.ElapsedGameTime.TotalSeconds;
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
        public static void InternalUpdate(float time) //(GameTime time)
        {
            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
            IsAnyKeyDown = false;
            //Keyboard
            foreach (Keys e in Enum.GetValues(typeof(Keys)))
            {
                if (!BeefyKeys.Exists(key => key.KeyCode == e))
                {
                    BeefyKeys.Add(UpdateKey(e, time));
                }
                else
                {
                    BeefyKeys[BeefyKeys.FindIndex(key => key.KeyCode == e)] = UpdateKey(e, time);
                }
            }
            //Mouse
            DeltaX = CurrentMouseState.X - LastMouseState.X;
            DeltaY = CurrentMouseState.Y - LastMouseState.Y;
            if (CurrentMouseState.LeftButton == ButtonState.Pressed)
                LeftMouseButton.BtnDown = true;
            if (CurrentMouseState.LeftButton == ButtonState.Released && LastMouseState.LeftButton == ButtonState.Pressed)
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

            if (CurrentMouseState.RightButton == ButtonState.Released && LastMouseState.RightButton == ButtonState.Released)
            {
                RightMouseButton.BtnUp = true;
            }
            else
            {
                RightMouseButton.BtnUp = false;
            }

            MouseScroll = CurrentMouseState.ScrollWheelValue - LastMouseState.ScrollWheelValue; //Subtracting the cumalative ScrollWheelValues yields the mouse scroll change for the last frame

            LastKeyboardState = CurrentKeyboardState;
            LastMouseState = CurrentMouseState;
        }

        public static void Dispose()
        {
            BeefyKeys.Clear();
        }
    }

    public class BeefyInputEngine : IBeefySystem
    {
        public BeefyEngine Core { get; set; }

        public BeefyInputEngine(BeefyEngine core)
        {
            Core = core;            
            Input.Initialize(Core);
        }

        public string Update(BeefyLevel Level)
        {
            foreach (BeefyObject BO in Level.InputBO)
            {
                BeefyInputController BIC = BO.GetComponent<BeefyInputController>();
                foreach (BeefyInputBinding BIB in BIC.Bindings)
                {
                    if (BIB.Enabled)
                    switch (BIB.Input.InputDevice)
                    {
                        case InputDevice.Keyboard:
                            switch (BIB.Condition)
                            {
                                case InputCondition.Down:
                                    if (Input.IsDown(((BKey)BIB.Input).KeyCode))
                                    {
                                        BIB.DoAction();
                                    }
                                    break;
                                case InputCondition.Up:
                                    if (Input.IsUp(((BKey)BIB.Input).KeyCode))
                                    {
                                        BIB.DoAction();
                                    }
                                    break;
                                case InputCondition.Hold:
                                    if (Input.GetHeldTime(((BKey)BIB.Input).KeyCode) == BIB.HoldTime)
                                    {
                                        BIB.DoAction();
                                    }
                                    break;
                            }
                            break;
                        case InputDevice.Mouse:
                            switch (BIB.Condition)
                            {
                                case InputCondition.Down:
                                    if (Input.IsDown(((BMouseBtn)BIB.Input).MouseButton))
                                    {
                                        BIB.DoAction();                                        
                                    }
                                    break;
                                case InputCondition.Up:
                                    if (Input.IsUp(((BMouseBtn)BIB.Input).MouseButton))
                                    {
                                        BIB.DoAction();
                                    }
                                    break;
                                case InputCondition.Hold:
                                    if (Input.GetHeldTime(((BMouseBtn)BIB.Input).MouseButton) == BIB.HoldTime)
                                    {
                                        BIB.DoAction();
                                    }
                                    break;
                                case InputCondition.Move:
                                    switch (((BMouseMove)BIB.Input).InputAxis)
                                    {
                                        case MouseAxis.X:
                                            if (Input.DeltaX != 0)
                                                BIB.DoAction();
                                            break;
                                        case MouseAxis.Y:
                                            if (Input.DeltaY != 0)
                                                BIB.DoAction();
                                            break;
                                    }
                                    break;
                                case InputCondition.Scroll:
                                    BIB.DoAction();
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

        }
    }
}
