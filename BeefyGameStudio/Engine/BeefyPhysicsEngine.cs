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
        public Vector2 Velocity { get; set; }
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
            AngularVelocity = 0f;
            AffectedByGravity = true;
            GAcceleration = -9.8f;
        }

        public void AddForce(Vector2 force)
        {
            Velocity += force;
        }        

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }


    public class BeefyPhysicsEngine : IBeefySystem
    {
        public BeefyEngineCore Core { get; }

        public BeefyPhysicsEngine(BeefyEngineCore core)
        {
            Core = core;
        }

        public bool CheckCollision(BeefyObject ba, BeefyObject bb)
        {
            return false;
        }

        public string Update(BeefyLevel Level)
        {
            //TODO : HUGE PHYSICS WORK
            // 
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
            }
            return null;
        }

        public void Dispose()
        {
            
        }
    }
}
