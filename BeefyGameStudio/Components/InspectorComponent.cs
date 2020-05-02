using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeefyGameEngine;

namespace BeefyGameStudio.Components
{
    interface InspectorComponent
    {
        void UpdateParameters();
        void TransferParameters();
    }

    public class UnmanagedComponent : Control, InspectorComponent
    {
        public UnmanagedComponent()
        {
            Name = "UnmanagedComponent";
        }

        public void UpdateParameters() { }

        public void TransferParameters() { }
    }
}
