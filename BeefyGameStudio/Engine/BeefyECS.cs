﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// This is the ECS System of the BeefyEngine
/// </summary>

namespace BeefyGameEngine
{  
    /// <summary>
    /// System that reacts to Components
    /// </summary>
    public interface IBeefySystem : IDisposable
    {
        BeefyEngine Core { get; }
        string Update(BeefyLevel Level);
    }

    /// <summary>
    /// Component that attaches to a Game Object
    /// </summary>
    public interface IBeefyComponent : ICloneable
    {
        string ComponentID { get; }
        bool Enabled { get; }
        void Enable(); void Disable();
        BeefyObject Entity { get; set; }
    }

    /// <summary>
    /// Basic Component for a tangible Game Object
    /// </summary>
    public class BeefyTransform : IBeefyComponent 
    {
        public string ComponentID { get { return "Transform"; } }
        public bool Enabled { get; private set; }
        public BeefyObject Entity { get; set; }
        public float Depth { get; set; }
        public Vector2 Coordinates { get; set; }
        public Vector2 LastCoordinates { get; set; }
        public Vector2 LastScale { get; set; }
        public Vector2 Scale { get; set; }        
        public int Rotation { get; set; }
        public int LastRotation { get; set; }
        public Vector2 UpVector { get { return new Vector2(0, (float)Math.Sin(Rotation)); } }
        public Vector2 RightVector { get { return new Vector2((float)Math.Cos(Rotation), 0); } }

        private BeefyTransform()
        {
            //Parameterless constructor for XML serialization
        }

        public BeefyTransform(BeefyObject parent)
        {
            Entity = parent;
            Scale = new Vector2(1);
            LastScale = Scale;
            Depth = 0.5f;
            Enable();
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
            BeefyTransform bt = new BeefyTransform(Entity);
            bt.Depth = Depth;
            bt.Coordinates = Coordinates;
            bt.LastCoordinates = LastCoordinates;
            bt.Rotation = Rotation;
            bt.LastRotation = LastRotation;
            bt.Scale = bt.Scale;
            return bt;
        }
    }

    public class BeefyCustomProperty : IBeefyComponent
    {
        public string ComponentID => throw new NotImplementedException();
        public bool Enabled { get; private set; }
        public BeefyObject Entity { get; set; }

        private BeefyCustomProperty()
        {
            //Parameterless constructor for XML serialization
        }

        public BeefyCustomProperty(BeefyObject parent)
        {
            Entity = parent;
            Enable();
        }

        public object Clone()
        {
            BeefyCustomProperty bcp = new BeefyCustomProperty(Entity);
            return bcp;
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
}
