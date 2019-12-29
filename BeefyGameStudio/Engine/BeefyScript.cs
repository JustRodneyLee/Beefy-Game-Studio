using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BeefyEngine
{
    public class BeefyCustomComponent : IBeefyComponent
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
        IBeefyComponent Component;

        public BeefyScript(IBeefyComponent ibc)
        {
            Component = ibc;
        }

        public void Invoke(string actionName)
        {
            Type thisType = this.GetType();
            MethodInfo info = thisType.GetMethod(actionName);
            try
            {
                info.Invoke(this, null);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
