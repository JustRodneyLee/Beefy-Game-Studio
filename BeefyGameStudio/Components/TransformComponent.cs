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
        float cX, cY, sX, sY;
        int rot;
        bool iabs;

        public TransformComponent(BeefyTransform bt)
        {            
            Name = "TransformComponent";
            transform = bt;
            cX = transform.Coordinates.X;
            cY = transform.Coordinates.Y;
            sX = transform.Scale.X;
            sY = transform.Scale.Y;
            rot = transform.Rotation;
            iabs = bt.Entity.IsAbstract;
            InitializeComponent();
        }

        public void UpdateParameters()
        {
            cX = transform.Coordinates.X;
            cY = transform.Coordinates.Y;
            sX = transform.Scale.X;
            sY = transform.Scale.Y;
            rot = transform.Rotation;
            valueBoxCoordsX.SetValue(ref cX);
            valueBoxCoordsY.SetValue(ref cY);
            valueBoxScaleX.SetValue(ref sX);
            valueBoxScaleY.SetValue(ref sY);
            valueBoxRotation.SetValue(ref rot);
            valueBoxIsAbstract.SetValue(ref iabs);
        }

        public void TransferParameters()
        {            
            if (valueBoxCoordsX.ActiveValueChange || valueBoxCoordsY.ActiveValueChange)
            {
                transform.Coordinates = new Vector2(cX, cY);
            }
            if (valueBoxScaleX.ActiveValueChange || valueBoxScaleY.ActiveValueChange)
            {
                transform.Scale = new Vector2(sX, sY);
            }
            if (valueBoxRotation.ActiveValueChange)
            {
                transform.Rotation = rot;
            }                
            transform.Entity.IsAbstract = iabs;
        }

        private void TransformComponent_Load(object sender, EventArgs e)
        {
            this.Width = Parent.Width;
            valueBoxCoordsX.SetValue(ref cX);
            valueBoxCoordsY.SetValue(ref cY);
            valueBoxScaleX.SetValue(ref sX);
            valueBoxScaleY.SetValue(ref sY);
            valueBoxRotation.SetValue(ref rot);
            valueBoxIsAbstract.SetValue(ref iabs);
            valueBoxRotation.SetSuffix("°");
        }        
    }
}
