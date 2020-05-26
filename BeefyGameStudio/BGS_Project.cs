using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content.Pipeline;
using MonoGame.RuntimeBuilder;
using BeefyGameEngine;
using Microsoft.Xna.Framework.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace BeefyGameStudio
{
    static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }

    public static class CurrentProject
    {
        public static BeefyProject Data { get; internal set; }
        public static bool IsNull
        {
            get { return Data == null; }
        }

        public static string ProjectName { get { if (Data == null) return "Untitled Game"; else return Data.ProjectName; } set { Data.ProjectName = value; } }
        public static string ProjectExe { get { return Data.ProjectName; } }
        public static List<string> ProjectDevelopers { get { return Data.ProjectDevelopers; } set { Data.ProjectDevelopers = value; } }
        public static Version ProjectVersion { get { return Data.ProjectVersion; } set { Data.ProjectVersion = value; } }
        public static string ProjectLogoPath { get { return Data.ProjectLogoPath; } set { Data.ProjectLogoPath = value; } }
        public static string ProjectPath { get { return Data.ProjectPath; } set { Data.ProjectPath = value; } }
        public static string RawPath { get { return Data.RawPath; } }
        public static string LevelsPath { get { return Data.LevelsPath; } }
        public static string AssetsPath { get { return Data.AssetsPath; } }
        public static string TempPath { get { return Data.TempPath; } }
        public static string ObjPath { get { return Data.ObjPath; } }
        public static string BuildPath { get { return Data.BuildPath; } }
        public static string EnginePath { get { return Data.EnginePath; } }
        public static string CurrentLevelPath { get { return Data.CurrentLevelPath; } }
        ///Editing Info
        public static string CurrentLevelID { get { return Data.CurrentLevelID; } set { Data.CurrentLevelID = value; } }
        ///Game Settings
        public static bool PartialLoading { get { return Data.PartialLoading; } set { Data.PartialLoading = value; } }
        public static bool DeveloperMode { get { return Data.DeveloperMode; } set { Data.DeveloperMode = value; } }
        public static bool RunFullscreen { get { return Data.RunFullScreen; } set { Data.RunFullScreen = value; } }
        public static bool RunBorderless { get { return Data.RunBorderless; } set { Data.RunBorderless = value; } }
        public static bool ShowMouseCursor { get { return Data.ShowMouseCursor; } set { Data.ShowMouseCursor = value; } }
        public static List<string> LevelIDs { get { return Data.LevelIDs; } set { Data.LevelIDs = value; } }
        public static string StartUpLevelID { get { return Data.StartUpLevelID; } set { Data.StartUpLevelID = value; } }
        public static bool ProjectBuilt { get; set; }
        public static bool AssetsBuilt { get; set; }
        public static bool Saved { get; set; }

        public static GameState ProjectState { get; set; }

        public static void SetProjectData(BeefyProject project)
        {
            Data = project;
        }

        public static void Reset()
        {
            Data = null;
        }
    }

    public class BeefyProject : ICloneable, IDisposable
    {
        /// Basic Info             
        public string ProjectName { get; set; }
        public string ProjectExe { get { return ProjectName + ".exe"; } }
        public List<string> ProjectDevelopers { get; set; }
        public Version ProjectVersion { get; set; }
        public string ProjectLogoPath { get; set; }
        public string ProjectPath { get; set; }
        public string RawPath { get { return ProjectPath + "\\Raw"; } }
        public string LevelsPath { get { return RawPath + "\\Levels"; } }
        public string AssetsPath { get { return RawPath + "\\Assets"; } }
        public string TempPath { get { return ProjectPath + "\\Temp"; } }
        public string ObjPath { get { return ProjectPath + "\\Obj"; } }
        public string BuildPath { get { return ProjectPath + "\\Build"; } }
        public string EnginePath { get { return ProjectPath + "\\Engine"; } }
        ///Editing Info
        public string CurrentLevelID { get; set; }
        public string CurrentLevelPath { get { return LevelsPath + "\\" + CurrentLevelID; } }
        ///Game Settings
        public bool PartialLoading { get; set; }
        public bool DeveloperMode { get; set; }
        public bool RunFullScreen { get; set; }
        public bool RunBorderless { get; set; }
        public bool ShowMouseCursor { get; set; }
        public Point Resolution { get; set; }
        public List<string> LevelIDs { get; set; }
        public string StartUpLevelID { get; set; }

        public BeefyProject()
        {
            ProjectDevelopers = new List<string>();
            LevelIDs = new List<string>();
            ProjectLogoPath = "";
            ProjectVersion = new Version(1, 0, 0, 0);
        }

        public object Clone()
        {
            BeefyProject project = new BeefyProject();
            project.ProjectName = ProjectName;
            project.ProjectVersion = ProjectVersion;
            project.ProjectDevelopers = (List<string>)ProjectDevelopers.Clone();
            project.ProjectLogoPath = ProjectLogoPath;
            project.PartialLoading = PartialLoading;
            project.DeveloperMode = DeveloperMode;
            project.CurrentLevelID = CurrentLevelID;
            project.LevelIDs = (List<string>)LevelIDs.Clone();
            return project;            
        }

        public void Dispose()
        {
            ProjectDevelopers.Clear();
        }
    }

    public partial class BGS : Form
    {
        public enum FileType
        {
            Project,
            Level,
            Object,
            Asset,
        }

        public enum ImportAction
        {
            ImportOnly,
            AddToScene,
            WaitToAdd,
        }

        public BeefyAssetLibrary AssetLib { get; set; }
        
        public string ExtractNameFromPath(string path, bool keepExtension = false)
        {
            if (keepExtension)
                return path.Split('\\').Last();
            else
                return path.Split('\\').Last().Split('.').First();
        }

        public List<string> ExtractNamesFromPaths(string[] paths)
        {
            List<string> names = new List<string>();
            foreach(string path in paths)
            {
                names.Add(ExtractNameFromPath(path));
            }
            return names;
        }   

        private Bitmap FitImage(Image image)
        {
            Bitmap img = new Bitmap(72, 72);
            float imgWidth = (float)image.Width;
            float imgHeight = (float)image.Height;
            float ratio = imgWidth / imgHeight;
            int dW, dH, tX, tY; //Destined width, height; Target X, Y;
            //TODO : Improvement on small images
            if (imgWidth <= 72 && imgHeight <= 72)
            {
                dW = (int)imgWidth;
                dH = (int)imgHeight;
                tX = (72 - dW) / 2;
                tY = (72 - dH) / 2;
            }
            if (imgWidth > imgHeight)
            {
                dW = 72;
                dH = (int)(dW / ratio);
                tX = 0;
                tY = (72 - dH) / 2;
            }
            else
            {
                dH = 72;
                dW = (int)(dH * ratio);
                tX = (72 - dW) / 2;
                tY = 0;                
            }
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Transparent);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, new Rectangle(tX, tY, dW, dH), new Rectangle(0, 0, (int)imgWidth, (int)imgHeight), GraphicsUnit.Pixel);
            return img;
        }

        public bool ImportAssets(string[] paths, BeefyAssetType type)
        {
            List<string> imported = new List<string>();
            foreach (string path in paths)
            {
                string aName = ExtractNameFromPath(path);
                if (AssetLib.Exists(aName))
                {
                    imported.Add(aName);
                    continue;
                }
                else
                    try
                    {
                        string aPath =  CurrentProject.AssetsPath + "\\" + aName;
                        using (FileStream stream = File.Open(path, FileMode.Open))
                        {   
                            switch (type)
                            {
                                case BeefyAssetType.Visual:
                                    BeefySprite bs = new BeefySprite() { AssetName = aName, AssetPath = aPath, SpriteData = Texture2D.FromStream(MainViewport.Editor.graphics, stream), ImportScale = EditorSettings.PixelScale };
                                    AssetLib.AddAsset(bs);
                                    AssetLibrary.LargeImageList.Images.Add(bs.AssetName, FitImage(Image.FromStream(stream)));
                                    AssetLibrary.Items.Add(bs.AssetName, bs.AssetName, bs.AssetName);
                                    break;
                                case BeefyAssetType.Auditory:
                                    AssetLib.AddAsset(new BeefySound() { AssetName = aName, AssetPath = aPath, AudioSource = SoundEffect.FromStream(stream) });
                                    //TODO
                                    break;
                                case BeefyAssetType.Object:
                                    AssetLib.AddAsset(new BeefySprite() { AssetName = aName, AssetPath = aPath, SpriteData = Texture2D.FromStream(MainViewport.GraphicsDevice, stream) });
                                    //TODO
                                    break;
                            }
                        }                        
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Invalid Operation! Error:" + e.ToString(), "Beefy Game Studio - Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }                            
            }
            if (imported.Count() != 0)
            {
                //TODO : Same name different data causes conflict; Could use data tags instead
                foreach (string name in imported)
                    MessageBox.Show("The Asset " + name + " is already Imported!", "Beefy Game Studio - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }

        public void NewProject(string name, string path)
        {
            //TODO
            try
            {
                BeefyProject project = new BeefyProject();
                project.ProjectName = name;
                project.ProjectPath = path;                
                project.ProjectDevelopers.Add(Environment.UserName);                
                Directory.CreateDirectory(project.ProjectPath);
                using (StreamWriter writer = new StreamWriter(project.ProjectPath + "\\" + project.ProjectName + ".bgp"))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(BeefyProject));
                    serializer.Serialize(writer, project);
                }
                Directory.CreateDirectory(project.BuildPath);                
                Directory.CreateDirectory(project.ObjPath);
                Directory.CreateDirectory(project.RawPath);
                Directory.CreateDirectory(project.AssetsPath);
                Directory.CreateDirectory(project.LevelsPath);
                Directory.CreateDirectory(project.EnginePath);
                Directory.SetCurrentDirectory(project.ProjectPath);
                CurrentProject.SetProjectData(project);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "Beefy Game Studio - Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                RefreshTitleText();
            }
        }

        public void ExportAssetLib(string path)
        {

        }

        public void ImportAssetLib(string path)
        {

        }

        public void SaveProject()
        {
            //TODO
            ExportAssetLib(CurrentProject.RawPath);
            using (StreamWriter writer = new StreamWriter(CurrentProject.ProjectPath + "\\" + CurrentProject.ProjectName + ".bgp"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BeefyProject));
                serializer.Serialize(writer, CurrentProject.Data);
            }
            CurrentProject.Saved = true;
        }

        public string OpenProject(string path)
        {
            string returnMsg;
            int loadStatus;

            string ReadProject()
            {
                XmlSerializer xml;
                try
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        xml = new XmlSerializer(typeof(BeefyProject));
                        CurrentProject.SetProjectData((BeefyProject)xml.Deserialize(reader));
                    }
                }
                catch
                {
                    return "Cannot read project file!"; //.bgp file corrupt
                }
                Console.WriteLine(CurrentProject.CurrentLevelID);
                if (CurrentProject.CurrentLevelID is null)
                {
                    MainViewport.InternalLoad(new BeefyLevel("New Level"));
                    return "OK";
                }                    
                else
                {
                    int ret = MainViewport.LoadLevel(CurrentProject.LevelsPath + "\\" + CurrentProject.CurrentLevelID + "\\" + CurrentProject.CurrentLevelID + ".bgl");
                    switch (ret)
                    {
                        case 0:
                            return "OK";
                        case 1:
                            return "Cannot read level file!";
                        case -1:
                            return "Default level is null!";
                        default:
                            return "Unhandled";
                    }
                }
            }

            if (CurrentProject.Saved||CurrentProject.Data == null)
            {
                returnMsg = ReadProject();                           
            }
            else
            {
                DialogResult dr = MessageBox.Show("You haven't saved your current Project! Do you wish to save it now?", "Beefy Game Studio - Save Project", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    SaveProject();
                    returnMsg = ReadProject();
                }
                else if (dr == DialogResult.No)
                {
                    returnMsg = ReadProject();
                }
                else if (dr == DialogResult.Cancel)
                {
                    //Do nothing  
                    returnMsg = "Canceled";
                }
                else
                {
                    //Do nothing
                    returnMsg = "Unhandled";
                }
            }
            if (returnMsg.Contains("OK"))
            {
                RefreshTitleText();
            }
            return returnMsg;
        }

        public async void BuildAssets()
        {
            //TODO
            if (!CurrentProject.AssetsBuilt)
            {
                RuntimeBuilder builder = new RuntimeBuilder(CurrentProject.TempPath, CurrentProject.ObjPath, CurrentProject.BuildPath, TargetPlatform.Windows, GraphicsProfile.HiDef, true) { Logger = new StringBuilderLogger() };
                foreach (IBeefyAsset iba in AssetLib.Assets.Values)
                {
                    await builder.BuildContent(iba.AssetPath);
                }
                Directory.Delete(CurrentProject.TempPath, true);//Delete Temp Files
                MessageBox.Show("Asset build successful.");
            }
            else
            {
                MessageBox.Show("Your assets have already been built!");
            }
        }

        public bool BuildProject()
        {
            BuildAssets();
            StatusStrip.Text = "Building " + CurrentProject.ProjectName + "...";
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
            string output = CurrentProject.BuildPath + "\\" + CurrentProject.ProjectName + ".exe";
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = output;
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("MonoGame.Framework.dll");
            //Create CSharp files for compilation
            /*foreach (string bl in CurrentProject.Levels)
            {
                using (StreamWriter writer = new StreamWriter(CurrentProject.BuildPath))
                {

                }
            }*/
            CompilerResults results = codeProvider.CompileAssemblyFromFile(parameters, "//TODO");
            if (results.Errors.HasErrors)
            {
                StatusStrip.Text = "Build failed.";
                //TODO : Pop up errors
                List<CompilerError> errors = new List<CompilerError>();
                List<CompilerError> warnings = new List<CompilerError>();
                foreach (CompilerError error in results.Errors)
                {
                    if (!error.IsWarning)
                        errors.Add(error);
                    else
                        warnings.Add(error);
                }
                errors.AddRange(warnings);
                OutputConsole console = new OutputConsole(errors);
                warnings.Clear();
                errors.Clear(); //Attempt to dispose
                return false;
            }
            else
            {
                StatusStrip.Text = "Build Successful!";
                return true;
            }            
        }

        public bool SaveLevel(string path)
        {
            bool success = true;
            try
            {
                string lvlPath = path.Split('.').First();
                Directory.CreateDirectory(lvlPath);
                LevelData data = new LevelData(MainViewport.Level);
                //Write level settings using XML serialiation
                XmlSerializer serializer;
                using (StreamWriter file = new StreamWriter(lvlPath + "\\" + MainViewport.Level.LevelID + ".bgl"))
                {
                    serializer = new XmlSerializer(typeof(LevelData));
                    serializer.Serialize(file, data);
                }
                foreach (BeefyLayer bl in MainViewport.Level.Layers)
                {
                    string layerPath = lvlPath + "\\" + bl.LayerID;
                    Directory.CreateDirectory(layerPath);
                    foreach (BeefyObject bo in bl.BOC)
                    {
                        using (StreamWriter file = new StreamWriter(layerPath + "\\" + bo.ObjectID + ".bgo"))
                        {
                            foreach (IBeefyComponent ibc in bo.Components)
                            {
                                switch (ibc.ComponentID)
                                {
                                    case "Transform":
                                        file.WriteLine();
                                        break;
                                    case "Physics":
                                        break;
                                    case "Renderer2D":
                                        break;
                                    case "InputController":
                                        break;
                                    case "Audio":
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                success = false;
            }
            finally
            {
                if (success)
                {                    
                    lvlModified = false;
                }
            }
            return success;
        }
    }
}
