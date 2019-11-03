using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace BeefyEngine
{  
    public class BeefyAssetManager
    {
        List<IBeefyAsset> Assets;        
        ContentManager content;

        public BeefyEngineCore Core { get; }        

        public BeefyAssetManager(BeefyEngineCore core)
        {
            Core = core;
        }

        public T LoadAsset<T>(string assetPath, BeefyLevel beefyLevel)
        {            
            return content.Load<T>(assetPath);
        }

        public void Dispose()
        {
            content.Unload();
            content.Dispose();   
        }
    }
}
