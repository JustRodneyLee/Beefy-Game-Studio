using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeefyGameEngine
{
    /*public class BeefyObjectCollection : ICollection<BeefyObject>
    {
        private List<BeefyObject> _BeefyObjectCollection = new List<BeefyObject>();

        public int Count => _BeefyObjectCollection.Count;

        public bool IsReadOnly => true;

        public BeefyObjectCollection(List<BeefyObject> boc = default)
        {
            if (boc!=null)
                _BeefyObjectCollection = boc;
        }

        public void Add(BeefyObject item)
        {
            _BeefyObjectCollection.Add(item);
        }

        public void Clear()
        {
            _BeefyObjectCollection.Clear();
        }

        public bool Contains(BeefyObject item)
        {
            return _BeefyObjectCollection.Contains(item);
        }

        public void CopyTo(BeefyObject[] array, int arrayIndex)
        {
            _BeefyObjectCollection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<BeefyObject> GetEnumerator()
        {
            return _BeefyObjectCollection.GetEnumerator();
        }

        public bool Remove(BeefyObject item)
        {
            return _BeefyObjectCollection.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _BeefyObjectCollection.GetEnumerator();
        }
    }*/

    public class BeefyObject : IEquatable<BeefyObject>, ICloneable
    {
        public string ObjectID { get; internal set; }
        public bool IsAbstract { get; internal set; } //Determines if this Object is tangible
        public readonly List<IBeefyComponent> Components;
        public BeefyScript Script { get; internal set; }
        public List<BeefyObject> Children { get; set; }

        private BeefyObject()
        {
            //Parameterless constructor for XML serialization
        }

        public BeefyObject(bool isAbstract = false)
        {
            Components = new List<IBeefyComponent>();
            if (isAbstract)
            {
                IsAbstract = true;
                ObjectID = "Abstract";
            }
            Components.Add(new BeefyTransform(this));
            Children = new List<BeefyObject>();
        }

        public BeefyObject(string id)
        {
            Components = new List<IBeefyComponent>();
            IsAbstract = false;
            ObjectID = id;
            Components.Add(new BeefyTransform(this));
            Children = new List<BeefyObject>();
        }

        ~BeefyObject()
        {
            RemoveAllComponents();
            Children.Clear();
        }

        #region Entity-Component-System (ECS)

        public bool HasComponent<T>() where T : class, IBeefyComponent
        {
            foreach (IBeefyComponent IBC in Components)
            {
                if (IBC is T)
                    return true;
            }
            return false;
        }

        public T GetComponent<T>() where T : class, IBeefyComponent
        {
            foreach (IBeefyComponent cmp in Components)
            {
                if (cmp is T)
                    return (T)cmp;
            }
            return default(T);
        }

        public void AddComponent(IBeefyComponent component)
        {
            if (component == null)
            {
                BeefyDebugger.LogInternal("Cannot add Null Component!");
                return;
            }
            if (Components.Exists(cmp => cmp.ComponentID == component.ComponentID))
            {
                BeefyDebugger.LogInternal("Component " + component.ComponentID + " already exists in " + ObjectID + "!");
            }
            else
            {
                component.Entity = this;
                Components.Add(component);
                BeefyDebugger.LogInternal("Added Component " + component.ComponentID + " to " + ObjectID + ".");
            }
        }

        public void RemoveComponent<T>() where T : class, IBeefyComponent
        {
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i] is T)
                {
                    Components[i].Disable();
                    Components[i].Entity = null;
                    Components.RemoveAt(i);
                    BeefyDebugger.LogInternal("Removed Component " + typeof(T).ToString() + " from " + ObjectID + ".");
                    return;
                }
            }
            BeefyDebugger.LogInternal("Component " + typeof(T) + " doesn't exists in " + ObjectID + "!");
        }

        public void RemoveAllComponents()
        {
            for (int i = 0; i < Components.Count; i++)
            {
                Components[i].Disable();
                Components[i].Entity = null;
                Components.RemoveAt(i);
            }
            Components.Clear();
            BeefyDebugger.LogInternal("All Components cleared from " + ObjectID + ".");
        }

        #endregion

        public bool Equals(BeefyObject other)
        {
            return this.ObjectID == other.ObjectID;
        }

        public object Clone()
        {
            BeefyObject bo;
            bo = new BeefyObject(IsAbstract);
            foreach (IBeefyComponent bc in Components)
            {
                bo.AddComponent((IBeefyComponent)bc.Clone());
            }
            return bo;
        }
    }
}
