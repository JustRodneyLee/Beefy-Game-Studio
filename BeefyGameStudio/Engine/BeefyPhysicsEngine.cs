using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BeefyEngine
{

    /// <summary>
    /// Physics Component
    /// </summary>
    public class BeefyPhysics : IBeefyComponent
    {

        public enum PhysicsType
        {
            Rigidbody,
            Static,
        }

        public string ComponentID { get { return "Physics"; } }
        public bool Enabled { get; private set; }
        public BeefyObject Entity { get; set; }
        public BeefyShape Collider { get; set; }
        public PhysicsType Type { get; set; }
        public Vector2 Velocity { get; set; } //ms^-1
        public float Mass { get; set; } //kg
        public float AngularVelocity {get;set;} //Only rotates perpendicular to the 2D-plane; +ve for clockwise and -ve for counterclockwise
        public bool AffectedByGravity { get; set; }
        public float GAcceleration { get; set; }

        public void Enable()
        {
            Enabled = true;
        }

        public void Disable()
        {
            Enabled = false;
        }

        public BeefyPhysics(BeefyObject parent)
        {
            Entity = parent;
            Collider = new BeefyShape();
            Type = PhysicsType.Rigidbody;            
            Velocity = new Vector2(0, 0);
            Mass = 1f;
            AngularVelocity = 0f;
            AffectedByGravity = true;
            GAcceleration = -9.8f;
        }

        public void AddForce(Vector2 force)
        {
            Vector2 acceleration = force / Mass;
        }        

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }


    public class BeefyPhysicsEngine : IBeefySystem
    {
        public BeefyEngine Core { get; }

        public BeefyPhysicsEngine(BeefyEngine core)
        {
            Core = core;
        }

        public static bool CheckCollision(BeefyObject ba, BeefyObject bb)
        {
            if (ba.HasComponent<BeefyPhysics>() && bb.HasComponent<BeefyPhysics>())
                return BeefyShape.IsIntersecting(ba.GetComponent<BeefyPhysics>().Collider, bb.GetComponent<BeefyPhysics>().Collider);
            else
                return false;
        }

        public string Update(BeefyLevel Level)
        {
            //TODO : HUGE PHYSICS WORK
            // 
            Level.QuadTree.Clear();
            for (int i = 0; i < Level.PhysicsBO.Count; i++)
                Level.QuadTree.Insert(Level.PhysicsBO[i]);

            List<BeefyObject> returnObjects = new List<BeefyObject>();
            for (int i = 0; i < Level.PhysicsBO.Count; i++)
            {
                returnObjects.Clear();
                Level.QuadTree.Retrieve(returnObjects, Level.PhysicsBO[i].GetComponent<BeefyPhysics>().Collider.GetBoundingRectangle());

                for (int j = 0; j < returnObjects.Count; j++)
                {
                    bool colliding = BeefyShape.IsIntersecting(Level.PhysicsBO[i].GetComponent<BeefyPhysics>().Collider, returnObjects[j].GetComponent<BeefyPhysics>().Collider);
                }
            }
            /*
            foreach (BeefyObject BO in Level.PhysicsBO)
            {
                BeefyPhysics BPComponent = BO.GetComponent<BeefyPhysics>();
                //Calculate Vertical Displacement
                if (BPComponent.AffectedByGravity)
                {
                    BPComponent.Velocity = new Vector2(BPComponent.Velocity.X, BPComponent.Velocity.Y + BPComponent.GAcceleration);
                }
                //Calculate Horizontal Displacement

                //Calculate Final Displacement
                BO.GetComponent<BeefyTransform>().Coordinates += BPComponent.Velocity;
                //Calculate Rotation                
                //BO.GetComponent<BeefyTransform>().Rotation += BPComponent.AngularVelocity;
            }*/
            return null;
        }

        public void Dispose()
        {
            
        }
    }
}
