using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BeefyEngine
{
    public class BeefyScriptComponent : IBeefyComponent
    {
        //TODO
        public string ComponentID { get { return "CustomComponent"; } }

        public bool Enabled { get; internal set; }

        public BeefyObject Entity { get; set; }

        public object Clone()
        {
            throw new NotImplementedException();
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

    public class BeefyScript
    {
        public object Parent { get; set; }

        public BeefyScript(object parent)
        {
            Parent = parent;
        }

        public object Invoke(string actionName)
        {
            return Invoke(actionName, null);
        }

        public object Invoke(string actionName, object param1)
        {
            return Invoke(actionName, new object[1] { param1 });
        }

        public object Invoke(string actionName, object param1, object param2)
        {
            return Invoke(actionName, new object[2] { param1, param2 });
        }

        public object Invoke(string actionName, object param1, object param2, object param3)
        {
            return Invoke(actionName, new object[3] { param1, param2, param3 });
        }

        public object Invoke(string actionName, object param1, object param2, object param3, object param4)
        {
            return Invoke(actionName, new object[4] { param1, param2, param3, param4 });
        }

        public object Invoke(string actionName, object param1, object param2, object param3, object param4, object param5)
        {
            return Invoke(actionName, new object[5] { param1, param2, param3, param4, param5 });
        }

        public object Invoke(string actionName, object param1, object param2, object param3, object param4, object param5, object param6)
        {
            return Invoke(actionName, new object[6] { param1, param2, param3, param4, param5, param6 });
        }

        public object Invoke(string actionName, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
        {
            return Invoke(actionName, new object[7] { param1, param2, param3, param4, param5, param6, param7 });
        }

        public object Invoke(string actionName, object param1, object param2, object param3, object param4, object param5, object param6, object param7, object param8)
        {
            return Invoke(actionName, new object[8] { param1, param2, param3, param4, param5, param6, param7, param8 });
        }

        public object Invoke(string actionName, object[] parameters = null)
        {
            Type thisType = this.GetType();
            MethodInfo info = thisType.GetMethod(actionName);
            try
            {
                return info.Invoke(this, parameters);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }
    }
}
