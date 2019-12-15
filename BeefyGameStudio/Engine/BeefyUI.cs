using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeefyEngine
{
    public class BeefyUISystem : IBeefySystem
    {
        public BeefyEngine Core { get; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string Update(BeefyLevel Level)
        {
            return null;
        }
    }

    public class BeefyUI : BeefyObject
    {
        public BeefyShape Control { get; set; }

        public BeefyUI()
        {
            AddComponent(new BeefyPhysics(this));
            Control = GetComponent<BeefyPhysics>().Collider;
        }
    }

    public class BeefyButton : BeefyUI
    {

    }

    public class BeefyTextBox: BeefyUI
    {

    }
}
