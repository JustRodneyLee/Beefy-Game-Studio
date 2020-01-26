using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using BeefyEngine;

namespace BeefyEngine
{
    public enum BeefyAssetType
    {
        Visual,
        Auditory,
        Object,
    }

    public interface IBeefyAsset
    {
        string AssetName { get; set; }
        string AssetPath { get; set; }
        BeefyAssetType AssetType { get; }
    }

    public class BeefyAssetLibrary
    {
        public string LibraryName { get; set; }
        public Dictionary<string, IBeefyAsset> Assets { get; set; }

        public BeefyAssetLibrary(string name)
        {
            LibraryName = name;
            Assets = new Dictionary<string, IBeefyAsset>();
        }

        public void AddAsset(IBeefyAsset asset)
        {
            Assets.Add(asset.AssetName, asset);
        }

        public void Reset()
        {
            Assets.Clear();
        }

        public IBeefyAsset GetAssetByID(string id)
        {
            return Assets[id];
        }

        public List<IBeefyAsset> GetAssetsByIDs(List<string> ids)
        {
            List<IBeefyAsset> ibaList = new List<IBeefyAsset>();
            foreach (string id in ids)
            {
                ibaList.Add(Assets[id]);
            }
            return ibaList;
        }

        public void RemoveAsset(string id)
        {
            Assets.Remove(id);
        }

        public bool Exists(string id)
        {
            if (Assets.ContainsKey(id))
            {
                return true;
            }
            return false;
        }
    }

    public class BeefySprite : IBeefyAsset
    {
        public string AssetName { get; set; }
        public string AssetPath { get; set; }
        public BeefyAssetType AssetType { get { return BeefyAssetType.Visual; } }
        public Texture2D SpriteData { get; set; }
        public float ImportScale { get; set; }
    }

    public class BeefySound : IBeefyAsset
    {
        public string AssetName { get; set; }
        public string AssetPath { get; set; }
        public BeefyAssetType AssetType { get { return BeefyAssetType.Auditory; } }
        public SoundEffect AudioSource { get; set; }
    }
}
