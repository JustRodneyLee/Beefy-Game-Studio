using System;
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
    public class BeefyAnimation
    {
        public string AnimationName { get; set; }
        public Texture2D TextureData { get; set; }
        public List<Rectangle> SourceFrames { get; set; }
        public bool Loop { get; set; }
        public int FrameRate { get; set; }
        public int FramesToPlay { get; set; }
        public int Frame { get; set; }

        public BeefyAnimation(string name, Texture2D ss, List<Rectangle> sf, int fr, int fc, bool lp = true, int f = 0)
        {
            AnimationName = name;
            TextureData = ss;
            SourceFrames = sf;
            FrameRate = fr;
            FramesToPlay = fc;
            Frame = f;
            Loop = lp;
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
        public Vector2 Scaling { get { return AbsoluteScaling(); } }
        private Color intTint; //Internal tint
        public Color Tint { get { return intTint * Alpha; } set { intTint = value; } }
        public float Alpha { get; set; }
        public Vector2 Origin { get; set; }
        public bool Animated { get { return Entity.HasComponent<BeefyAnimator2D>(); } }

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

        private Vector2 AbsoluteScaling()
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
        public BeefyAnimation CurrentAnimation { get; set; }
        public BeefyObject Entity { get; set; }
        public BeefyRenderer2D Renderer { get { return Entity.GetComponent<BeefyRenderer2D>(); } }
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

        public void Animate(string animName, int startFrame = 0, int framesToPlay = 128)
        {
            CurrentAnimation = Animations.Find(x => x.AnimationName==animName);
            CurrentAnimation.FramesToPlay = framesToPlay;
            if (CurrentAnimation.FramesToPlay > 0)
                if (CurrentAnimation.FramesToPlay > CurrentAnimation.SourceFrames.Count)
                    CurrentAnimation.FramesToPlay = CurrentAnimation.SourceFrames.Count - startFrame + 1;
            else if (CurrentAnimation.FramesToPlay < 0)
                if (CurrentAnimation.FramesToPlay > CurrentAnimation.SourceFrames.Count)
                    CurrentAnimation.FramesToPlay = - CurrentAnimation.SourceFrames.Count - startFrame - 1;
            if (Renderer.Texture != CurrentAnimation.TextureData)
                Renderer.SetTexture(CurrentAnimation.TextureData);
            Renderer.SourceRectangle = CurrentAnimation.SourceFrames[startFrame];
        }

        public void AddAnimation(BeefyAnimation animation)
        {
            Animations.Add(animation);
        }

        public void Update(float gameTick)
        {
            if (Timer > (1 / CurrentAnimation.FrameRate))
            {
                if (CurrentAnimation.FramesToPlay > 0)
                {
                    CurrentAnimation.Frame++;
                    if (CurrentAnimation.Frame > CurrentAnimation.FramesToPlay)
                        if (CurrentAnimation.Loop)
                            CurrentAnimation.Frame = 0;
                        else
                            CurrentAnimation = null;
                }                    
                else if (CurrentAnimation.FramesToPlay < 0)
                {
                    CurrentAnimation.Frame--;
                    if (CurrentAnimation.Frame < CurrentAnimation.FramesToPlay)
                        if (CurrentAnimation.Loop)
                            CurrentAnimation.Frame = 0;
                        else
                            CurrentAnimation = null;
                }
                if (CurrentAnimation != null)
                {
                    Renderer.SourceRectangle = CurrentAnimation.SourceFrames[CurrentAnimation.Frame];
                }
            }
            Timer += gameTick;
        }

        public object Clone()
        {
            BeefyAnimator2D ba2d = new BeefyAnimator2D(Entity);            
            //TODO
            return ba2d;
        }
    }

    public class BeefyRenderingEngine : IBeefySystem
    {
        public BeefyEngine Core { get; }
        public BeefyCamera2D Camera { get; set; }
        public Texture2D LightMap { get; }
        SpriteBatch renderer;

        public BeefyRenderingEngine(BeefyEngine core)
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
                    renderer.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, null, null, null, null, Camera.Transform);
                    renderer.Draw(BR2D.Texture, BO.GetComponent<BeefyTransform>().Coordinates, BR2D.SourceRectangle, BR2D.Tint * BL.LayerAlpha, BO.GetComponent<BeefyTransform>().Rotation, BR2D.Origin, BO.GetComponent<BeefyTransform>().Scale, SpriteEffects.None, BO.GetComponent<BeefyTransform>().Depth);
                    //TODO : Lighting and Normal Maps
                    renderer.End();
                    if (BR2D.Animated)
                        BO.GetComponent<BeefyAnimator2D>().Update(Core.GameTickTime);
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
