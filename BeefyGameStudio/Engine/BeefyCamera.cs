using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeefyGameEngine
{
    /// <summary>
    /// 2D Camera
    /// --[Edited March 3rd.2019]
    /// </summary>
    public class BeefyCamera2D
    {
        public BeefyCamera2D(Viewport viewport)
        {
            Viewport = viewport;
            Zoom = 1f;
            Position = Vector2.Zero;   
        }

        public enum LensType
        {
            Perspective,
            Orthographic,            
        }

        Viewport Viewport;
        public LensType Lens { get; set; }
        public Matrix Transform
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) * Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) * Matrix.CreateTranslation(new Vector3(Viewport.Width * 0.5f, Viewport.Height * 0.5f, 0));
            }
        }
        public Vector2 Position { get; internal set; } 
        public float Zoom { get; internal set; }
        public float Rotation { get; internal set; }

        public void ZoomNormal()
        {
            Zoom = 1f;
        }

        public void ZoomIn(float zoom)
        {
            Zoom *= zoom;
            if (Zoom < 0.01f)
                Zoom = 0.01f;
        }

        public void Pan(Vector2 panning)
        {
            Position += panning;
        }
    }
}
