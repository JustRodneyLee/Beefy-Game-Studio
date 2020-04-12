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
    /// <summary>
    /// This is not part of the engine but rather presets that will be compiled and treated like any other game objects at run-time
    /// </summary>

    public static class BeefyPresets
    {
        public static GraphicsDevice graphics;

        public static void SetGraphicsDevice(GraphicsDevice gd)
        {
            graphics = gd;
        }

        public class Box : BeefyObject
        {
            public Box(string name = "Box", Color boxColor = default, float width = 5f, float height = 5f)
            {
                ObjectID = name;
                AddComponent(new BeefyRenderer2D(this));
                Texture2D boxTex = new Texture2D(graphics, (int)width, (int)height);
                Color[] data = new Color[(int)width * (int)height];
                if (boxColor == default) boxColor = Color.White;
                for (int i = 0; i < data.Length; i++) data[i] = boxColor;
                boxTex.SetData(data);
                GetComponent<BeefyRenderer2D>().SetTexture(boxTex);
                GetComponent<BeefyRenderer2D>().Origin = new Vector2(width/2, height/2);
                AddComponent(new BeefyPhysics(this));                
            }            
        }

        public class Circle : BeefyObject
        {
            public Circle(string name = "Circle", Color circleColor = default, float radius = 10f)
            {
                int texRadius = (int)radius * 50;
                ObjectID = name;
                AddComponent(new BeefyRenderer2D(this));
                Texture2D boxTex = new Texture2D(graphics, (int)(2f*texRadius), (int)(2f*texRadius));
                Color[] data = new Color[4 * texRadius * texRadius];
                if (circleColor == default) circleColor = Color.White;
                for (int y = -texRadius; y < texRadius; y++)
                    for (int x = -texRadius; x < texRadius; x++)
                        if (x * x + y * y <= texRadius * texRadius)
                            data[x + texRadius + (y + texRadius) * 2 * texRadius] = circleColor;
                boxTex.SetData(data);
                GetComponent<BeefyRenderer2D>().SetTexture(boxTex);
                GetComponent<BeefyRenderer2D>().PixelScaling = texRadius / radius;
                //GetComponent<BeefyRenderer2D>().Origin = new Vector2(radius);
                AddComponent(new BeefyPhysics(this));
            }
        }
    }
}
