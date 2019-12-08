using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using BeefyEngine;

namespace BeefyGameStudio
{
    public static class BeefyPresets
    {
        public class Box : BeefyObject
        {
            public Box(string name = "Box")
            {
                ObjectID = name;
                AddComponent(new BeefyRenderer2D(this));
                //GetComponent<BeefyRenderer2D>().SetTexture();
                //GetComponent<BeefyRenderer2D>().Origin = new Vector2();                
                AddComponent(new BeefyPhysics(this));                
            }            
        }
    }
}
