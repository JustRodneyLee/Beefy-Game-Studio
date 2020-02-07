using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using BeefyEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Forms;
using MonoGame.Forms.Components;
using MonoGame.Forms.Controls;
using BeefyGameStudio.Components;

namespace BeefyGameStudio
{
    public class GameViewport : MonoGameControl
    {
        public enum EditorAction
        {
            None,
            BoxSelect,
            Move,
            Rotate,
            Scale,
            Pan,
            EditOrigin,
            AddNew,
            Draw,
        }

        public enum EditorManipulator
        {
            NoEdit,
            Translation,
            Rotation,
            Scaling,
        }

        public enum EditorView
        {
            Rendered,
            Normal,
            Lightmap,
            Wireframe,
            Commentary,
        }

        public enum EditorAxis
        {
            X, Y, XY
        }

        public Camera2D View;        
        public EditorAction editorAction;
        public EditorAction lastAction;
        public EditorManipulator editorManipulator;
        public EditorView editorView;        
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

        //Editor Graphics
        Texture2D AbstractIcon;
        GraphingTools GraphingTools;
        ///Editor Actions
        bool editConfirm;
        //Selection
        bool potentialSelect;
        bool selection_firstPointSet;
        bool selection_multiSelect;
        Point selection_firstPoint;
        Rectangle selection_Box;
        //Translation
        Vector2 translation;
        //Scaling
        float sXY;
        Vector2 scaling;
        Vector2 scalingTurnPoint; //The position of the mouse in the editor when scaling starts; When mouse moves in the circle that the scaleOrigin and scalingTurnPoint forms, scale becomes <1
        //Rotating
        float rotation;
        float startRotation;
        //Zoom
        float zoomLvl;

        Vector2 modifierOrigin; //Origin variable for all modifying

        //Snapping
        bool SnapToGrid;
        //General
        EditorAxis axis;        
        bool editing;
        string statVar; //Variable for status text

        //Commentary System
        BeefyShape tempShape;
        string tempComment;

        //Editor-Related Form Controls
        Label InspectorLabel;
        ContextMenuStrip AddContextMenu;
        ContextMenuStrip EditContextMenu;
        ContextMenuStrip LayerContextMenu;
        Panel InspectorPanel;
        ToolStripStatusLabel Status;
        TreeView HierarchyView;
        SearchBar Search;
        Button AddPropertyBtn;

        List<BeefyObject> ObjectsToAdd;
        List<BeefyObject> DrawnObjects;
        Dictionary<string, BeefyShape> SelectionBoundaries;
        List<BeefyObject> SelectedObjects;
        public BeefyObject InspectedObject { get { if (SelectedObjects.Count==1 && SelectedObjects.First() != null) { return SelectedObjects.First(); } else { return null; } } }

        BeefyLayer currentLayer;
        bool showAllLayers;
        public BeefyLevel Level;

        protected override void Initialize()
        {
            base.Initialize();
            View = new Camera2D();
            View.Position = new Vector2(0, 0);
            View.Zoom = 32f;
            zoomLvl = 2f;
            editorAction = EditorAction.None;
            lastAction = editorAction;
            editorManipulator = EditorManipulator.NoEdit;
            editorView = EditorView.Normal;
            selection_firstPoint = new Point(0, 0);
            ObjectsToAdd = new List<BeefyObject>();
            SelectedObjects = new List<BeefyObject>();
            SelectionBoundaries = new Dictionary<string, BeefyShape>();
            DrawnObjects = new List<BeefyObject>();           
            AbstractIcon = SysBmpToTex2D(Properties.Resources.AbstractIcon);            
            axis = EditorAxis.XY;
            MousePosStr = "Mouse Pos:[" + Math.Round(EditorMousePos.X, 2).ToString() + "," + Math.Round(EditorMousePos.Y, 2).ToString() + "]";
            RecalculateCulling();
        }

        private Texture2D SysBmpToTex2D(System.Drawing.Bitmap bmp)
        {
            Texture2D rtrn = new Texture2D(GraphicsDevice, bmp.Width, bmp.Height);
            Color[] cData = new Color[bmp.Width * bmp.Height];
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                    cData[i + j * bmp.Width] = new Color(bmp.GetPixel(i, j).R, bmp.GetPixel(i, j).G, bmp.GetPixel(i, j).B, bmp.GetPixel(i, j).A);
            rtrn.SetData(cData);
            return rtrn;
        }

        public void LoadLevel(BeefyLevel level)
        {
            Level = level;
            SwitchToLayer(level.Layers.First().LayerID);
            ToggleAllLayers(true);
            GraphingTools = new GraphingTools(Editor.graphics, Editor.spriteBatch, EditorSettings.SelectionBorderWidth, EditorSettings.SelectionBorderColor);
        }

        public void ClearLevel()
        {
            LoadLevel(new BeefyLevel(Level.LevelID));
        }

        public void SetControls(ContextMenuStrip addCMS, ContextMenuStrip editCMS, ContextMenuStrip layerCMS, Label il /*Inspector Label*/, Panel ip/*Inspector Panel*/, Button btn /*AddPropertyButton*/, ToolStripStatusLabel sb, TreeView hr)
        {
            AddContextMenu = addCMS;
            EditContextMenu = editCMS;
            LayerContextMenu = layerCMS;
            InspectorLabel = il;
            InspectorPanel = ip;
            AddPropertyBtn = btn;
            Status = sb;
            HierarchyView = hr;
            Search = new SearchBar(this);
        }

        public void SetControl(Control ctrl)
        {
            if (ctrl is ContextMenuStrip)
            {
                AddContextMenu = (ContextMenuStrip)ctrl;
            }
            else if (ctrl is TreeView)
            {
                HierarchyView = (TreeView)ctrl;
            }
        }

        public void Inspect(BeefyObject bo)
        {
            if (bo != null)
            {
                InspectorPanel.Controls.Clear();
                InspectorLabel.Text = "Inspector - " + bo.ObjectID;                
                AddPropertyBtn.Enabled = true;
                AddPropertyBtn.Visible = true;
                foreach (IBeefyComponent bc in bo.Components)
                {
                    switch (bc.ComponentID)
                    {
                        case "Transform":
                            InspectorPanel.Controls.Add(new TransformComponent((BeefyTransform)bc, this));
                            break;
                        case "Renderer2D":
                            InspectorPanel.Controls.Add(new Renderer2DComponent((BeefyRenderer2D)bc));
                            break;
                        case "Lighting":
                            InspectorPanel.Controls.Add(new LightingComponent((BeefyLighting)bc));
                            break;
                        case "Audio":
                            InspectorPanel.Controls.Add(new AudioComponent((BeefyAudio)bc));
                            break;
                        case "InputController":
                            InspectorPanel.Controls.Add(new InputComponent((BeefyInputController)bc));
                            break;
                        case "Physics":
                            InspectorPanel.Controls.Add(new PhysicsComponent((BeefyPhysics)bc));
                            break;
                        case "Custom":
                            //InspectorPanel.Controls.Add(new BeefyCustomProperty((BeefyPhysics)bc));
                            break;
                        default:
                            //InspectorPanel.Controls.Add();
                            break;
                    }
                }                
            }
            else
            {
                AddPropertyBtn.Enabled = false;
                AddPropertyBtn.Visible = false;                
            }
        }

        #region Keyboard Controls

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (ModifierKeys == System.Windows.Forms.Keys.Control)
                SnapToGrid = true;
            if (ModifierKeys == System.Windows.Forms.Keys.Shift)
                selection_multiSelect = true;

            if (ModifierKeys == System.Windows.Forms.Keys.Control && e.KeyData == System.Windows.Forms.Keys.C)
            {
                Clipboard.SetDataObject(SelectedObjects, false);
            }            

