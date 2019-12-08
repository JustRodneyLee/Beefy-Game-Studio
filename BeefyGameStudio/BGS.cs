using BeefyEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BeefyGameStudio
{
    public partial class BGS : Form
    {
        FileType fileType;
        ImportAction importAction;
        
        bool lvlSaved;
        bool lvlModified;

        public BGS()
        {
            InitializeComponent();
        }

        private void BGS_Load(object sender, EventArgs e)
        {
            EditorSettings.Init();
            MainViewport.SetControls(ViewportAddMenuStrip, ViewportEditMenuStrip, LayerMenuStrip, InspectorLabel, InspectorPanel, addProperty, (ToolStripStatusLabel)StatusStrip.Items[0], AllLayersHierarchy);
            InitGameViewport();            
            InitAssetLib();
            this.Text = "Beefy Game Studio v" + EditorSettings.Version + " - " + MainViewport.Level.LevelID;
        }

        private void InitAssetLib()
        {
            AssetLibrary.LargeImageList = new ImageList();
            AssetLibrary.LargeImageList.ImageSize = new Size(72, 72);
            assetLib = new BeefyAssetLibrary(MainViewport.Level.LevelID + "Library");
        }

        private void InitGameViewport()
        {
            MainViewport.Editor.Initialize();
            MainViewport.LoadLevel(new BeefyLevel("New Level"));
            lvlSaved = false;
            lvlModified = true;
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvlSaved)
                Close();
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!lvlSaved)
            {
                SaveFileDialog.Title = "Beefy Game Studio - Save this Level";
                SaveFileDialog.Filter = "Beefy Game Levels|*.bgl";
                SaveFileDialog.DefaultExt = "bgl";
                SaveFileDialog.ShowDialog();
            }            
        }

        private void Hierarchy_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                HierarchyMenuStrip.Show(e.Location);
            }
        }

        private void AddLayerButton_Click(object sender, EventArgs e)
        {
            //MainViewport
            TabPage tp = new TabPage("Layer " + (MainViewport.GetLargestLayerCount()+1).ToString());
            if (MainViewport.AddLayer(tp.Text))
            {
                //Control names??
                TreeView tv = new TreeView();
                tp.Controls.Add(tv);
                tv.Dock = DockStyle.Fill;
                HierarchyTab.TabPages.Add(tp);
                HierarchyTab.SelectTab(tp);
            }
            else
                MessageBox.Show("Layer ID Duplicated!", "Beefy Game Studio - Error", MessageBoxButtons.OK);
        }

        private void RemoveLayerButton_Click(object sender, EventArgs e)
        {
            if (HierarchyTab.SelectedTab != AllLayersTab && HierarchyTab.TabCount > 2)
            {
                MainViewport.RemoveLayer(HierarchyTab.SelectedTab.Text);
                HierarchyTab.TabPages.Remove(HierarchyTab.SelectedTab);
            }                
            else
                MessageBox.Show("Cannot delete all Layers!", "Beefy Game Studio", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void HierarchyTab_Selected(object sender, TabControlEventArgs e)
        {
            if (HierarchyTab.SelectedTab == AllLayersTab)
            {
                MainViewport.ToggleAllLayers(true);
                TabNameLabel.Visible = false;
                OpacityLabel.Visible = false;
                OpacityTrackBar.Visible = false;
                RemoveLayerButton.Enabled = false;
                LayerNameTxtBox.Enabled = false;
                LayerNameTxtBox.Text = "";
                layerNameLabel.Text = "";                
            }
            else
            {
                MainViewport.SwitchToLayer(HierarchyTab.SelectedTab.Text);
                TabNameLabel.Visible = true;
                OpacityLabel.Visible = true;
                OpacityTrackBar.Value = (int)(MainViewport.Level.Layers.Find(x => x.LayerID == HierarchyTab.SelectedTab.Text).LayerAlpha * 100);
                OpacityTrackBar.Visible = true;
                RemoveLayerButton.Enabled = true;
                LayerNameTxtBox.Enabled = true;
                LayerNameTxtBox.Text = HierarchyTab.SelectedTab.Text;
                layerNameLabel.Text = HierarchyTab.SelectedTab.Text;
            }
        }

        private void LayerNameLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (HierarchyTab.SelectedTab != AllLayersTab)
            {
                layerNameLabel.Visible = false;
                LayerNameTxtBox.Visible = true;
                LayerNameTxtBox.Focus();
            }
        }

        private void LayerNameTxtBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MainViewport.Level.Layers.Find(x => x.LayerID == HierarchyTab.SelectedTab.Text).LayerID = LayerNameTxtBox.Text;
                HierarchyTab.SelectedTab.Text = LayerNameTxtBox.Text;                
                layerNameLabel.Text = HierarchyTab.SelectedTab.Text;
                LayerNameTxtBox.Visible = false;
                layerNameLabel.Visible = true;
                layerNameLabel.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                LayerNameTxtBox.Text = HierarchyTab.SelectedTab.Text;
                LayerNameTxtBox.Visible = false;
                layerNameLabel.Visible = true;
                layerNameLabel.Focus();
            }
        }

        private void LayerNameTxtBox_Leave(object sender, EventArgs e)
        {
            LayerNameTxtBox.Text = HierarchyTab.SelectedTab.Text;
            LayerNameTxtBox.Visible = false;
            layerNameLabel.Visible = true;
        }

        private void TextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileType = FileType.Asset;
            OpenFileDialog.Multiselect = true;
            OpenFileDialog.Title = "Beefy Game Studio - Add Texture to Library";
            OpenFileDialog.Filter = "Portable Network Graphics|*.png";
            OpenFileDialog.ShowDialog();
        }

        private void AudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog.Multiselect = true;
            OpenFileDialog.Title = "Beefy Game Studio - Add Audio to Library";
            OpenFileDialog.Filter = "Ogg Vorbis|*.ogg";
            OpenFileDialog.ShowDialog();
        }

        private void ObjectToLibToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileType = FileType.Asset;
            OpenFileDialog.Multiselect = true;
            OpenFileDialog.Title = "Beefy Game Studio - Add Object to Library";
            OpenFileDialog.Filter = "Beefy Game Object|*.bgo";
            OpenFileDialog.ShowDialog();    
        }

        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string[] paths;
            switch (fileType)
            {
                case FileType.Project:
                    
                    break;
                case FileType.Asset:
                    paths = OpenFileDialog.FileNames;
                    if (ImportAssets(paths, BeefyAssetType.Visual))
                    {
                        switch (importAction)
                        {
                            case ImportAction.ImportOnly:
                                //Just import and do nothing else                                
                                break;
                            case ImportAction.AddToScene:
                                //Add to scene                            
                                MainViewport.AddBeefyObjects(assetLib.GetAssetsByIDs(ExtractNamesFromPaths(paths)), MainViewport.MousePos);
                                break;
                            case ImportAction.WaitToAdd:
                                MainViewport.AddNewObjects(assetLib.GetAssetsByIDs(ExtractNamesFromPaths(paths)));
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Import Failed!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case FileType.Level:

                    break;
            }
        }

        private void SaveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            switch (fileType)
            {
                case FileType.Level: //Save Level
                    if (!SaveLevel(SaveFileDialog.FileName))
                    {
                        MessageBox.Show("Level cannot be saved!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case FileType.Project: //Save Project
                    //TODO
                    break;
            }
        }

        private void NewLvlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!lvlSaved || lvlModified)
            {
                DialogResult dr;
                dr = MessageBox.Show("Do you want to save last changes?", "Beefy Game Studio", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    if (lvlSaved)
                    {
                        SaveLevel(currentLevelPath);
                    }
                    else
                    {
                        SaveFileDialog.Title = "Beefy Game Studio - Save Level";
                        SaveFileDialog.Filter = "Beefy Game Levels|*.bgl";
                        SaveFileDialog.DefaultExt = "bgl";
                        SaveFileDialog.ShowDialog();
                    }
                }
                else if (dr == DialogResult.No)
                {
                    
                }
                else
                {
                    return;
                }
            }
            InitGameViewport();
        }

        private void OpenLvlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            OpenFileDialog.Multiselect = false;
            dr = OpenFileDialog.ShowDialog();
        }

        private void SaveLvlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!lvlSaved)
            {
                SaveFileDialog.Title = "Beefy Game Studio - Save Level";
                SaveFileDialog.Filter = "Beefy Game Levels|*.bgl";
                SaveFileDialog.DefaultExt = "bgl";
                fileType = FileType.Level;
                SaveFileDialog.ShowDialog();
            }
            else
            {
                SaveLevel(currentLevelPath);
            }
        }

        private void SaveLvlAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Are you sure you want to Clear the Level? You cannot undo this action.", "Beefy Game Studio - Clear Level", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr==DialogResult.Yes)
                MainViewport.ClearLevel();
        }

        private void BGS_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.Shift)
            {
                if (e.KeyCode == Keys.S)
                {
                    if (lvlSaved)
                    {
                        SaveLevel(currentLevelPath);
                    }
                    else
                    {

                    }
                }else if (e.KeyCode == Keys.C)
                {
                    if (MainViewport.Focused)
                    {
                        Clipboard.SetDataObject(MainViewport.ReturnSelected(), true);
                    }                    
                }
                else if (e.KeyCode == Keys.X)
                {
                    if (MainViewport.Focused)
                    {
                        Clipboard.SetDataObject(MainViewport.ReturnSelected(), true);
                        foreach (BeefyObject bo in MainViewport.ReturnSelected())
                            MainViewport.RemoveBeefyObject(bo);
                    }                    
                }
                else if (e.KeyCode == Keys.V)
                {
                    if (Clipboard.GetDataObject() is List<IBeefyAsset>)
                    {
                        if (MainViewport.Focused)
                        {
                            MainViewport.AddBeefyObject((IBeefyAsset)Clipboard.GetDataObject(), MainViewport.MousePos);
                        }
                    }
                    else if (Clipboard.GetDataObject() is List<BeefyObject>)
                    {
                        if (MainViewport.Focused)
                        {
                            MainViewport.AddBeefyObject((BeefyObject)Clipboard.GetDataObject());
                        }
                    }
                }
            }
        }

        private void OpacityTrackBar_Scroll(object sender, EventArgs e)
        {
            MainViewport.Level.Layers.Find(layer => layer.LayerID == HierarchyTab.SelectedTab.Text).SetAlpha(OpacityTrackBar.Value / 100f);
            MainViewport.Invalidate();
        }

        private void AssetLibrary_DragEnter(object sender, DragEventArgs e)
        {
            
        }

        private void AssetLibrary_DragLeave(object sender, EventArgs e)
        {
            
        }

        private void MainViewport_DragEnter(object sender, DragEventArgs e)
        {
            //Console.WriteLine(e.Data.GetData(typeof(string)));
            //MainViewport.AddBeefyObject();
        }

        private void AllLayersHierarchy_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {            
            MainViewport.SelectObject(e.Node.Name);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void AddProperty_Click(object sender, EventArgs e)
        {
            //TODO
            AddPropertyMenuStrip.Show(MousePosition);
        }

        #region Viewport Add Menu Strip
        private void AbstractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewport.AddAbstractObject(MainViewport.MousePos);            
        }

        private void FromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileType = FileType.Asset;
            importAction = ImportAction.AddToScene;
            OpenFileDialog.Multiselect = false;
            OpenFileDialog.Title = "Beefy Game Studio - Add Object From File";
            OpenFileDialog.Filter = "Image Files|*.bmp;*.png;*.jpg;*.jpeg|Ogg Vorbis|*.ogg|Beefy Game Object|*.bgo";
            OpenFileDialog.ShowDialog();
        }

        private void FromAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AssetLibraryForm(assetLib, AssetLibrary, MainViewport, true).ShowDialog();
        }

        private void BoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeefyObject Box = new BeefyObject("Box");
            Box.AddComponent(new BeefyRenderer2D(Box));
            Box.AddComponent(new BeefyPhysics(Box));
            MainViewport.AddBeefyObject(Box);
        }

        private void CircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeefyObject Circle = new BeefyObject("Circle");
            Circle.AddComponent(new BeefyRenderer2D(Circle));
            Circle.AddComponent(new BeefyPhysics(Circle));
            MainViewport.AddBeefyObject(Circle);
        }

        #endregion

        #region Editor Menu
        private void AddAbstractToolStripMenuItem_Click(object sender, EventArgs e)
        {                       
            MainViewport.AddNewObject(new BeefyObject(true));
        }

        private void AddBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeefyObject Box = new BeefyPresets.Box();
            MainViewport.AddNewObject(Box);
        }

        private void AddCircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeefyObject Circle = new BeefyObject("Circle");
            Circle.AddComponent(new BeefyRenderer2D(Circle));
            Circle.AddComponent(new BeefyPhysics(Circle));
            MainViewport.AddNewObject(Circle);
        }

        private void AddCharacterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void AddObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void AddFromAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AssetLibraryForm(assetLib, AssetLibrary, MainViewport, false).ShowDialog();
        }

        private void AddFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importAction = ImportAction.WaitToAdd;
            fileType = FileType.Asset;
            OpenFileDialog.Multiselect = false;
            OpenFileDialog.Title = "Beefy Game Studio - Add Object From File";
            OpenFileDialog.Filter = "Image Files|*.bmp;*.png;*.jpg;*.jpeg|Ogg Vorbis|*.ogg|Beefy Game Object|*.bgo";
            OpenFileDialog.ShowDialog();
        }
        #endregion

        private void AddPropertyToolStripTextBox_Leave(object sender, EventArgs e)
        {
            addPropertyToolStripTextBox.ForeColor = Color.LightGray;
            addPropertyToolStripTextBox.Text = "Search for a Property...";
        }

        private void AddPropertyToolStripTextBox_Click(object sender, EventArgs e)
        {
            addPropertyToolStripTextBox.ForeColor = Color.Black;
            addPropertyToolStripTextBox.Text = "";
        }

        private void MainViewport_Resize(object sender, EventArgs e)
        {
            MainViewport.Update();
        }

        #region Editing Menu

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewport.Undo();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewport.Redo();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Add Property Menu

        private void AddRenderer2DPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewport.InspectedObject.AddComponent(new BeefyRenderer2D(MainViewport.InspectedObject));
            MainViewport.Invalidate();
        }

        private void AddAudioPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewport.InspectedObject.AddComponent(new BeefyAudio(MainViewport.InspectedObject));
            MainViewport.Invalidate();
        }

        private void AddPhysicsPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewport.InspectedObject.AddComponent(new BeefyPhysics(MainViewport.InspectedObject));
            MainViewport.Inspect(MainViewport.InspectedObject);
            MainViewport.Invalidate();
        }

        private void AddInputPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewport.InspectedObject.AddComponent(new BeefyInputController(MainViewport.InspectedObject));
            MainViewport.Inspect(MainViewport.InspectedObject);
            MainViewport.Invalidate();
        }

        private void AddCameraPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MainViewport.InspectedObject.AddComponent(new BeefyCameraTracking(MainViewport.InspectedObject));
            MainViewport.Invalidate();
        }

        private void CustomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewport.InspectedObject.AddComponent(new BeefyCustomProperty(MainViewport.InspectedObject));
            MainViewport.Invalidate();
        }
        #endregion

        #region Asset Library 
        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO
            OpenFileDialog.ShowDialog();
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO
            SaveFileDialog.ShowDialog();
        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (assetLib.Assets.Count == 0)
            {
                assetLib.Reset();
            }
            else
            {
                DialogResult dr = MessageBox.Show("You are about to reset library " + assetLib.LibraryName + ", are you sure?", "Beefy Game Studio - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    assetLib.Reset();
                }
            }
        }
        #endregion

        private void HotkeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Hotkeys().ShowDialog();
        }        

        private void LayerMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            LayerMenuStrip.Items.Clear();
            foreach (BeefyLayer bl in MainViewport.Level.Layers)
            {
                if (bl.LayerID != MainViewport.GetCurrentLayer().LayerID)
                {
                    ToolStripItem item = LayerMenuStrip.Items.Add("Move To " + bl.LayerID);
                    item.Click += (_sender, _e) => layerMenuItem_Clicked(_sender, _e, bl);
                }                    
            }
        }

        private void layerMenuItem_Clicked(object sender, EventArgs e, BeefyLayer bl)
        {
            MainViewport.MoveObjectsToLayer(MainViewport.ReturnSelected(), bl);
        }

        private void BGS_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void InspectorVSHierarchy_Panel2_Resize(object sender, EventArgs e)
        {
//            MainViewport.Inspect(MainViewport.InspectedObject);
        }

        private void editingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainViewport.CanRedo)
                redoToolStripMenuItem.Enabled = true;
            else
                redoToolStripMenuItem.Enabled = false;
            if (MainViewport.CanUndo)
                undoToolStripMenuItem.Enabled = true;
            else
                undoToolStripMenuItem.Enabled = false;
            if (MainViewport.ReturnSelected().Count!=0)
            {
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                pasteToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
            }
            else
            {
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                pasteToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
            }
        }

        private void newProjToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openProjToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveProjToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
