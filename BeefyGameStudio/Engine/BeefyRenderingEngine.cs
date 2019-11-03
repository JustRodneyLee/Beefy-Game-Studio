﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeefyEngine
{
    /// <summary>
    /// Sprites and Textures Rendering
    /// </summary>
    /// 

    public struct BeefyAnimation
    {
        Texture2D SpriteSheet { get; set; }
        List<Rectangle> SourceFrames { get; set; }
        int FrameRate { get; set; }
        int FrameCount { get; set; }
        int Frame { get; set; }
        public BeefyAnimation(Texture2D ss, List<Rectangle> sf, int fr, int fc, int f = 0)
        {
            SpriteSheet = ss;
            SourceFrames = sf;
            FrameRate = fr;
            FrameCount = fc;
            Frame = f;
        }
    }

    public class BeefyRenderer2D : IBeefyComponent
    {
        public string ComponentID { get { return "Renderer2D"; } }
        public bool Enabled { get; private set; }        
        public Vector2 RenderCoordinates { get { return CheckCoords(); } }
        public BeefyObject Entity { get; set; }
        public Texture2D Texture { get; set; }
        public float Rotation { get { return ConvertDegToRad(Entity.GetComponent<BeefyTransform>().Rotation); } }
        public Rectangle SourceRectangle { get; set; }
        public SpriteEffects SpriteEffects { get { return CheckFX(); } }
        public Vector2 Scaling { get { return AbsScaling(); } }
        private Color intTint; //Internal tint
        public Color Tint { get { return intTint * Alpha; } set { intTint = value; } }
        public float Alpha { get; set; }
        public Vector2 Origin { get; set; }

        public BeefyRenderer2D(BeefyObject parent)
        {
            Entity = parent;
            SourceRectangle = new Rectangle();            
            Alpha = 1;
            Origin = Vector2.Zero;
            intTint = Color.White;
            Enable();
        }

        private float ConvertDegToRad(int deg)
        {
            return (float)(deg * Math.PI / 180);
        }

        private Vector2 CheckCoords()
        {   
            Vector2 ret = Entity.GetComponent<BeefyTransform>().Coordinates;
            if (Entity.GetComponent<BeefyTransform>().Scale.X < 0 && Entity.GetComponent<BeefyTransform>().Scale.Y < 0)
                return ret;
            else if (Entity.GetComponent<BeefyTransform>().Scale.X < 0)
                return new Vector2(
                    ret.X + Entity.GetComponent<BeefyTransform>().Scale.X * (Texture.Width - 2 * Origin.X) * (float)Math.Cos(Rotation),
                    ret.Y - (float)Math.Sin(Rotation) * (Texture.Width - 2 * Origin.X) * Entity.GetComponent<BeefyTransform>().Scale.X);
            else if (Entity.GetComponent<BeefyTransform>().Scale.Y < 0)
                return new Vector2(
                    ret.X - (float)Math.Sin(Rotation) * Entity.GetComponent<BeefyTransform>().Scale.Y * (Texture.Height - 2 * Origin.Y),
                    ret.Y + 2 * Origin.Y * Entity.GetComponent<BeefyTransform>().Scale.Y * (float)Math.Cos(Rotation) - Entity.GetComponent<BeefyTransform>().Scale.Y * Texture.Height * (float)Math.Cos(Rotation));
            else
                return ret;
        }

        private SpriteEffects CheckFX()
        {
            if (Entity.GetComponent<BeefyTransform>().Scale.X < 0 && Entity.GetComponent<BeefyTransform>().Scale.Y < 0)
            {
                return SpriteEffects.None;
            }
            else if (Entity.GetComponent<BeefyTransform>().Scale.X < 0)
            {
                return SpriteEffects.FlipHorizontally;
            }
            else if (Entity.GetComponent<BeefyTransform>().Scale.Y < 0)
            {
                return SpriteEffects.FlipVertically;
            }
            else
            {
                return SpriteEffects.None;
            }
        }

        private Vector2 AbsScaling()
        {
            Vector2 s = Entity.GetComponent<BeefyTransform>().Scale;
            if (s.X < 0 && s.Y < 0)
                return new Vector2(s.X, s.Y);
            else
                return new Vector2(Math.Abs(s.X), Math.Abs(s.Y));
        }

        public void Enable()
        {
            Enabled = true;
        }

        public void Disable()
        {
            Enabled = false;
        }

        public void SetTexture(Texture2D tex)
        {
            Texture = tex;
            SourceRectangle = tex.Bounds;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }

    public class BeefyLighting : IBeefyComponent
    {
        public enum LightingType
        {
            Spot,
            Directional,
            Point,
        }

        public string ComponentID { get { return "Lighting"; } }
        public bool Enabled { get; private set; }
        public BeefyObject Entity { get; set; }
        public float Intensity { get; set; } //Candela
        public LightingType Type { get; set; }
        public float Rotation { get; set; }

        public BeefyLighting(BeefyObject parent)
        {
            Entity = parent;
        }

        public void Disable()
        {
            Enabled = false;
        }

        public void Enable()
        {
            Enabled = true;
        }

        public object Clone()
        {
            BeefyLighting bl = new BeefyLighting(Entity);
            return bl;
        }
    }

    public class BeefyAnimator2D : IBeefyComponent //Works Collaboratively with BeefyRenderer2D
    {
        public string ComponentID { get { return "Animator2D"; } }
        public bool Enabled { get; private set; }        
        public List<BeefyAnimation> Animations { get; set; }
        public BeefyObject Entity { get; set; }
        public float Timer { get; internal set; }

        public BeefyAnimator2D(BeefyObject parent)
        {
            Entity = parent;
        }

        public void Disable()
        {
            Enabled = true;
        }

        public void Enable()
        {
            Enabled = false;
        }

        public void Animate(BeefyAnimation animation)
        {
            
        }

        public void AddAnimation()
        {

        }

        public object Clone()
        {
            BeefyAnimator2D ba2d = new BeefyAnimator2D(Entity);
            return ba2d;
        }
    }

    public class BeefyRenderingEngine : IBeefySystem
    {
        public BeefyEngineCore Core { get; }
        public BeefyCamera2D Camera { get; set; }
        public Texture2D LightMap { get; }
        SpriteBatch renderer;

        public BeefyRenderingEngine(BeefyEngineCore core)
        {
            Core = core;
            Camera = new BeefyCamera2D(Core.GraphicsDevice.Viewport);
            renderer = new SpriteBatch(Core.GraphicsDevice);
        }

        public string Update(BeefyLevel Level)
        {
            foreach (BeefyLayer BL in Level.Layers)
            {
                foreach (BeefyObject BO in BL.BOC)
                {
                    BeefyRenderer2D BR2D = BO.GetComponent<BeefyRenderer2D>();
                    renderer.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, Camera.Transform);
                    renderer.Draw(BR2D.Texture, BO.GetComponent<BeefyTransform>().Coordinates, BR2D.SourceRectangle, BR2D.Tint * BL.LayerAlpha, BO.GetComponent<BeefyTransform>().Rotation, BR2D.Origin, BO.GetComponent<BeefyTransform>().Scale, SpriteEffects.None, BO.GetComponent<BeefyTransform>().Depth);
                    renderer.End();
                }
            }
            return null;
        }

        public void BakeLightMap(BeefyLevel Level)
        {
            //TODO
        }
        
        public void Dispose()
        {            
            renderer.Dispose();            
        }
    }
}