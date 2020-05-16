using BeefyGameEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

        ScriptEditorForm scriptEditor;
        BeefyHub beefyHub;

        public BGS(BeefyHub hub)
        {
            InitializeComponent();
            beefyHub = hub;                   
        }

        private void BGS_Load(object sender, EventArgs e)
        {
            MainViewport.SetControls(ViewportAddMenuStrip, ViewportEditMenuStrip, LayerMenuStrip, InspectorLabel, InspectorPanel, addProperty, (ToolStripStatusLabel)StatusStrip.Items[0], AllLayersHierarchy);
            InitGameViewport();
            InitAssetLib();
            InspectorPanel.HorizontalScroll.Enabled = false;
            RefreshTitleText();
            BeefyPresets.SetGraphicsDevice(MainViewport.Editor.graphics);
        }

        /// <summary>
        /// Refreshes BGS's Title Text
        /// </summary>
        public void RefreshTitleText()
        {
            if (MainViewport.Level != null)
            {
                if (CurrentProject.IsNull)
                    Text = "Beefy Game Studio v" + EditorSettings.Version + " - " + MainViewport.Level.LevelID;
                else
                    Text = "Beefy Game Studio v" + EditorSettings.Version + " - " + CurrentProject.ProjectName + " - " + MainViewport.Level.LevelID;
            }
            else
                Text = "Beefy Game Studio v" + EditorSettings.Version;
                
        }

        private void InitAssetLib()
        {
            AssetLibrary.LargeImageList = new ImageList();
            AssetLibrary.LargeImageList.ImageSize = new Size(72, 72);
            AssetLib = new BeefyAssetLibrary(MainViewport.Level.LevelID + "Library");
        }

        private void InitGameViewport()
        {
            MainViewport.InternalLoad(new BeefyLevel("New Level"));
            MainViewport.Editor.Initialize();                  
            lvlSaved = false;
            lvlModified = true;
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

        private void addScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileType = FileType.Asset;
            OpenFileDialog.Multiselect = true;
            OpenFileDialog.Title = "Beefy Game Studio - Add Script to Library";
            OpenFileDialog.Filter = "Beefy Game Script|*.bgs";
            OpenFileDialog.ShowDialog();
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
            fileType = FileType.Asset;
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
                    OpenProject(OpenFileDialog.FileName);
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
                                MainViewport.AddBeefyObjects(AssetLib.GetAssetsByIDs(ExtractNamesFromPaths(paths)), MainViewport.MousePos);
                                break;
                            case ImportAction.WaitToAdd:
                                MainViewport.AddNewObjects(AssetLib.GetAssetsByIDs(ExtractNamesFromPaths(paths)));
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
                    else
                    {
                        lvlSaved = true;
                        lvlModified = false;
                    }
                    break;
                case FileType.Project: //Save Project
                    SaveProject();
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
                        SaveLevel(CurrentProject.CurrentLevelPath);
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
                    //Do Nothing
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

        /// <summary>
        /// Saves the current editor level and asks the user before doing so
        /// </summary>
        private void SaveCurrentLevel()
        {
            if (CurrentProject.IsNull)
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
                    SaveLevel(CurrentProject.CurrentLevelPath);
                    RefreshTitleText();
                }
            }
            else
            {
                if (!lvlSaved)
                {
                    NameDialog nd = new NameDialog("Save Current Level");
                    nd.ShowDialog();
                    if (nd.DialogResult == DialogResult.OK)
                    {
                        if (CurrentProject.LevelIDs.Contains(nd.NameValue))
                        {
                            MessageBox.Show("A Level with this name already exists!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            nd.Close();
                            MainViewport.Level.LevelID = nd.NameValue;
                            SaveLevel(CurrentProject.LevelsPath + "\\" + nd.NameValue + ".bgl");
                            RefreshTitleText();
                        }
                    }
                }
                else
                {
                    SaveLevel(CurrentProject.CurrentLevelPath);
                    RefreshTitleText();
                }
            }
        }

        private void SaveLvlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrentLevel();
        }

        private void SaveLvlAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentProject.IsNull)
            {
                SaveFileDialog.Title = "Beefy Game Studio - Save Level As";
                SaveFileDialog.Filter = "Beefy Game Levels|*.bgl";
                SaveFileDialog.DefaultExt = "bgl";
                fileType = FileType.Level;
                SaveFileDialog.ShowDialog();
            }
            else
            {
                NameDialog nd = new NameDialog("Save Current Level");
                nd.ShowDialog();
                if (nd.DialogResult == DialogResult.OK)
                {
                    if (CurrentProject.LevelIDs.Contains(nd.NameValue))
                    {
                        MessageBox.Show("A Level with this name already exists!", "Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        nd.Close();
                        MainViewport.Level.LevelID = nd.NameValue;
                        SaveLevel(CurrentProject.LevelsPath + "\\" + nd.NameValue + ".bgl");
                    }
                }                
            }
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
            if (ModifierKeys == Keys.Control)
            {
                if (e.KeyCode == Keys.S)
                {
                    SaveCurrentLevel();
                }
                else if (e.KeyCode == Keys.C)
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
            //TODO
        }

        private void AssetLibrary_DragLeave(object sender, EventArgs e)
        {
            //TODO
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
            new AssetLibraryForm(AssetLib, AssetLibrary, MainViewport, true).ShowDialog();
        }

        private void BoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeefyObject Box = new BeefyPresets.Box();
            Box.GetComponent<BeefyTransform>().Coordinates = MainViewport.MousePos;
            Box.GetComponent<BeefyTransform>().LastCoordinates = MainViewport.MousePos;
            MainViewport.AddBeefyObject(Box);
        }

        private void CircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeefyObject Circle = new BeefyPresets.Circle();
            Circle.GetComponent<BeefyTransform>().Coordinates = MainViewport.MousePos;
            Circle.GetComponent<BeefyTransform>().LastCoordinates = MainViewport.MousePos;
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
            BeefyObject Circle = new BeefyPresets.Circle();            
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
            new AssetLibraryForm(AssetLib, AssetLibrary, MainViewport, false).ShowDialog();
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
            MainViewport.Inspect(MainViewport.InspectedObject);
        }

        private void AddAudioPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewport.InspectedObject.AddComponent(new BeefyAudio(MainViewport.InspectedObject));
            MainViewport.Inspect(MainViewport.InspectedObject);
        }

        private void AddPhysicsPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewport.InspectedObject.AddComponent(new BeefyPhysics(MainViewport.InspectedObject));
            MainViewport.Inspect(MainViewport.InspectedObject);
        }

        private void AddInputPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewport.InspectedObject.AddComponent(new BeefyInputController(MainViewport.InspectedObject));
            MainViewport.Inspect(MainViewport.InspectedObject);
        }

        private void AddCameraPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MainViewport.InspectedObject.AddComponent(new BeefyCameraTracking(MainViewport.InspectedObject));
            MainViewport.Inspect(MainViewport.InspectedObject);
        }

        private void CustomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewport.InspectedObject.AddComponent(new BeefyCustomProperty(MainViewport.InspectedObject));
            MainViewport.Inspect(MainViewport.InspectedObject);
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
            if (AssetLib.Assets.Count == 0)
            {
                AssetLib.Reset();
            }
            else
            {
                DialogResult dr = MessageBox.Show("You are about to reset library " + AssetLib.LibraryName + ", are you sure?", "Beefy Game Studio - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    AssetLib.Reset();
                }
            }
        }

        private void addObjectToLibToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileType = FileType.Asset;
            OpenFileDialog.Multiselect = true;
            OpenFileDialog.Title = "Beefy Game Studio - Add Object to Library";
            OpenFileDialog.Filter = "Beefy Game Object|*.bgo";
            OpenFileDialog.ShowDialog();
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
            //beefyHub.Show();
            Dispose();
            Application.Exit();
        }

        private void InspectorVSHierarchy_Panel2_Resize(object sender, EventArgs e)
        {
            foreach (Control c in InspectorPanel.Controls)
            {
                c.Width = InspectorPanel.Width - 2 * InspectorPanel.Margin.Horizontal;
            }
        }

        private void editingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (true)
                redoToolStripMenuItem.Enabled = true;
            else
                redoToolStripMenuItem.Enabled = false;
            if (true)
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

        #region Project Menu
        private void newProjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProject np = new NewProject();
            if (np.ShowDialog()==DialogResult.OK)
            {
                NewProject(np.ProjName, np.ProjPath);
                np.Dispose();
            }
        }

        private void openProjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog.Multiselect = false;
            OpenFileDialog.Title = "Beefy Game Studio - Open Project";
            OpenFileDialog.Filter = "Beefy Game Project(*.bgp)|*.bgp";
            OpenFileDialog.ShowDialog();
        }

        private void saveProjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrentLevel();
            SaveProject();
        }

        private void projSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentProject.IsNull)
                MessageBox.Show("You are not editing a project!","Beefy Game Studio - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                new ProjectSettings().ShowDialog();            
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvlSaved)
                Close();
            else
            {
                SaveCurrentLevel();
                SaveProject();
            }
        }
        #endregion

        private void InspectorPanel_Resize(object sender, EventArgs e)
        {
            foreach (Control c in InspectorPanel.Controls)
            {
                c.Width = InspectorPanel.Width - 2 * InspectorPanel.Margin.Horizontal;
            }
        }

        private void AssetLibrary_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            MainViewport.DeselectAll();            
        }

        private void buildProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuildProject();
        }

        private void buildAssetLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuildAssets();
        }        

        private void runProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            void RunProjectStandalone()
            {
                Process process = Process.Start(CurrentProject.BuildPath + "\\" + CurrentProject.ProjectExe);
                StatusStrip.Text = CurrentProject.ProjectName + " Running.";
                process.WaitForExit();
            }

            if (CurrentProject.ProjectBuilt)
            {
                RunProjectStandalone();
            }
            else
            {
                DialogResult dr = MessageBox.Show("Do you want to rebuild your project? If you choose no, your last build will be run.","Beefy Game Studio - Request to Build Project", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    BuildProject();
                    RunProjectStandalone();
                }
                else if (dr == DialogResult.No)
                {
                    RunProjectStandalone();
                }
                else if (dr == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    return;
                }
            }
        }

        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO
        }

        /// <summary>
        /// Creates a new Beefy Script
        /// </summary>
        private void scriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowScriptEditor();            
        }

        #region Viewport Mode

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normalToolStripMenuItem.Checked = true;
            renderedToolStripMenuItem.Checked = false;
            collisionsToolStripMenuItem.Checked = false;
            lightmapToolStripMenuItem.Checked = false;
            commentaryToolStripMenuItem.Checked = false;
            MainViewport.editorView = GameViewport.EditorView.Normal;
        }

        private void renderedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normalToolStripMenuItem.Checked = false;
            renderedToolStripMenuItem.Checked = true;
            collisionsToolStripMenuItem.Checked = false;
            lightmapToolStripMenuItem.Checked = false;
            commentaryToolStripMenuItem.Checked = false;
            MainViewport.editorView = GameViewport.EditorView.Rendered;
        }

        private void collisionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normalToolStripMenuItem.Checked = false;
            renderedToolStripMenuItem.Checked = false;
            collisionsToolStripMenuItem.Checked = true;
            lightmapToolStripMenuItem.Checked = false;
            commentaryToolStripMenuItem.Checked = false;
            MainViewport.editorView = GameViewport.EditorView.Wireframe;
        }

        private void lightmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normalToolStripMenuItem.Checked = false;
            renderedToolStripMenuItem.Checked = false;
            collisionsToolStripMenuItem.Checked = false;
            lightmapToolStripMenuItem.Checked = true;
            commentaryToolStripMenuItem.Checked = false;
            MainViewport.editorView = GameViewport.EditorView.Lightmap;
        }

        private void commentaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normalToolStripMenuItem.Checked = false;
            renderedToolStripMenuItem.Checked = false;
            collisionsToolStripMenuItem.Checked = false;
            lightmapToolStripMenuItem.Checked = false;
            commentaryToolStripMenuItem.Checked = true;
            MainViewport.editorView = GameViewport.EditorView.Commentary;            
        }

        #endregion

        public bool scriptEditorShown { get; set; }

        private void scriptEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowScriptEditor();
        }

        private void viewLogicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowScriptEditor();
            foreach (BeefyObject bo in MainViewport.ReturnSelected())
            {
                scriptEditor.LoadScript(bo.Script.Name);
            }
        }

        public void ShowScriptEditor()
        {
            if (scriptEditorShown == false)
            {
                scriptEditor = new ScriptEditorForm(this);
                scriptEditor.Show();
                scriptEditorShown = true;
            }
        }
        
        private void cutEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(MainViewport.ReturnSelected());
            MainViewport.ReturnSelected().Clear();
        }

        private void copyEditToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pasteEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject() is List<BeefyObject>)
                MainViewport.AddBeefyObjects((List<BeefyObject>)Clipboard.GetDataObject());
        }

        private void deleteEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void bringToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sendToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lockSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewport.Lock(MainViewport.ReturnSelected());
        }

        private void unlockSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewport.Unlock(MainViewport.ReturnSelected());
        }

        private void toolStripButton_RunLevel_Click(object sender, EventArgs e)
        {
            toolStripButton_RunLevel.Enabled = false;
            toolStripButton_PauseLevel.Enabled = true;
            toolStripButton_StopLevel.Enabled = true;
            CurrentProject.ProjectState = GameState.Running;
        }

        private void toolStripButton_PauseLevel_Click(object sender, EventArgs e)
        {
            toolStripButton_RunLevel.Enabled = true;
            toolStripButton_PauseLevel.Enabled = false;            
            toolStripButton_StopLevel.Enabled = true;
            CurrentProject.ProjectState = GameState.Paused;
        }

        private void toolStripButton_StopLevel_Click(object sender, EventArgs e)
        {
            toolStripButton_RunLevel.Enabled = true;
            toolStripButton_PauseLevel.Enabled = false;
            toolStripButton_StopLevel.Enabled = false;
            CurrentProject.ProjectState = GameState.Aborted;
        }

        private void toolStripComboBox_Environment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }    
}