            switch (e.KeyData)
            {
                case System.Windows.Forms.Keys.A:
                    if (SelectedObjects.Count == 0)
                    {
                        foreach (BeefyObject bo in Level.BOC)
                        {
                            SelectObject(bo);
                        }
                    }
                    else
                        DeselectAll();
                    break;
                case System.Windows.Forms.Keys.B:
                    editing = true;
                    selection_firstPointSet = false;
                    selection_Box = new Rectangle();
                    if (editorAction == EditorAction.BoxSelect)
                        editorAction = EditorAction.None;
                    else
                        editorAction = EditorAction.BoxSelect;
                    break;
                case System.Windows.Forms.Keys.D:
                    editorAction = EditorAction.Draw;
                    break;
                case System.Windows.Forms.Keys.F1:
                    editorView = EditorView.Commentary;
                    break;
                case System.Windows.Forms.Keys.F:
                    View.Position = FindFocus();
                    break;
                case System.Windows.Forms.Keys.G:
                    if (editorAction == EditorAction.Move)
                        editorAction = EditorAction.None;
                    else
                    {
                        if (SelectedObjects.Count != 0)
                        {
                            translation = Vector2.Zero;
                            editorAction = EditorAction.Move;
                        }                            
                        else
                        {
                            editing = true;
                            editorManipulator = EditorManipulator.Translation;
                        }
                    }
                    break;
                case System.Windows.Forms.Keys.M:
                    if (SelectedObjects.Count != 0 && Level.Layers.Count>1)
                    {
                        LayerContextMenu.Show(MousePosition);
                    }                        
                    //Else feedback?
                    break;
                case System.Windows.Forms.Keys.O:
                    //TODO : Change origin bug fix
                    if (editorAction == EditorAction.EditOrigin)
                        editorAction = EditorAction.None;
                    else
                    {
                        if (SelectedObjects.Count != 0)
                        {                            
                            editorAction = EditorAction.EditOrigin;
                        }
                    }
                    break;
                case System.Windows.Forms.Keys.R:
                    if (editorAction == EditorAction.Rotate)
                        editorAction = EditorAction.None;
                    else
                    {
                        if (SelectedObjects.Count != 0)
                        {                            
                            editorAction = EditorAction.Rotate;
                            rotation = 0;
                            startRotation = (float)Math.Atan2(EditorMousePos.Y - modifierOrigin.Y, EditorMousePos.X - modifierOrigin.X);
                            GetModifierOrigin();
                        }                            
                        else
                        {
                            editing = true;
                            editorManipulator = EditorManipulator.Rotation;
                        }                            
                    }                        
                    break;
                case System.Windows.Forms.Keys.S:
                    if (editorAction == EditorAction.Scale)
                        editorAction = EditorAction.None;
                    else
                    {
                        if (SelectedObjects.Count != 0)
                        {                            
                            editorAction = EditorAction.Scale;
                            sXY = 1;
                            scaling = new Vector2();
                            scalingTurnPoint = EditorMousePos;
                            GetModifierOrigin();
                        }
                        else
                        {
                            editing = true;
                            editorManipulator = EditorManipulator.Scaling;
                        }
                    }
                    break;
                case System.Windows.Forms.Keys.Q:
                    editing = false;
                    editorManipulator = EditorManipulator.NoEdit;
                    break;
                case System.Windows.Forms.Keys.E:
                    editing = true;
                    break;
                case System.Windows.Forms.Keys.X:
                    if (editorAction == EditorAction.Move||editorAction == EditorAction.Scale)
                    {
                        CancelAction();
                        if (axis == EditorAxis.X)
                            axis = EditorAxis.XY;
                        else
                            axis = EditorAxis.X;
                    }
                    break;
                case System.Windows.Forms.Keys.Y:
                    if (editorAction==EditorAction.Move||editorAction==EditorAction.Scale)
                    {
                        CancelAction();
                        if (axis == EditorAxis.Y)
                            axis = EditorAxis.XY;
                        else
                            axis = EditorAxis.Y;
                    }
                    break;
                case System.Windows.Forms.Keys.Z:
                    //TODO
                    if (editorView == EditorView.Wireframe)
                        editorView = EditorView.Normal;
                    else
                        editorView = EditorView.Wireframe;
                    break;
                case System.Windows.Forms.Keys.Delete:
                    if (SelectedObjects.Count != 0)
                    {
                        for (int i = 0;i < SelectedObjects.Count; i++)
                        {
                            RemoveBeefyObject(SelectedObjects.ElementAt(i));
                        }                            
                    }
                    break;
                case System.Windows.Forms.Keys.Space:
                    Search.Show();
                    break;
                case System.Windows.Forms.Keys.Escape:
                    CancelAction(true);
                    editorAction = EditorAction.None;
                break;
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (ModifierKeys != System.Windows.Forms.Keys.Control)
                SnapToGrid = false;
            if (ModifierKeys != System.Windows.Forms.Keys.Shift)
                selection_multiSelect = false;
            base.OnKeyUp(e);
        }

        #endregion

        #region Mouse Controls

