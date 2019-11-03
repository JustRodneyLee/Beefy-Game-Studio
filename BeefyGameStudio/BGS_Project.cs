using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using BeefyEngine;

namespace BeefyGameStudio
{
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

        public BeefyAssetLibrary assetLib;
        public string currentLevelPath;

        public string ExtractNameFromPath(string path)
        {
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
                if (assetLib.Exists(aName))
                {
                    imported.Add(aName);
                    continue;
                }
                else
                    try
                    {
                        using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            switch (type)
                            {
                                case BeefyAssetType.Visual:
                                    BeefySprite bs = new BeefySprite() { AssetName = aName, AssetPath = path, SpriteData = Texture2D.FromStream(MainViewport.Editor.graphics, stream) };
                                    assetLib.AddAsset(bs);
                                    AssetLibrary.LargeImageList.Images.Add(bs.AssetName, FitImage(Image.FromStream(stream)));
                                    AssetLibrary.Items.Add(bs.AssetName, bs.AssetName, bs.AssetName);
                                    break;
                                case BeefyAssetType.Auditory:
                                    assetLib.AddAsset(new BeefySound() { AssetName = aName, AssetPath = path, AudioSource = SoundEffect.FromStream(stream) });
                                    //TODO
                                    break;
                                case BeefyAssetType.Object:
                                    assetLib.AddAsset(new BeefySprite() { AssetName = aName, AssetPath = path, SpriteData = Texture2D.FromStream(MainViewport.GraphicsDevice, stream) });
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

        public bool SaveLevel(string path)
        {
            if (MainViewport.SaveLevel(path))
            {
                lvlSaved = true;
                lvlModified = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
