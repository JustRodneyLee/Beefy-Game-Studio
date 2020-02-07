using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BeefyEngine
{
    public class BeefyEngine : Game
    {
        Version _Version = new Version(1,0);

        public BeefyInputEngine BInput { get; internal set; }
        public BeefyPhysicsEngine BPhysics { get; internal set; }
        public BeefyRenderingEngine BRenderer { get; internal set; }
        public BeefyAudioEngine BAudio { get; internal set; }
        public BeefyUISystem BUI { get; internal set; }
        public BeefyAssetManager BAsset { get; internal set; }

        public string GameName { get; set; }
        public int FPS { get; internal set; }
        public float GameTickTime { get; internal set; }// { return (float)TargetElapsedTime.TotalSeconds; } }
        float secondsPerFrame;
        int framesPerSecond;
        public List<BeefyLevel> Levels { get; set; } 
        public BeefyLevel CurrentLevel { get; set; }

        public BeefyEngine()
        {        
            BeefyDebugger.LogInternal("Running " + GameName + " on Beefy Engine v" + _Version.ToString());            
        }

        protected override void Initialize()
        {
            base.Initialize();
            BeefyDebugger.LogInternal("Beefy Engine Initializing...");   
            BInput = new BeefyInputEngine(this);
            BeefyDebugger.LogInternal("Beefy Input Engine Initialization Complete.");
            BPhysics = new BeefyPhysicsEngine(this);
            BeefyDebugger.LogInternal("Beefy Physics Engine Initialization Complete.");
            BRenderer = new BeefyRenderingEngine(this);
            BeefyDebugger.LogInternal("Beefy Rendering Engine Initialization Complete.");
            BAudio = new BeefyAudioEngine(this);
            BeefyDebugger.LogInternal("Beefy Audio Engine Initialization Complete.");
            BUI = new BeefyUISystem(this);
            BeefyDebugger.LogInternal("Beefy UI System Initialization Complete.");
            BAsset = new BeefyAssetManager(this);
            BeefyDebugger.LogInternal("Beefy Asset Manager Initialization Complete.");
            Levels = new List<BeefyLevel>();
            BeefyDebugger.LogInternal("Beefy Engine Start-Up Complete.");
        }

        protected override void LoadContent() //Load all or load portion of game data?
        {
            //TODO
                        
            base.LoadContent();            
        }

        protected override void Update(GameTime gameTime)
        {
            GameTickTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Input.InternalUpdate(GameTickTime); //(float)gameTime.ElapsedGameTime.TotalSeconds);
            if (Levels.Count != 0)
                BInput.Update(Levels.LastOrDefault());
                BUI.Update(Levels.LastOrDefault());
            foreach (BeefyLevel BL in Levels)
            {
                BPhysics.Update(BL);
                BAudio.Update(BL);
            }
            base.Update(gameTime);
            BeefyDebugger.Update();            
        }

        protected override void Draw(GameTime gameTime)
        {
            foreach (BeefyLevel BL in Levels)
            {
                BRenderer.Update(BL);
            }
            base.Draw(gameTime);
            //Frame Count
            framesPerSecond++;
            secondsPerFrame += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (secondsPerFrame >= 1)
            {
                FPS = framesPerSecond;
                framesPerSecond = 0;
                secondsPerFrame = 0;
            }
        }
    }
}
