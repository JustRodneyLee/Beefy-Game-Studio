using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BeefyEngine
{
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
            info.Invoke(this, null);
        }

        public virtual void Run()
        {
         
        }
    }
}
