using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BeefyEngine
{
    public class BeefyLayer : IEquatable<BeefyLayer>
    {
        public short LayerNo { get; set; }
        public string LayerID { get; internal set; }
        public float LayerAlpha { get; set; }
        public List<BeefyObject> BOC { get; set; }
        public BeefyLevel ParentLevel { get; set; }
        public QuadTree QuadTree { get; set; }

        private BeefyLayer()
        {
            //Parameterless constructor for XML serialization
        }

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

    public class BeefyLevelSettings
    {
        public float LeftBounds { get; set; }
        public float RightBounds { get; set; }
        public float TopBounds { get; set; }
        public float BottomBounds { get; set; }

        public BeefyLevelSettings()
        {
            LeftBounds = 5000;
            RightBounds = 5000;
            TopBounds = 5000;
            BottomBounds = 5000;
        }
    }

    public class QuadTree //+X -Y coordinates
    {
        private int Max_Objects = 10;
        private int Max_Depth = 5;

        private int Depth;
        private List<BeefyObject> Objects;
        private Rectangle Bounds;
        private QuadTree[] Nodes;

        public QuadTree(int depth, Rectangle bounds)
        {
            Depth = depth;
            Objects = new List<BeefyObject>();
            Bounds = bounds;
            Nodes = new QuadTree[4];
        }

        public void Clear()
        {
            Objects.Clear();

            for (int i = 0; i < Nodes.Length; i++)
            {
                if (Nodes[i] != null)
                {
                    Nodes[i].Clear();
                    Nodes[i] = null;
                }
            }
        }

        private void Split()
        {
            int subWidth = Bounds.Width / 2;
            int subHeight = Bounds.Height / 2;
            int x = Bounds.X;
            int y = Bounds.Y;

            Nodes[0] = new QuadTree(Depth + 1, new Rectangle(x + subWidth, y, subWidth, subHeight)); //First quadrant
            Nodes[1] = new QuadTree(Depth + 1, new Rectangle(x, y, subWidth, subHeight)); //Second Quadrant
            Nodes[2] = new QuadTree(Depth + 1, new Rectangle(x, y + subHeight, subWidth, subHeight)); //Third Quadrant
            Nodes[3] = new QuadTree(Depth + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight)); //Fourth Quadrant
        }

        public void Insert(BeefyObject bgo)
        {
            BeefyObject bgo_ = bgo;
            Rectangle bounds = bgo.GetComponent<BeefyPhysics>().Collider.GetBoundingRectangle();

            if (Nodes[0] != null)
            {
                List<int> indexes = GetIndexes(bounds);
                for (int i = 0; i < indexes.Count; i++)
                {
                    int index = indexes[i];
                    if (index != -1)
                    {
                        Nodes[index].Insert(bgo_);
                        return;
                    }
                }
            }

            Objects.Add(bgo_);

            if (Objects.Count > Max_Objects && Depth < Max_Depth)
            {
                if (Nodes[0] == null)
                {
                    Split();
                }

                int i = 0;
                while (i < Objects.Count)
                {
                    BeefyObject beefyObject = Objects[i];
                    Rectangle bounds_ = beefyObject.GetComponent<BeefyPhysics>().Collider.GetBoundingRectangle();
                    List<int> indexes = GetIndexes(bounds_);
                    for (int j = 0; j < indexes.Count; j++)
                    {
                        int index = indexes[j];
                        if (index != -1)
                        {
                            Nodes[index].Insert(beefyObject);
                            Objects.Remove(beefyObject);
                        }
                        else
                        {
                            i++;
                        }
                    }
                }
            }
        }

        private List<int> GetIndexes(Rectangle bounds)
        {
            List<int> indexes = new List<int>();

            float verticalMidpoint = Bounds.X + (Bounds.Width / 2);
            float horizontalMidpoint = Bounds.Y + (Bounds.Height / 2);

            bool topQuadrant = bounds.Y >= horizontalMidpoint;
            bool bottomQuadrant = (bounds.Y - bounds.Height) <= horizontalMidpoint;
            bool topAndBottomQuadrant = bounds.Y + bounds.Height + 1 >= horizontalMidpoint && bounds.Y + 1 <= horizontalMidpoint;

            if (topAndBottomQuadrant)
            {
                topQuadrant = false;
                bottomQuadrant = false;
            }

            if (bounds.X + bounds.Width + 1 >= verticalMidpoint && bounds.X - 1 <= verticalMidpoint)
            {
                if (topQuadrant)
                {
                    indexes.Add(2);
                    indexes.Add(3);
                }
                else if (bottomQuadrant)
                {
                    indexes.Add(0);
                    indexes.Add(1);
                }
                else if (topAndBottomQuadrant)
                {
                    indexes.Add(0);
                    indexes.Add(1);
                    indexes.Add(2);
                    indexes.Add(3);
                }
            }
            else if (bounds.X + 1 >= verticalMidpoint)
            {
                if (topQuadrant)
                {
                    indexes.Add(3);
                }
                else if (bottomQuadrant)
                {
                    indexes.Add(0);
                }
                else if (topAndBottomQuadrant)
                {
                    indexes.Add(3);
                    indexes.Add(0);
                }
            }
            else if (bounds.X - bounds.Width <= verticalMidpoint)
            {
                if (topQuadrant)
                {
                    indexes.Add(2);
                }
                else if (bottomQuadrant)
                {
                    indexes.Add(1);
                }
                else if (topAndBottomQuadrant)
                {
                    indexes.Add(2);
                    indexes.Add(1);
                }
            }
            else
            {
                indexes.Add(-1);
            }
            return indexes;
        }

        public List<BeefyObject> Retrieve(List<BeefyObject> objList, Rectangle bounds)
        {
            List<int> indexes = GetIndexes(bounds);
            for (int i = 0; i < indexes.Count; i++)
            {
                int index = indexes[i];
                if (index != -1 && Nodes[0] != null)
                {
                    Nodes[index].Retrieve(objList, bounds);
                }

                objList.AddRange(Objects);
            }
            return objList;
        }
    }

    public class BeefyLevel : IEquatable<BeefyLevel>, IDisposable
    {

        public string LevelID { get; internal set; }

        public BeefyLevelSettings Settings { get; set; }

        public List<BeefyLayer> Layers { get; set; }

        public List<BeefyObject> BOC { get; set; }

        public List<BeefyObject> PhysicsBO { get; }
        public QuadTree QuadTree { get; set; }
        public List<BeefyObject> AudioBO { get; }
        public List<BeefyObject> RenderBO { get; }
        public List<BeefyObject> InputBO { get; }
        public List<BeefyObject> UI { get; }

        public BeefyScript Logic { get; }

        public BeefyLevel(string lvlName)
        {
            LevelID = lvlName;
            Settings = new BeefyLevelSettings();
            Layers = new List<BeefyLayer>();
            Layers.Add(new BeefyLayer(this, "Layer 0"));
            BOC = new List<BeefyObject>();
            PhysicsBO = new List<BeefyObject>();
            QuadTree = new QuadTree(0, new Rectangle((int)Settings.LeftBounds, (int)Settings.BottomBounds, (int)(Settings.RightBounds - Settings.LeftBounds), (int)(Settings.TopBounds - Settings.BottomBounds)));
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
