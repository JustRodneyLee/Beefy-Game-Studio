using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BeefyEngine
{
    /// <summary>
    /// In future versions this will be an independent engine
    /// </summary>
    public class BeefyEngineCore : IDisposable
    {
        Version _Version = new Version(1,0);

        public Game TheGame { get; }
        BeefyInputEngine BIE;
        BeefyPhysicsEngine BPE;        
        BeefyRenderingEngine BRE;
        BeefyAudioEngine BAE;

        internal double Timer = 0;
        public bool Running { get; internal set; }
        public GraphicsDevice GraphicsDevice { get; internal set; }
        public ContentManager Content { get; internal set; }
        public int FramesPerSecond { get; internal set; }
        public float SecondsPerFrame { get; internal set; }
        public List<BeefyLevel> Levels { get; set; }

        public BeefyEngineCore(Game game)
        {
            TheGame = game;            
            BeefyDebugger.LogInternal("Running " + TheGame.ToString() + " on Beefy Engine v" + _Version.ToString());
        }

        public void Initialize()
        {
            BeefyDebugger.LogInternal("Beefy Engine Initializing...");
            SecondsPerFrame = (float)TheGame.TargetElapsedTime.TotalSeconds;
            FramesPerSecond = (int)(1 / SecondsPerFrame);
            GraphicsDevice = TheGame.GraphicsDevice;
            Content = TheGame.Content;
            BeefyDebugger.LogInternal("Running at " + FramesPerSecond.ToString() + " Frames per Second.");
            BIE = new BeefyInputEngine(this);
            BeefyDebugger.LogInternal("Beefy Input Engine Initialization Complete.");
            BPE = new BeefyPhysicsEngine(this);
            BeefyDebugger.LogInternal("Beefy Physics Engine Initialization Complete.");
            BRE = new BeefyRenderingEngine(this);
            BeefyDebugger.LogInternal("Beefy Rendering Engine Initialization Complete.");
            BAE = new BeefyAudioEngine(this);
            BeefyDebugger.LogInternal("Beefy Audio Engine Initializaiton Complete.");
            Levels = new List<BeefyLevel>();
            BeefyDebugger.LogInternal("Beefy Engine Start-Up Complete.");
        }

        public void Load()
        {
            //TODO
        }

        public void Update()
        {
            BIE.InternalUpdate();
            foreach (BeefyLevel BL in Levels)
            {
                BPE.Update(BL);
                BAE.Update(BL);
                
            }
            BeefyDebugger.Update();
        }

        public void Draw()
        {
            foreach (BeefyLevel BL in Levels)
            {
                BRE.Update(BL);
            }            
        }

        public void Shutdown()
        {
            BeefyDebugger.LogInternal("Stopping Beefy Engine. Shutting down" + TheGame.ToString() + "...");
            Dispose();
            TheGame.Exit();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);            
            BIE.Dispose();
            BPE.Dispose();            
            BRE.Dispose();
            GC.ReRegisterForFinalize(this);
        }
    }
}
