using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BeefyEngine
{
    public class BeefyAssetManager
    {        
        ContentManager mainContent;
        Dictionary<BeefyLevel, ContentManager> contents; //Extra Content Managers for levels

        public BeefyEngine Core { get; }

        public BeefyAssetManager(BeefyEngine core)
        {
            Core = core;
        }

        public bool LoadAsset<T>(string assetPath, BeefyLevel beefyLevel)
        {
            //beefyLevel. mainContent.Load<T>(assetPath);
            try
            {
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public void Dispose()
        {
            mainContent.Unload();
            mainContent.Dispose();
        }
    }

    public class LevelData
    {
        BeefyLevelSettings settings;
        Dictionary<string, string> layers_objects;
        Dictionary<string, int> layersID_layerNo;

        public LevelData()
        {

        }

        public LevelData(BeefyLevel bl)
        {
            settings = bl.Settings;
            layers_objects = new Dictionary<string, string>();
            foreach(BeefyLayer layer in bl.Layers)
            {
                foreach (BeefyObject bo in layer.BOC)
                {
                    layers_objects.Add(bo.ObjectID, layer.LayerID);
                }
                layersID_layerNo.Add(layer.LayerID, layer.LayerNo);
            }
        }

    }

    public class ObjectData
    {

    }

    public class ScriptData
    {
        
    }
}
