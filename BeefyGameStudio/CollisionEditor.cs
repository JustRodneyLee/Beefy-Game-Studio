using System;
using System.Windows.Forms;
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
    public class CollisionEditor : MonoGameControl
    {
        enum EditorAction
        {
            None,
            Pan,
            Move,
        }

        public BeefyShape Collider;
        List<BeefyVertex> SelectedVertices;
        EditorAction editorAction;
        public Camera2D View;
        float lX, lY; //Last Absolute Mouse Position
        Vector2 EditorMousePos; //+X +Y Position
        string MousePosStr; //Mouse Position string
        float dX; //Mouse delta X
        float dY; //Mouse delta Y
        public Vector2 MousePos { get { return new Vector2(EditorMousePos.X, EditorMousePos.Y); } }
        Rectangle CullingRect;
        //Editor Properties
        int VPWidth { get { return Editor.graphics.Viewport.Width; } }
        int VPHeight { get { return Editor.graphics.Viewport.Height; } }

        protected override void Initialize()
        {
            base.Initialize();
            Editor.BackgroundColor = new Color(50, 50, 50);
            View = new Camera2D();
            editorAction = EditorAction.None;
            SelectedVertices = new List<BeefyVertex>();
        }

        public void GetEditorMousePos(MouseEventArgs e)
        {
            if (IsMouseInsideControl)
                EditorMousePos = new Vector2((float)Math.Round((Editor.Cam.Position.X + e.X - VPWidth / 2) / View.Zoom, 2), (float)Math.Round((-Editor.Cam.Position.Y - e.Y + VPHeight / 2) / View.Zoom, 2));
        }

        public BeefyVertex GetVertex(Vector2 pos)
        {
            foreach (BeefyVertex bv in Collider.VertexSet)
            {
                if (Vector2.Distance(bv.ToVector2(), pos) < 10 / View.Zoom)
                {
                    return bv;
                }
            }
            return null;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            switch (editorAction)
            {
                case EditorAction.None:
                    if (GetVertex(EditorMousePos) != null)
                    {
                        //TODO
                    }
                    else
                        editorAction = EditorAction.Pan;
                    break;
                case EditorAction.Pan:
                    break;
                case EditorAction.Move:
                    break;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            dX = lX - e.X;
            dY = lY - e.Y;
            switch (editorAction)
            {
                case EditorAction.None:
                    break;
                case EditorAction.Pan:
                    View.Move(new Vector2(dX / View.Zoom * EditorSettings.PanSpeed, dY / View.Zoom * EditorSettings.PanSpeed));
                    break;
                case EditorAction.Move:
                    break;
            }

            lX = e.X; lY = e.Y;
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            switch (editorAction)
            {
                case EditorAction.None:
                    break;
                case EditorAction.Pan:
                    editorAction = EditorAction.None;
                    break;
                case EditorAction.Move:
                    break;
            }
            base.OnMouseUp(e);
        }

        protected override void Draw()
        {
            base.Draw();
            Editor.BeginCamera2D();
            Editor.Cam.Position = View.Position * View.Zoom;
            //Editor.spriteBatch.Draw();
            //foreach (ScriptNode sn in )
            Editor.EndCamera2D();
            //TODO
        }
    }
}
