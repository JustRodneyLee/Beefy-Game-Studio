using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using BeefyEngine;

namespace BeefyGameStudio.Components
{
    public partial class TransformComponent : UserControl, InspectorComponent
    {
        BeefyTransform transform;
        GameViewport viewport;

        public TransformComponent(BeefyTransform bt, GameViewport vp)
        {            
            Name = "TransformComponent";
            transform = bt;
            viewport = vp;
            InitializeComponent();
        }

        public void UpdateParameters()
        {
            valueBoxCoordsX.SetValue(transform.Coordinates.X);
            valueBoxCoordsY.SetValue(transform.Coordinates.Y);
            valueBoxScaleX.SetValue(transform.Scale.X);
            valueBoxScaleY.SetValue(transform.Scale.Y);
            valueBoxRotation.SetValue(transform.Rotation);            
            valueBoxIsAbstract.SetValue(transform.Entity.IsAbstract);
        }

        public void TransferParameters()
        {
            transform.Coordinates = new Vector2(valueBoxCoordsX.Value, valueBoxCoordsY.Value);
            transform.Scale = new Vector2(valueBoxScaleX.Value, valueBoxScaleY.Value);
            transform.Rotation = valueBoxRotation.Value;
            transform.Entity.IsAbstract = valueBoxIsAbstract.Value;
        }

        private void TransformComponent_Load(object sender, EventArgs e)
        {
            Width = Parent.Width - 2 * Parent.Margin.Horizontal;
            valueBoxRotation.SetSuffix("°");
            if (transform.Entity.Components.Count == 1)
                valueBoxIsAbstract.Enabled = false;
        }

        private void valueBoxCoordsX_ValueChange(object sender, EventArgs e)
        {
            transform.Coordinates = new Vector2(valueBoxCoordsX.Value, transform.Coordinates.Y);
            transform.LastCoordinates = transform.Coordinates;
            viewport.SyncBounds(transform.Entity);
        }

        private void valueBoxCoordsY_ValueChange(object sender, EventArgs e)
        {
            transform.Coordinates = new Vector2(transform.Coordinates.X, valueBoxCoordsY.Value);
            transform.LastCoordinates = transform.Coordinates;
            viewport.SyncBounds(transform.Entity);
        }

        private void valueBoxRotation_ValueChange(object sender, EventArgs e)
        {
            transform.Rotation = (int)valueBoxRotation.Value;
            transform.LastRotation = transform.Rotation;
            viewport.SyncBounds(transform.Entity);
        }

        private void valueBoxIsAbstract_ValueChange(object sender, EventArgs e)
        {
            transform.Entity.IsAbstract = valueBoxIsAbstract.Value;
            viewport.SyncBounds(transform.Entity);
        }

        private void valueBoxScaleX_ValueChange(object sender, EventArgs e)
        {
            transform.Scale = new Vector2(valueBoxScaleX.Value, transform.Scale.Y);
            transform.LastScale = transform.Scale;
            viewport.SyncBounds(transform.Entity);
        }

        private void valueBoxScaleY_ValueChange(object sender, EventArgs e)
        {
            transform.Scale = new Vector2(transform.Scale.X, valueBoxScaleY.Value);
            transform.LastScale = transform.Scale;
            viewport.SyncBounds(transform.Entity);
        }

        private void TransformComponent_Resize(object sender, EventArgs e)
        {
            Width = Parent.Width - 2 * Parent.Margin.Horizontal;
        }
    }
}
    