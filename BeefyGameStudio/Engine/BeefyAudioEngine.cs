using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace BeefyEngine
{
    /// <summary>
    /// Audio or Music playing
    /// </summary>
    public class BeefyAudio : IBeefyComponent
    {
        public string ComponentID { get { return "Audio"; } }
        public bool Enabled { get; private set; }
        public BeefyObject Entity { get; set; }
        public List<SoundEffect> SoundEffects { get; set; }
        public Queue<SoundEffectInstance> QueuedSounds { get; }

        public BeefyAudio(BeefyObject parent)
        {
            Entity = parent;
            SoundEffects = new List<SoundEffect>();
        }

        public void QueueSound(SoundEffect sE)
        {
            QueuedSounds.Enqueue(sE.CreateInstance());
        }

        public void AddSoundEffect()
        {

        }

        public void RemoveSoundEffect()
        {

        }

        public void Enable()
        {
            Enabled = true;
        }

        public void Disable()
        {
            Enabled = false;
        }

        public object Clone()
        {
            BeefyAudio ba = new BeefyAudio(Entity);
            return ba;
        }
    }

    public class BeefyAudioEngine : IBeefySystem
    {
        public BeefyEngineCore Core { get; }

        public BeefyAudioEngine(BeefyEngineCore core)
        {
            Core = core;
        }

        public void Dispose()
        {
            
        }

        public string Update(BeefyLevel Level)
        {
            foreach(BeefyObject bo in Level.AudioBO)
            {
                //bo.GetComponent<BeefyAudio>().QueuedSounds;
            }
            return null;
        }
    }
}
