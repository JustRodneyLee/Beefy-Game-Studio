using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeefyGameEngine;

namespace BeefyGameStudio.Components
{
    public partial class AudioComponent : UserControl
    {
        BeefyAudio audio;

        public AudioComponent(BeefyAudio cmp)
        {
            Name = "AudioComponent";
            audio = cmp;
            InitializeComponent();
        }
    }
}
