using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeefyGameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Forms;
using MonoGame.Forms.Components;
using MonoGame.Forms.Controls;

namespace BeefyGameStudio
{
    public class AssetView : MonoGameControl
    {
        public BGS MainForm { get; set; }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //MainForm.currentProject;
        }

        protected override void Draw()
        {
            base.Draw();
        }
    }
}