        public void GetEditorMousePos(MouseEventArgs e)
        {
            if (IsMouseInsideControl)
                EditorMousePos = new Vector2((float)Math.Round((Editor.Cam.Position.X + e.X - VPWidth / 2) / View.Zoom, 2), (float)Math.Round((-Editor.Cam.Position.Y - e.Y + VPHeight / 2) / View.Zoom, 2));
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {

            void CheckZoom()
            {
                if (e.Delta > 0)
                {
                    View.Zoom *= 2f;
                    if (View.Zoom >= 256f)
                        View.Zoom = 256f;
                }
                else
                {
                    View.Zoom *= 0.5f;
                    if (View.Zoom <= 0.25f)
                        View.Zoom = 0.25f;
                }
                if (View.Zoom >= 0.25f && View.Zoom <= 2f)
                {
                    zoomLvl = 2f / View.Zoom;
                }
                else if (View.Zoom >= 4f && View.Zoom <= 32f)
                {
                    zoomLvl = 32f / View.Zoom;
                }
                else if (View.Zoom >= 64f && View.Zoom <= 256f)
                {
                    zoomLvl = 256f / View.Zoom;
                }
            }

            ///Zooming
            GetEditorMousePos(e);
            switch (editorAction)
            {
                case EditorAction.None:
                    CheckZoom();
                    break;
                case EditorAction.Pan:
                    CheckZoom();
                    break;
                case EditorAction.Scale:
                    if (e.Delta > 0)
                    {
                        sXY += 0.1f;
                    }
                    else
                    {
                        sXY -= 0.1f;
                    }
                    for (int i = 0; i < SelectedObjects.Count; i++)
                    {
                        BeefyObject bo = SelectedObjects.ElementAt(i);
                        BeefyTransform bt = bo.GetComponent<BeefyTransform>();
                        if (axis == EditorAxis.XY)
                            bt.Scale = bt.LastScale * new Vector2(sXY);
                        else if (axis == EditorAxis.X)
                            bt.Scale = bt.LastScale * new Vector2(sXY, 1);
                        else
                            bt.Scale = bt.LastScale * new Vector2(1, sXY);
                        SyncBounds(bo);
                    }
                    break;
            }
            base.OnMouseWheel(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            lX = e.X;
            lY = e.Y;          
            switch (e.Button)
            {
                case MouseButtons.Middle:
                    switch (editorAction)
                    {
                        case EditorAction.None:
                            lastAction = editorAction;
                            editorAction = EditorAction.Pan;
                            break;
                    }
                    break;
                case MouseButtons.Left:
                    switch (editorAction)
                    {
                        case EditorAction.None:
                            if (editing)
                            {
                                selection_firstPointSet = true;
                                selection_firstPoint = new Point(Editor.GetRelativeMousePosition.X, Editor.GetRelativeMousePosition.Y);
                                selection_Box = new Rectangle(EditorMousePos.ToPoint(), new Point(0, 0));
                                potentialSelect = true;
                            }
                            else
                            {
                                lastAction = editorAction;
                                editorAction = EditorAction.Pan;                                
                            }
                            break;
                        case EditorAction.BoxSelect:
                            selection_firstPointSet = true;
                            selection_firstPoint = new Point(Editor.GetRelativeMousePosition.X, Editor.GetRelativeMousePosition.Y);
                            selection_Box = new Rectangle(EditorMousePos.ToPoint(), new Point(0, 0));
                            break;
                        case EditorAction.Move:                            
                            foreach (BeefyObject bo in SelectedObjects)
                            {
                                bo.GetComponent<BeefyTransform>().LastCoordinates = bo.GetComponent<BeefyTransform>().Coordinates;                                
                            }
                            lastAction = editorAction;
                            editorAction = EditorAction.None;
                            axis = EditorAxis.XY;
                            editConfirm = true;
                            break;
                        case EditorAction.Rotate:
                            foreach (BeefyObject bo in SelectedObjects)
                            {
                                bo.GetComponent<BeefyTransform>().LastRotation = bo.GetComponent<BeefyTransform>().Rotation;
                            }
                            lastAction = editorAction;
                            editorAction = EditorAction.None;
                            axis = EditorAxis.XY;
                            editConfirm = true;
                            break;
                        case EditorAction.Scale:
                            foreach (BeefyObject bo in SelectedObjects)
                            {
                                bo.GetComponent<BeefyTransform>().LastScale = bo.GetComponent<BeefyTransform>().Scale;
                            }
                            lastAction = editorAction;
                            editorAction = EditorAction.None;
                            axis = EditorAxis.XY;
                            editConfirm = true;
                            break;
                        case EditorAction.EditOrigin:
                            foreach (BeefyObject bo in SelectedObjects)
                            {
                                if (!bo.IsAbstract)
                                {
                                    if (bo.HasComponent<BeefyRenderer2D>())
                                    {
                                        //Need fix
                                        if (bo.GetComponent<BeefyTransform>().Rotation==0)
                                            bo.GetComponent<BeefyRenderer2D>().Origin -= new Vector2(bo.GetComponent<BeefyTransform>().LastCoordinates.X - SelectionBoundaries[bo.ObjectID].Origin.X, SelectionBoundaries[bo.ObjectID].Origin.Y - bo.GetComponent<BeefyTransform>().LastCoordinates.Y);
                                    }
                                    bo.GetComponent<BeefyTransform>().Coordinates = SelectionBoundaries[bo.ObjectID].Origin;
                                    bo.GetComponent<BeefyTransform>().LastCoordinates = bo.GetComponent<BeefyTransform>().Coordinates;
                                }
                            }                            
                            break;
                        case EditorAction.AddNew:
                            if (ObjectsToAdd.Count != 0)
                            {
                                DeselectAll();
                                AddBeefyObjects(ObjectsToAdd);
                                foreach (BeefyObject bo in ObjectsToAdd)
                                {
                                    SelectObject(bo);
                                }
                            }                                
                            break;
                    }
                    break;
                case MouseButtons.Right:
                    if (!(editorAction == EditorAction.None || editorAction == EditorAction.Pan))
                        CancelAction(true);
                    break;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    switch (editorAction)
                    {
                        case EditorAction.None:
                            if (lastAction == EditorAction.None)
                            {
                                BeefyObject so = GetBeefyObject(EditorMousePos.X, EditorMousePos.Y);
                                if (so != null)
                                {
                                    if (!selection_multiSelect)
                                        DeselectAll();
                                    if (SelectedObjects.Exists(x => x.ObjectID == so.ObjectID))
                                    {
                                        DeselectObject(so);
                                    }
                                    else
                                    {
                                        SelectObject(so);
                                    }
                                }
                                else
                                {
                                    if (!editConfirm)
                                        DeselectAll();
                                    else
                                        editConfirm = false;
                                }
                                potentialSelect = false;
                            }                           
                            break;
                        case EditorAction.BoxSelect:
                            editorAction = EditorAction.None;
                            foreach (BeefyObject bo in DrawnObjects)
                            {
                                if (BeefyShape.IsIntersecting(new BeefyShape(selection_Box), SelectionBoundaries[bo.ObjectID]))
                                {
                                    SelectObject(bo);
                                }
                            }
                            selection_firstPointSet = false;
                            selection_Box = new Rectangle();
                            break;
                        case EditorAction.Move:                            
                            editorAction = EditorAction.None;
                            break;
                        case EditorAction.Rotate:
                            editorAction = EditorAction.None;
                            break;
                        case EditorAction.Scale:
                            editorAction = EditorAction.None;
                            break;
                        case EditorAction.AddNew:
                            editorAction = EditorAction.None;
                            break;
                    }
                    break;
                case MouseButtons.Middle:
                    if (editorAction == EditorAction.Move)
                        editorAction = EditorAction.None;
                    break;
                case MouseButtons.Right:
                    if (editorAction == EditorAction.None)
                    {
                        if (GetBeefyObject(EditorMousePos)==null)
                            AddContextMenu.Show(MousePosition);
                        else
                            EditContextMenu.Show(MousePosition);
                    }                        
                    break;        
            }
            editorAction = EditorAction.None;
            lastAction = editorAction;
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            dX = lX - e.X;
            dY = lY - e.Y;
            GetEditorMousePos(e);
            switch (editorAction)
            {
                case EditorAction.None:
                    if (e.Button == MouseButtons.Right)
                    {
                        editorAction = EditorAction.Pan;
                    }                    
                    if (SnapToGrid)
                    {
                        //TODOs
                    }
                    if (potentialSelect&&!selection_multiSelect)
                    {                        
                        if (dX!=0||dY!=0)
                        {
                            potentialSelect = false;
                            editorAction = EditorAction.BoxSelect;
                        }                        
                    }                        
                    break;
                case EditorAction.Pan:
                    View.Move(new Vector2(dX / View.Zoom * EditorSettings.PanSpeed, dY / View.Zoom * EditorSettings.PanSpeed));
                    RecalculateCulling();
                    break;
                case EditorAction.BoxSelect:
                    if (selection_firstPointSet)
                    {
                        selection_Box.Width = (int)(EditorMousePos.X - selection_Box.X);
                        selection_Box.Height = -(int)(EditorMousePos.Y - selection_Box.Y); //This box is in +X +Y space
                    }
                    break;
                case EditorAction.Move:
                    switch (axis)
                    {
                        case EditorAxis.XY:
                            translation += (new Vector2(-dX, dY)) / View.Zoom;
                            break;
                        case EditorAxis.X:
                            translation += (new Vector2(-dX, 0)) / View.Zoom;
                            break;
                        case EditorAxis.Y:
                            translation += (new Vector2(0, dY)) / View.Zoom;
                            break;
                    }
                    translation = new Vector2((float)Math.Round(translation.X, 2), (float)Math.Round(translation.Y, 2));                    
                    foreach (BeefyObject bo in SelectedObjects)
                    {
                        bo.GetComponent<BeefyTransform>().Coordinates = bo.GetComponent<BeefyTransform>().LastCoordinates + translation;
                        SyncBounds(bo);
                    }
                    break;
                case EditorAction.Rotate:
                    rotation = (float)Math.Atan2(EditorMousePos.Y - modifierOrigin.Y, EditorMousePos.X - modifierOrigin.X);
                    for (int i = 0; i < SelectedObjects.Count; i++)
                    {   
                        BeefyObject bo = SelectedObjects.ElementAt(i);
                        BeefyTransform bt = bo.GetComponent<BeefyTransform>();
                        bt.Rotation = bt.LastRotation - (ToDegrees(rotation - startRotation));
                        SyncBounds(bo);
                    }
                    break;
                case EditorAction.Scale:
                    float dist, stdDist;
                    switch (axis)
                    {
                        case EditorAxis.X:
                            dist = EditorMousePos.X - modifierOrigin.X;
                            stdDist = scalingTurnPoint.X - modifierOrigin.X;
                            break;
                        case EditorAxis.Y:
                            dist = EditorMousePos.Y - modifierOrigin.Y;
                            stdDist = scalingTurnPoint.Y - modifierOrigin.Y;
                            break;
                        case EditorAxis.XY:
                            dist = Vector2.Distance(modifierOrigin, EditorMousePos);
                            stdDist = Vector2.Distance(scalingTurnPoint, modifierOrigin);
                            break;
                        default:
                            dist = 1;
                            stdDist = 1;
                            break;
                    }                    
                    sXY = (float)Math.Round(dist / stdDist, 2);                    
                    for (int i = 0; i < SelectedObjects.Count; i++)
                    {
                        BeefyObject bo = SelectedObjects.ElementAt(i);
                        BeefyTransform bt = bo.GetComponent<BeefyTransform>();
                        if (axis == EditorAxis.XY)
                            scaling = new Vector2(sXY);
                        else if (axis == EditorAxis.X)
                            scaling = new Vector2(sXY, 1);
                        else
                            scaling = new Vector2(1, sXY);
                        bt.Scale = bt.LastScale * scaling;
                        SyncBounds(bo);
                    }
                    break;
                case EditorAction.EditOrigin:
                    foreach (BeefyObject bo in SelectedObjects)
                    {
                        if (!bo.IsAbstract)
                        {
                            if (SnapToGrid)
                            {
                                if (bo.HasComponent<BeefyRenderer2D>())
                                {
                                    SelectionBoundaries[bo.ObjectID].FindOrigin();                                    
                                }                                   
                            }
                            else
                            {
                                SelectionBoundaries[bo.ObjectID].SetOrigin(EditorMousePos);
                            }                            
                        }
                    }
                    break;
                case EditorAction.AddNew:
                    foreach (BeefyObject bo in ObjectsToAdd)
                    {
                        bo.GetComponent<BeefyTransform>().Coordinates = EditorMousePos;                        
                    }
                    break;
            }
            lX = e.X; lY = e.Y;
            base.OnMouseMove(e);            
        }

        #endregion

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            //TODO
            base.OnDragEnter(drgevent);
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            //TODO
            base.OnDragDrop(drgevent);
        }

        #region Game Editing
        public BeefyObject ConvertAssetToObject(IBeefyAsset iba)
        {
            BeefyObject bo = new BeefyObject(iba.AssetName);
            switch (iba.AssetType)
            {
                case BeefyAssetType.Visual:
                    bo.AddComponent(new BeefyRenderer2D(bo));                    
                    bo.GetComponent<BeefyRenderer2D>().SetTexture(((BeefySprite)iba).SpriteData);
                    bo.GetComponent<BeefyRenderer2D>().SetScaling(((BeefySprite)iba).ImportScale);
                    break;
                case BeefyAssetType.Auditory:
                    bo.AddComponent(new BeefyAudio(bo));
                    bo.GetComponent<BeefyAudio>().AddSoundEffect();//TODO
                    break;
                case BeefyAssetType.Object:
                    //TODO
                    break;
            }
            return bo;
        }

        /// <summary>
        /// Adds a Game Object to the current Level
        /// </summary>
        /// <param name="tag">Object ID</param>
        /// <param name="coords">Coordinates</param>
        /// <param name="isAbstract">If this is an abstract object</param>
        public void AddBeefyObject(string tag, Vector2 coords)
        {
            BeefyObject nbo = new BeefyObject(tag);
            nbo.GetComponent<BeefyTransform>().Coordinates = coords;
            nbo.GetComponent<BeefyTransform>().LastCoordinates = coords;
            AddBeefyObject(nbo);
        }

        public void AddBeefyObject(IBeefyAsset iba, Vector2 coords)
        {
            BeefyObject bo = ConvertAssetToObject(iba);
            bo.GetComponent<BeefyTransform>().Coordinates = coords;
            bo.GetComponent<BeefyTransform>().LastCoordinates = coords;
            AddBeefyObject(bo);
        }

        /// <summary>
        /// This is the elementary function for adding objects to the scene
        /// </summary>
        /// <param name="bo"></param>
        public void AddBeefyObject(BeefyObject bo)
        {
            bo.ObjectID = CheckObjectID(bo);
            SelectionBoundaries.Add(bo.ObjectID, GetBoundary(bo));
            SyncBounds(bo);
            Level.BOC.Add(bo);
            currentLayer.AddObject(bo);
            HierarchyView.Nodes.Add(bo.ObjectID, bo.ObjectID);
            DeselectAll();
            SelectObject(bo);
            //Modifications.Add(new Mo)
        }

        public void AddBeefyObjects(List<BeefyObject> bos)
        {
            foreach (BeefyObject bo in bos)
            {
                if (bo.IsAbstract)
                    AddAbstractObject(bo.GetComponent<BeefyTransform>().Coordinates);
                else
                    AddBeefyObject(bo);
            }            
        }        

        public void AddBeefyObjects(List<IBeefyAsset> ibas, Vector2 coords)
        {
            foreach (IBeefyAsset iba in ibas)
            {
                AddBeefyObject(iba, coords);
            }
        }

        /// <summary>
        /// Adds an Abstract object to the scene.
        /// </summary>
        /// <param name="coords"></param>
        public void AddAbstractObject(Vector2 coords)
        {
            BeefyObject nbo = new BeefyObject(true);
            nbo.GetComponent<BeefyTransform>().Coordinates = coords;
            nbo.GetComponent<BeefyTransform>().LastCoordinates = coords;
            AddBeefyObject(nbo);
        }

        /// <summary>
        /// Adds a new object which position in scene is determined by a user click.
        /// </summary>
        /// <param name="bo"></param>
        public void AddNewObject(BeefyObject bo)
        {
            ObjectsToAdd = new List<BeefyObject>();
            ObjectsToAdd.Add(bo);
            editorAction = EditorAction.AddNew;
            if (ObjectsToAdd.First().IsAbstract)
                statVar = "Abstract";
            else if (ObjectsToAdd.First().ObjectID == "Box")
                statVar = "Box";
            else if (ObjectsToAdd.First().ObjectID == "Circle")
                statVar = "Circle";
            else if (ObjectsToAdd.First().ObjectID == "Character")
                statVar = "Character";
            else
                statVar = "Object";
        }

        public void AddNewObjects(List<IBeefyAsset> ibaList)
        {
            List<BeefyObject> boList = new List<BeefyObject>();
            foreach (IBeefyAsset iba in ibaList)
            {
                boList.Add(ConvertAssetToObject(iba));
            }
            AddNewObjects(boList);
        }

        public void AddNewObjects(List<BeefyObject> boList)
        {
            ObjectsToAdd = boList;
            editorAction = EditorAction.AddNew;
            if (ObjectsToAdd.Count == 1)
            {
                if (ObjectsToAdd.First().IsAbstract)
                    statVar = "Abstract";
                else if (ObjectsToAdd.First().ObjectID == "Box")
                    statVar = "Box";
                else if (ObjectsToAdd.First().ObjectID == "Circle")
                    statVar = "Circle";
                else if (ObjectsToAdd.First().ObjectID == "Character")
                    statVar = "Character";
                else
                    statVar = "Object";
            }
            else
            {
                statVar = "Objects";
            }
        }

        public void RemoveBeefyObject(BeefyObject bo)
        {
            if (Level.BOC.Exists(x => x.ObjectID == bo.ObjectID))
            {
                Level.BOC.Remove(bo);                
                SelectionBoundaries.Remove(bo.ObjectID);
                HierarchyView.Nodes.RemoveByKey(bo.ObjectID);
            }                
        }

        public void DeselectAll()
        {
            SelectedObjects = new List<BeefyObject>();
        }

        public void SelectObject(BeefyObject bo)
        {
            SelectedObjects.Add(bo);
            if (SelectedObjects.Count == 1)
            {
                Inspect(SelectedObjects.First());
            }
            else
            {
                Inspect(null);
            }
        }

        public void SelectObject(string id)
        {
            SelectObject(GetBeefyObjectByID(id));
        }

        public void DeselectObject(BeefyObject bo)
        {
            SelectedObjects.Remove(bo);
        }

        public void DeselectObject(string id)
        {
            DeselectObject(GetBeefyObjectByID(id));
        }

        public List<BeefyObject> ReturnSelected()
        {
            return SelectedObjects;
        }

        public BeefyObject GetBeefyObjectByID(string id)
        {
            return Level.BOC.Find(x => x.ObjectID == id);
        }

        public BeefyObject GetBeefyObject(Vector2 vec)
        {
            return GetBeefyObject(vec.X, vec.Y);
        }

        public BeefyObject GetBeefyObject(float X, float Y)
        {
            List<BeefyObject> objects = new List<BeefyObject>();
            BeefyObject ret;
            if (CullingRect.X<=X&&CullingRect.Y>=Y&&(CullingRect.Y-CullingRect.Height<=Y)&& (CullingRect.X + CullingRect.Width >= X))
            {
                foreach (BeefyObject bo in DrawnObjects)
                {
                    if (SelectionBoundaries[bo.ObjectID].ContainsPoint(new Vector2(X, Y)))
                    {
                        objects.Add(bo);
                    }
                }
            }
            else
            {
                foreach (BeefyObject bo in Level.BOC)
                {
                    if (SelectionBoundaries[bo.ObjectID].ContainsPoint(new Vector2(X, Y)))
                    {
                        objects.Add(bo);
                    }
                }
            }
            if (objects.Count != 0)
            {
                ret = objects.First();
                foreach (BeefyObject bo in objects)
                {
                    if (bo.GetComponent<BeefyTransform>().Depth > ret.GetComponent<BeefyTransform>().Depth)
                    {
                        ret = bo;
                    }
                }
                return ret;
            }
            else
            {
                return null;
            }
        }

        public void MoveObjectsToLayer(List<BeefyObject> objects, BeefyLayer targetLayer)
        {
            foreach (BeefyObject bo in objects)
            {
                targetLayer.AddObject(bo);
                foreach (BeefyLayer bl in Level.Layers)
                {
                    if (bl!=targetLayer) //????
                        bl.RemoveObject(bo);
                }
            }            
        }

        public void SwitchToLayer(string id)
        {
            if (Level.Layers.Exists(x => x.LayerID == id))
            {
                currentLayer = Level.Layers.Find(x => x.LayerID == id);
                showAllLayers = false;
            }
        }

        public void ToggleAllLayers(bool toggle)
        {
            showAllLayers = toggle;
        }

        public int GetLargestLayerCount()
        {
            int ret = 0;
            foreach (BeefyLayer l in Level.Layers)
            {
                string id = l.LayerID;
                if (id.Substring(0, 6) == "Layer ")
                {
                    int n;
                    if (int.TryParse(id.Substring(6), out n))
                    {
                        if (n > ret) ret = n;
                    }   
                }
            }
            return ret;
        }

        public bool AddLayer(string id)
        {
            if (Level.Layers.Exists(x => x.LayerID == id))
            {
                //ID duplicated
                return false;
            }
            else
            {
                BeefyLayer bl = new BeefyLayer(Level, id);
                Level.AddLayer(bl);
                SwitchToLayer(bl.LayerID);
                return true;
            }
        }

        public bool RemoveLayer(string id)
        {
            if (Level.Layers.Exists(x => x.LayerID == id))
            {
                SwitchToLayer(Level.Layers.First().LayerID);
                //Bug fix : Remove objects in layer
                Level.RemoveLayer(id);                
                return true;
            }
            else
            {
                //Layer does not exist
                return false;
            }
        }

        public bool LoadLevel(string path)
        {
            BeefyLevel lvl;
            using (StreamReader file = new StreamReader(path))
            {
                lvl = new BeefyLevel(file.ReadLine());
            }
            if (lvl != null)
            {
                LoadLevel(lvl);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaveLevel(string path)
        {
            string lvlPath = path.Split('.').First();
            Directory.CreateDirectory(lvlPath);
            LevelData data = new LevelData(Level);
            //Write level settings using XML serialiation
            XmlSerializer serializer;
            using (StreamWriter file = new StreamWriter(lvlPath + "\\" + Level.LevelID + ".bgl"))
            {
                serializer = new XmlSerializer(typeof(LevelData));
                serializer.Serialize(file, data);
            }                        
            foreach (BeefyLayer bl in Level.Layers)
            {
                string layerPath = lvlPath + "\\" + bl.LayerID;
                foreach (BeefyObject bo in bl.BOC)
                {
                    using (StreamWriter file = new StreamWriter(layerPath + "\\" + bo.ObjectID + ".bgo"))
                    {
                        foreach (IBeefyComponent ibc in bo.Components)
                        {                            
                            //TODO
                        }                        
                    }
                }                
            }            
            return true;
        }
        #endregion

        public void RefreshHierarchy()
        {
            HierarchyView.Nodes.Clear();
            foreach (BeefyObject bo in Level.BOC)
            {
                HierarchyView.Nodes.Add(bo.ObjectID);
            }
        }

        public void Undo()
        {
            //TODO
        }

        public void Redo()
        {
            //TODO
        }

        public void CancelAction(bool cancelEdit = false)
        {
            switch (editorAction)
            {
                case EditorAction.Move:
                    foreach (BeefyObject bo in SelectedObjects)
                    {
                        bo.GetComponent<BeefyTransform>().Coordinates = bo.GetComponent<BeefyTransform>().LastCoordinates;
                        SyncBounds(bo);
                    }
                    translation = Vector2.Zero;                    
                    break;
                case EditorAction.Rotate:
                    foreach (BeefyObject bo in SelectedObjects)
                    {
                        bo.GetComponent<BeefyTransform>().Rotation = bo.GetComponent<BeefyTransform>().LastRotation;
                        SyncBounds(bo);
                    }
                    rotation = 0;
                    break;
                case EditorAction.Scale:
                    foreach (BeefyObject bo in SelectedObjects)
                    {
                        bo.GetComponent<BeefyTransform>().Scale = bo.GetComponent<BeefyTransform>().LastScale;
                        SyncBounds(bo);
                    }
                    sXY = 1;
                    break;
                case EditorAction.EditOrigin:
                    foreach (BeefyObject bo in SelectedObjects)
                    {
                        if (bo.HasComponent<BeefyRenderer2D>())
                        {
                            SelectionBoundaries[bo.ObjectID].SetOrigin(bo.GetComponent<BeefyTransform>().Coordinates + bo.GetComponent<BeefyRenderer2D>().Origin);
                        }                        
                    }
                    break;
            }
            if (cancelEdit)
            {
                editorAction = EditorAction.None;
                axis = EditorAxis.XY;
            }                
        }               

        private void RecalculateCulling()
        {
            CullingRect = new Rectangle((int)(View.Position.X - (VPWidth / 2) / View.Zoom), (int)(- View.Position.Y + (VPHeight / 2) / View.Zoom), (int)(VPWidth / View.Zoom), (int)(VPHeight / View.Zoom));
        }

        private void InspectorUpdate()
        {
            if (SelectedObjects.Count == 1)
            {
                foreach (Control ctrl in InspectorPanel.Controls)
                {
                    if (ctrl is InspectorComponent)
                    {
                        ((InspectorComponent)ctrl).UpdateParameters();
                    }
                }
            }
            else
            {
                InspectorLabel.Text = "Inspector";
                foreach (Control ctrl in InspectorPanel.Controls)
                {
                    if (ctrl.Name.Contains("Component"))
                    {
                        InspectorPanel.Controls.Remove(ctrl);
                    }
                }
            }
        }

        private void GetModifierOrigin()
        {
            modifierOrigin = new Vector2(0, 0);
            foreach (BeefyObject bo in SelectedObjects)
            {                
                BeefyTransform bt = bo.GetComponent<BeefyTransform>();
                if (bo.IsAbstract)
                {
                    modifierOrigin += bt.Coordinates + new Vector2(33, -33);
                }
                else
                {
                    BeefyRenderer2D br = bo.GetComponent<BeefyRenderer2D>();
                    if (br != null)
                        modifierOrigin += bt.Coordinates + br.Origin;
                    else
                        modifierOrigin += bt.Coordinates;
                }
            }
            modifierOrigin /= SelectedObjects.Count;
        }

        /// <summary>
        /// Checks if ID exists already for a Game Object. Returns a new ID if identical ID found.
        /// </summary>
        /// <returns>New ID if ID is repeated. Old ID if not.</returns>
        private string CheckObjectID(BeefyObject bo)
        {
            while (Level.BOC.Exists(x => x.ObjectID == bo.ObjectID))
            {
                //Potential Bug needs Fixing
                string[] ret = bo.ObjectID.Split('|');
                if (int.TryParse(ret.Last(), out int x))
                {
                    ret[ret.Count() - 1] = (x + 1).ToString();
                    bo.ObjectID = ""; //Initialize
                    for (int i = 0; i < ret.Count() - 1; i++)
                    {
                        bo.ObjectID = ret[i] + "|";
                    }
                    bo.ObjectID += ret.Last();
                }
                else
                {                    
                    bo.ObjectID += "|1";
                }
                
            }
            return bo.ObjectID;
        }

        /// <summary>
        /// Converts a +X +Y rectangle to a +X -Y rectangle
        /// </summary>
        /// <param name="rect">Rectangle to Concert</param>
        /// <returns>Returns a rectangle in +X -Y drawing coords</returns>
        private Rectangle ConvertRectCoords(Rectangle rect)
        {
            return new Rectangle(rect.X, -rect.Y, rect.Width, rect.Height);
        }

        private Vector2 ConvertVec2Coords(Vector2 v2)
        {
            return new Vector2(v2.X, -v2.Y);
        }

        private Vector2 FindFocus()
        {
            if (SelectedObjects.Count == 0)
            {
                return Vector2.Zero;
            }
            else
            {
                Vector2 focusPoint = new Vector2(0, 0);
                foreach (BeefyObject bo in SelectedObjects)
                {
                    focusPoint += Level.BOC.Find(x => x.ObjectID == bo.ObjectID).GetComponent<BeefyTransform>().Coordinates;
                }
                focusPoint /= SelectedObjects.Count;
                return focusPoint; 
            }
        }

        private int ToDegrees(double radians)
        {
            return (int)(radians/Math.PI*180);
        }

        public void SyncBounds(BeefyObject bo)
        {
            SelectionBoundaries[bo.ObjectID] = GetBoundary(bo);
            SelectionBoundaries[bo.ObjectID].SetOrigin(bo.GetComponent<BeefyTransform>().Coordinates);
            if (!bo.IsAbstract)
            {
                SelectionBoundaries[bo.ObjectID].Scale(bo.GetComponent<BeefyTransform>().Scale);
                SelectionBoundaries[bo.ObjectID].Rotate(-bo.GetComponent<BeefyTransform>().Rotation * Math.PI / 180f);
            }
        }

        private void ApplyModification(string id, string tag, object delta)
        {
            if (Level.BOC.Exists(x => x.ObjectID == id))
            {
                BeefyObject target = Level.BOC.Find(x => x.ObjectID == id);
                string[] splitTag = tag.Split('_');
                switch (splitTag[0])
                {
                    case "TRANSFORM":
                        if (target.HasComponent<BeefyTransform>())
                            switch (splitTag[1])
                            {
                                case "MOVE":
                                    target.GetComponent<BeefyTransform>().Coordinates -= (Vector2)delta;
                                    break;
                                case "SCALE":
                                    target.GetComponent<BeefyTransform>().Scale /= (Vector2)delta;
                                    break;
                                case "ROTATE":
                                    target.GetComponent<BeefyTransform>().Rotation -= (int)delta;
                                    break;
                                case "DEPTH":
                                    target.GetComponent<BeefyTransform>().Depth = (float)delta;                                    
                                    break;
                                case "ENABLE":
                                    target.GetComponent<BeefyTransform>().Enable();
                                    break;
                                case "DISABLE":
                                    target.GetComponent<BeefyTransform>().Disable();
                                    break;
                            }
                        break;
                    case "RENDER":
                        if (target.HasComponent<BeefyRenderer2D>())
                            switch (splitTag[1])
                            {

                            }
                        break;
                    case "PHYSICS":
                        break;
                }
            }
        }

        /// <summary>
        /// Draws the Game Object in the Editor.
        /// </summary>
        /// <param name="bo"></param>
        private void EditorDraw(BeefyObject bo)
        {
            Editor.spriteBatch.Draw(bo.GetComponent<BeefyRenderer2D>().Texture,
                ConvertVec2Coords(bo.GetComponent<BeefyRenderer2D>().RenderCoordinates * View.Zoom),
                null,
                bo.GetComponent<BeefyRenderer2D>().SourceRectangle,
                bo.GetComponent<BeefyRenderer2D>().Origin,
                bo.GetComponent<BeefyRenderer2D>().Rotation,
                bo.GetComponent<BeefyRenderer2D>().Scaling * new Vector2(View.Zoom),
                bo.GetComponent<BeefyRenderer2D>().Tint * GetObjectLayer(bo).LayerAlpha,
                bo.GetComponent<BeefyRenderer2D>().SpriteEffects,
                bo.GetComponent<BeefyTransform>().Depth);
            //Console.WriteLine(bo.GetComponent<BeefyRenderer2D>().Origin);
        }

        private void EditorDraw(Texture2D tex, Vector2 coords, Vector2 offset = default /*origin*/, Vector2 scaling = default, float rot = 0, Color color = default, Single depth = 0, SpriteEffects se = SpriteEffects.None)
        {
            if (color == default)
            {
                color = Color.White;
            }
            Editor.spriteBatch.Draw(tex,
                ConvertVec2Coords(coords * View.Zoom),
                null,
                null,
                offset,
                rot,
                new Vector2(View.Zoom) * scaling,
                color,
                se,
                depth);
        }

        /// <summary>
        /// Checks if a Game Object needs to be culled in editor view
        /// </summary>
        /// <param name="bo"></param>
        /// <returns>True if needs to be culled</returns>
        private bool CheckCulling(BeefyObject bo)
        {
            if (BeefyShape.IsIntersecting(new BeefyShape(CullingRect), SelectionBoundaries[bo.ObjectID]))
                return false;
            else
                return true;
        }

        
        /// <summary>
        /// Gets the boundary of a Game Object
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        private BeefyShape GetBoundary(BeefyObject bo)
        {
            BeefyShape boundary = new BeefyShape();
            BeefyTransform bt = bo.GetComponent<BeefyTransform>();
            BeefyRenderer2D br = bo.GetComponent<BeefyRenderer2D>();
            if (!bo.IsAbstract)
            {
                Vector2 firstVert = bt.Coordinates - new Vector2(br.Origin.X, -br.Origin.Y);
                if (br != null)
                {
                    boundary.AddVertex(firstVert);
                    boundary.AddVertex(firstVert + new Vector2(br.Texture.Width / br.PixelScaling, 0));
                    boundary.AddVertex(firstVert + new Vector2(br.Texture.Width / br.PixelScaling, -br.Texture.Height / br.PixelScaling));
                    boundary.AddVertex(firstVert + new Vector2(0, -br.Texture.Height / br.PixelScaling));
                }            
            }
            else
            {
                boundary.AddVertex(bt.Coordinates - new Vector2(33f / EditorSettings.PixelScale, 32f / EditorSettings.PixelScale));
                boundary.AddVertex(bt.Coordinates + new Vector2(32f / EditorSettings.PixelScale, -32f / EditorSettings.PixelScale));
                boundary.AddVertex(bt.Coordinates + new Vector2(32f / EditorSettings.PixelScale, 33f / EditorSettings.PixelScale));
                boundary.AddVertex(bt.Coordinates + new Vector2(-33f / EditorSettings.PixelScale, 33f / EditorSettings.PixelScale));
            }                
            return boundary;
        }

        private BeefyLayer GetObjectLayer(BeefyObject bo)
        {
            foreach (BeefyLayer bl in Level.Layers)
            {
                if (bl.HasObject(bo))
                    return bl;
            }
            return null;
        }

        public BeefyLayer GetCurrentLayer()
        {
            return currentLayer;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MousePosStr = "Mouse Pos:[" + Math.Round(EditorMousePos.X, 2).ToString() + "," + Math.Round(EditorMousePos.Y, 2).ToString() + "]";
            Editor.Cam.Position = View.Position * View.Zoom;
            RecalculateCulling();
            DrawnObjects = new List<BeefyObject>();
            if (showAllLayers)
            {
                foreach (BeefyObject bo in Level.BOC)
                {
                    if (!CheckCulling(bo))
                        DrawnObjects.Add(bo);
                }
            }
            else
            {
                foreach (BeefyObject bo in currentLayer.BOC)
                {
                    if (!CheckCulling(bo))
                        DrawnObjects.Add(bo);
                }
            }
            InspectorUpdate();
            if (Focused)
            switch (editorAction)
            {
                case EditorAction.None:
                    Mouse.SetCursor(MouseCursor.Arrow);
                    switch (editorManipulator)
                    {
                        case EditorManipulator.NoEdit:
                            Status.Text = "Ready";
                            break;
                        case EditorManipulator.Rotation:
                            Status.Text = "Ready - Rotation standby";
                            break;
                        case EditorManipulator.Scaling:
                            Status.Text = "Ready - Scaling standby";
                            break;
                        case EditorManipulator.Translation:
                            Status.Text = "Ready - Translation standby";
                            break;
                    }
                    break;
                case EditorAction.BoxSelect:
                    Mouse.SetCursor(MouseCursor.Crosshair);
                    if (selection_firstPointSet)
                    {
                        Status.Text = "Box Select - " + selection_Box.Location + " to " + EditorMousePos;
                    }
                    else
                    {
                        Status.Text = "Box Select - " + EditorMousePos;
                    }
                    break;
                case EditorAction.Move:
                    Mouse.SetCursor(MouseCursor.SizeAll);
                    if (axis == EditorAxis.X)
                        Status.Text = "Translating - " + "X:" + translation.X;
                    else if (axis == EditorAxis.Y)
                        Status.Text = "Translating - " + "Y:" + translation.Y;
                    else if (axis == EditorAxis.XY)
                        Status.Text = "Translating - " + translation;
                    break;
                case EditorAction.Scale:
                    Mouse.SetCursor(MouseCursor.SizeNESW);
                    if (axis == EditorAxis.X)
                        Status.Text = "Scaling - " + "X:" + sXY;
                    else if (axis == EditorAxis.Y)
                        Status.Text = "Scaling - " + "Y:" + sXY;
                    else if (axis == EditorAxis.XY)
                        Status.Text = "Scaling - " + sXY;
                    break;
                case EditorAction.Rotate:
                    Mouse.SetCursor(MouseCursor.SizeNS);
                    Status.Text = "Rotating - " + ToDegrees(rotation - startRotation) + "°";
                    break;
                case EditorAction.EditOrigin:
                    Mouse.SetCursor(MouseCursor.Hand);
                    Status.Text = "Moving Origin - " + EditorMousePos;
                    break;
                case EditorAction.AddNew:
                    Mouse.SetCursor(MouseCursor.Crosshair);
                    Status.Text = "Add New " + statVar + " At " + EditorMousePos;
                    break;
                case EditorAction.Draw:
                    Mouse.SetCursor(MouseCursor.Crosshair);
                    Status.Text = "Drawing Commentary";
                    break;
            }
        }

        /// <summary>
        /// Note:
        /// +X -Y Coords used for drawing!
        /// </summary>
        protected override void Draw()
        {
            Editor.BackgroundColor = new Color(50, 50, 50);
            base.Draw();
            Editor.BeginCamera2D(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp);
            Editor.Cam.Position = View.Position * View.Zoom;
            ///Underlying grid
            if (View.Zoom <= 2)
            {
                EditorSettings.GridSize = 100;
            }
            else if (View.Zoom <= 32)
            {
                EditorSettings.GridSize = 10;
            }
            else if (View.Zoom <= 256)
            {
                EditorSettings.GridSize = 1;
            }
            else
            {
                EditorSettings.GridSize = 1;
            }
            int minorGridSize = (int)(EditorSettings.GridSize * View.Zoom);
            int majorGridSize = 9 * minorGridSize;
            Color minorGridColor = Color.Gray * (0.85f - zoomLvl * 0.05f);
            //TODO : Bug fix. Lines disappear when zooming in 256x at 800,800
            for (int i = minorGridSize; i < VPWidth + (int)(Editor.Cam.Position.X); i += minorGridSize)
            {
                if (i % majorGridSize == 0)
                    Editor.spriteBatch.Draw(Editor.Pixel, new Rectangle(i, (int)Editor.Cam.Position.Y - VPHeight / 2, 1, VPHeight), null, Color.Gray * 0.85f, 0, default(Vector2), SpriteEffects.None, 0.9998f);
                else
                    Editor.spriteBatch.Draw(Editor.Pixel, new Rectangle(i, (int)Editor.Cam.Position.Y - VPHeight / 2, 1, VPHeight), null, minorGridColor, 0, default(Vector2), SpriteEffects.None, 0.9998f);
            }            
            for (int i = -minorGridSize; i > -VPWidth + (int)(Editor.Cam.Position.X); i -= minorGridSize)
            {
                if (i % majorGridSize == 0)
                    Editor.spriteBatch.Draw(Editor.Pixel, new Rectangle(i, (int)Editor.Cam.Position.Y - VPHeight / 2, 1, VPHeight), null, Color.Gray * 0.85f, 0, default(Vector2), SpriteEffects.None, 0.9998f);
                else
                    Editor.spriteBatch.Draw(Editor.Pixel, new Rectangle(i, (int)Editor.Cam.Position.Y - VPHeight / 2, 1, VPHeight), null, minorGridColor, 0, default(Vector2), SpriteEffects.None, 0.9998f);
            }
            for (int i = minorGridSize; i < VPHeight + (int)(Editor.Cam.Position.Y); i += minorGridSize)
            {
                if (i % majorGridSize == 0)
                    Editor.spriteBatch.Draw(Editor.Pixel, new Rectangle((int)Editor.Cam.Position.X - VPWidth / 2, i, VPWidth, 1), null, Color.Gray * 0.85f, 0, default(Vector2), SpriteEffects.None, 0.9998f);
                else
                    Editor.spriteBatch.Draw(Editor.Pixel, new Rectangle((int)Editor.Cam.Position.X - VPWidth / 2, i, VPWidth, 1), null, minorGridColor, 0, default(Vector2), SpriteEffects.None, 0.9998f);
            }
            for (int i = -minorGridSize; i > -VPHeight + (int)(Editor.Cam.Position.Y); i -= minorGridSize)
            {
                if (i % majorGridSize == 0)
                    Editor.spriteBatch.Draw(Editor.Pixel, new Rectangle((int)Editor.Cam.Position.X - VPWidth / 2, i, VPWidth, 1), null, Color.Gray * 0.85f, 0, default(Vector2), SpriteEffects.None, 0.9998f);
                else
                    Editor.spriteBatch.Draw(Editor.Pixel, new Rectangle((int)Editor.Cam.Position.X - VPWidth / 2, i, VPWidth, 1), null, minorGridColor, 0, default(Vector2), SpriteEffects.None, 0.9998f);
            }            
            ///X-Y Axis & Origin            
            if (CullingRect.Y >= 0 && (CullingRect.Y - CullingRect.Height) <= 0)
                Editor.spriteBatch.Draw(Editor.Pixel, new Rectangle((int)Editor.Cam.Position.X - VPWidth / 2, 0, VPWidth, 1), null, EditorSettings.XAxisColor * 0.85f, 0, default(Vector2), SpriteEffects.None, 0.9997f); //X-Axis
            if (CullingRect.X <= 0 && (CullingRect.X + CullingRect.Width) >= 0)
                Editor.spriteBatch.Draw(Editor.Pixel, new Rectangle(0, (int)Editor.Cam.Position.Y - VPHeight / 2, 1, VPHeight), null, EditorSettings.YAxisColor * 0.85f, 0, default(Vector2), SpriteEffects.None, 0.9997f); //Y-Axis
            for (int i = -2; i < 3; i++)
                for (int j = -2; j < 3; j++)
                    if ((i * i + j * j) != 8)
                        Editor.spriteBatch.Draw(Editor.Pixel, new Rectangle(i, j, 1, 1), null, EditorSettings.OriginColor, 0, default(Vector2), SpriteEffects.None, 0.9996f);
            ///Level Elements
            foreach (BeefyObject bo in DrawnObjects)
            {
                if (bo.IsAbstract)
                {
                    EditorDraw(AbstractIcon, bo.GetComponent<BeefyTransform>().Coordinates, new Vector2(33, 33), new Vector2(1) / EditorSettings.PixelScale, 0f, Color.White * GetObjectLayer(bo).LayerAlpha, bo.GetComponent<BeefyTransform>().Depth);
                }
                else
                {
                    if (bo.GetComponent<BeefyRenderer2D>()!=null && bo.GetComponent<BeefyRenderer2D>().Enabled)
                        EditorDraw(bo);
                }
                if (SelectedObjects.Exists(x => x.ObjectID == bo.ObjectID))
                {
                    #region Draw Object Boundary
                    BeefyShape bs = (BeefyShape)SelectionBoundaries[bo.ObjectID].Clone();
                    bs.SetOrigin(Vector2.Zero);
                    bs.Scale(View.Zoom, false);
                    if (editorAction == EditorAction.Move || editorAction == EditorAction.Rotate || editorAction == EditorAction.Scale)
                        GraphingTools.SetColor(Color.White);
                    else
                        GraphingTools.SetColor(Color.Orange);
                    GraphingTools.PlotShape(bs, 0.01f);
                    #endregion
                    #region Draw Object Origin
                    bs.SetOrigin(SelectionBoundaries[bo.ObjectID].Origin);
                    Vector2 o = bs.Origin;
                    o *= View.Zoom;
                    for (int i = -2; i < 3; i++)
                        for (int j = - 2; j < 3; j++)
                            if ((i * i + j * j) != 8)
                            {
                                GraphingTools.SetColor(Color.Orange);
                                GraphingTools.PlotPoint((int)o.X + i, (int)o.Y + j, 0.005f);
                            }
                            else
                            {
                                GraphingTools.SetColor(Color.Black);
                                GraphingTools.PlotPoint((int)o.X + i, (int)o.Y + j, 0.0075f);
                            }
                    GraphingTools.SetColor(Color.Black);
                    for (int i = -1; i < 2; i++)
                        GraphingTools.PlotPoint((int)o.X + i, (int)o.Y - 3, 0.0075f);
                    for (int i = -1; i < 2; i++)
                        GraphingTools.PlotPoint((int)o.X + i, (int)o.Y + 3, 0.0075f);
                    for (int i = -1; i < 2; i++)
                        GraphingTools.PlotPoint((int)o.X - 3, (int)o.Y + i, 0.0075f);
                    for (int i = -1; i < 2; i++)
                        GraphingTools.PlotPoint((int)o.X + 3, (int)o.Y + i, 0.0075f);
                    #endregion
                }
            }            
            Editor.EndCamera2D();            

            #region Draw Editor Action
            Editor.spriteBatch.Begin();
            switch (editorAction)
            {
                #region Box Select GUI
                case EditorAction.BoxSelect:
                    #region Selection Box Drawing
                    Rectangle outerRect = new Rectangle(selection_firstPoint.X, selection_firstPoint.Y, Editor.GetRelativeMousePosition.X - selection_firstPoint.X, Editor.GetRelativeMousePosition.Y - selection_firstPoint.Y);
                    if (selection_firstPointSet)
                    {
                        //Fixed selection box drawing
                        if (selection_Box.Width < 0 && selection_Box.Height > 0)
                        {
                            outerRect = new Rectangle(Editor.GetRelativeMousePosition.X, selection_firstPoint.Y, Math.Abs(Editor.GetRelativeMousePosition.X - selection_firstPoint.X), Editor.GetRelativeMousePosition.Y - selection_firstPoint.Y);
                        }
                        else if (selection_Box.Width > 0 && selection_Box.Height < 0)
                        {
                            outerRect = new Rectangle(selection_firstPoint.X, Editor.GetRelativeMousePosition.Y, Editor.GetRelativeMousePosition.X - selection_firstPoint.X, Math.Abs(Editor.GetRelativeMousePosition.Y - selection_firstPoint.Y));
                        }
                        Rectangle innerRect = new Rectangle(outerRect.X + 2, outerRect.Y + 2, outerRect.Width - 4, outerRect.Height - 4);
                        Rectangle topBorder = new Rectangle(outerRect.Location, new Point(outerRect.Width - 2, 2));
                        Rectangle leftBorder = new Rectangle(new Point(outerRect.Left, outerRect.Top + 2), new Point(2, outerRect.Height - 4));
                        Rectangle bottomBorder = new Rectangle(new Point(outerRect.Left, outerRect.Bottom - 2), new Point(outerRect.Width, 2));
                        Rectangle rightBorder = new Rectangle(new Point(outerRect.Right - 2, outerRect.Top), new Point(2, outerRect.Height - 2));
                        if (selection_Box.Width < 0 && selection_Box.Height < 0)
                        {
                            outerRect = new Rectangle(selection_firstPoint.X + (int)(selection_Box.Width * View.Zoom), selection_firstPoint.Y + (int)(selection_Box.Height * View.Zoom), Math.Abs(Editor.GetRelativeMousePosition.X - selection_firstPoint.X), Math.Abs(Editor.GetRelativeMousePosition.Y - selection_firstPoint.Y));
                            innerRect = new Rectangle(outerRect.X + 2, outerRect.Y + 2, outerRect.Width - 4, outerRect.Height - 4);
                            topBorder = new Rectangle(new Point(outerRect.X, outerRect.Y), new Point(outerRect.Width - 2, 2));
                            leftBorder = new Rectangle(new Point(outerRect.Left, outerRect.Top + 2), new Point(2, outerRect.Height - 4));
                            bottomBorder = new Rectangle(new Point(outerRect.Left, outerRect.Bottom - 2), new Point(outerRect.Width, 2));
                            rightBorder = new Rectangle(new Point(outerRect.Right - 2, outerRect.Top), new Point(2, outerRect.Height - 2));
                        }
                        Editor.spriteBatch.Draw(Editor.Pixel, topBorder, Color.LightSteelBlue * 0.6f);
                        Editor.spriteBatch.Draw(Editor.Pixel, rightBorder, Color.LightSteelBlue * 0.6f);
                        Editor.spriteBatch.Draw(Editor.Pixel, bottomBorder, Color.LightSteelBlue * 0.6f);
                        Editor.spriteBatch.Draw(Editor.Pixel, leftBorder, Color.LightSteelBlue * 0.6f);
                        Editor.spriteBatch.Draw(Editor.Pixel, innerRect, Color.LightSteelBlue * 0.3f);                        
                    }
                    break;
                    #endregion
                #endregion
            }
            Editor.spriteBatch.End();
            #endregion

            #region Draw Editor HUD
            Editor.spriteBatch.Begin();
            Editor.spriteBatch.DrawString(Editor.Font, "Scale:" + View.Zoom + "x", new Vector2(0, 0), Color.White);
            Editor.spriteBatch.DrawString(Editor.Font, zoomLvl.ToString(), new Vector2(0, Editor.FontHeight), Color.White);
            Editor.spriteBatch.DrawString(Editor.Font, MousePosStr, new Vector2((int)((VPWidth - Editor.Font.MeasureString(MousePosStr).X)/2), 0), Color.White);
            Editor.spriteBatch.End();
            #endregion            
        }
    }
}
