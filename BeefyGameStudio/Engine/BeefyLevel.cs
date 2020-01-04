using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BeefyEngine
{
    public class BeefyLayer : IEquatable<BeefyLayer>
    {
        public short LayerNo { get; set; }
        public string LayerID { get; internal set; }
        public float LayerAlpha { get; set; }
        public List<BeefyObject> BOC { get; set; }
        public BeefyLevel ParentLevel { get; set; }

        public BeefyLayer(BeefyLevel bl, string id)
        {
            ParentLevel = bl;
            LayerID = id;
            LayerAlpha = 1f;
            BOC = new List<BeefyObject>();
        }

        public void AddObject(BeefyObject bo)
        {
            if (!HasObject(bo))
                BOC.Add(bo);
        }

        public void RemoveObject(BeefyObject bo)
        {
            if (HasObject(bo))
                BOC.Remove(bo);
        }

        public bool HasObject(BeefyObject bo)
        {
            return BOC.Contains(bo); //???
        }

        public bool SetNumber(short n)
        {
            if (!ParentLevel.Layers.Exists(x => x.LayerNo == LayerNo))
            {
                LayerNo = n;
                return true;
            }
            else
            {
                return false;
            } 
        }

        public bool SetID(string id)
        {
            if (!ParentLevel.Layers.Exists(x => x.LayerID == LayerID))
            {
                LayerID = id;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetAlpha(float alpha)
        {
            LayerAlpha = alpha;
        }

        public bool Equals(BeefyLayer other)
        {
            if (LayerID == other.LayerID)
            {
                return true;
            }
            return false;
        }
    }

    public struct BeefyLevelSettings
    {
        //TODO
    }

    public struct QuadTree
    {
        //TODO
    }

    public class BeefyLevel : IEquatable<BeefyLevel>, IDisposable
    {

        public string LevelID { get; internal set; }

        public BeefyLevelSettings Settings { get; set; }

        public List<BeefyLayer> Layers { get; set; }

        public List<BeefyObject> BOC { get; set; }

        public List<BeefyObject> PhysicsBO { get; }
        public List<BeefyObject> AudioBO { get; }
        public List<BeefyObject> RenderBO { get; }
        public List<BeefyObject> InputBO { get; }
        public List<BeefyObject> UI { get; }

        public BeefyScript Logic { get; }

        public BeefyLevel(string lvlName)
        {
            LevelID = lvlName;
            Layers = new List<BeefyLayer>();
            Layers.Add(new BeefyLayer(this, "Layer 0"));
            BOC = new List<BeefyObject>();
            PhysicsBO = new List<BeefyObject>();
            AudioBO = new List<BeefyObject>();
            RenderBO = new List<BeefyObject>();
            InputBO = new List<BeefyObject>();
            UI = new List<BeefyObject>();
        }

        public void AddLayer(BeefyLayer layer)
        {
            Layers.Add(layer);
        }

        public void RemoveLayer(string id)
        {
            Layers.RemoveAll(x => x.LayerID == id);
        }

        public void RemoveLayer(BeefyLayer layer)
        {
            Layers.Remove(layer);
        }

        public void Clear()
        {
            Layers.Clear();
            BOC.Clear();
            PhysicsBO.Clear();
            AudioBO.Clear();
            RenderBO.Clear();
            InputBO.Clear();
            UI.Clear();
        }

        public virtual void Load()
        {
            foreach(BeefyObject BO in BOC)
            { 
                if (BO.HasComponent<BeefyInputController>())
                    InputBO.Add(BO);
                if (BO.HasComponent<BeefyPhysics>())
                    PhysicsBO.Add(BO);
                if (BO.HasComponent<BeefyAudio>())
                    AudioBO.Add(BO);                
                if (BO.HasComponent<BeefyRenderer2D>())
                    RenderBO.Add(BO);
                if (BO.HasComponent<BeefyUI>())
                    UI.Add(BO);
            }
        }

        public bool Equals(BeefyLevel other)
        {
            return other.LevelID == LevelID;
        }

        public void Dispose()
        {
            //TODO
            Clear();
            GC.Collect();
        }
    }
}
