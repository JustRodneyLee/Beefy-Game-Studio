using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;

namespace BeefyGameEngine
{
    public class BeefyScriptComponent : IBeefyComponent
    {
        //TODO
        public string ComponentID { get { return "ScriptComponent"; } }

        public bool Enabled { get; internal set; }

        public BeefyObject Entity { get; set; }

        public BeefyScript Script { get; set; }

        public BeefyScriptComponent(BeefyObject parent)
        {
            Entity = parent;
            Enabled = true;
        }

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

    public class ParameterCollection
    {
        Dictionary<string, object> parameters;

        public ParameterCollection(object s)
        {
            parameters = new Dictionary<string, object>();
        }

        public object this[string index]
        {
            get { return parameters[index]; }
        }

        public void SetParameter()
        {

        }
    }

    public class BeefyScript : IEnumerable<BeefyScript.IBeefyFunction>
    {
        public string Name { get; set; }
        public object Parent { get; set; }
        public List<IBeefyFunction> Functions { get; }

        public BeefyScript(object parent)
        {
            Parent = parent;            
        }

        public void AddFunction(IBeefyFunction ibf)
        {
            Functions.Add(ibf);
        }

        public void RemoveFunction(string name)
        {
            Functions.Remove(Functions.Find(x => x.FunctionName == name));
        }

        public IEnumerator<IBeefyFunction> GetEnumerator()
        {
            return Functions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Functions.GetEnumerator();
        }

        public interface IBeefyFunction
        {
            string FunctionName { get; }
            ParameterCollection Parameters { get; }
        }
    }
}
