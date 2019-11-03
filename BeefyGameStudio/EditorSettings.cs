using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeefyGameStudio
{
    /// <summary>
    /// Beefy Game Studio Settings
    /// </summary>
    public static class EditorSettings
    {
        public static bool ShowFPSInViewPort { get; set; }
        public static bool ShowScaleInViewPort { get; set; }
        public static int GridSize { get; set; }
        public static int SelectionBorderWidth { get; set; }
        public static Color SelectionBorderColor { get; set; }
        public static float PanSpeed { get; set; }
        public static Color YAxisColor { get; set; }
        public static Color XAxisColor { get; set; }
        public static Color OriginColor { get; set; }
        public static string Version { get; set; }

        public static void Init()
        {
            GridSize = 100; //px
            PanSpeed = 1;
            ShowScaleInViewPort = true;
            ShowFPSInViewPort = true;
            SelectionBorderWidth = 2;
            SelectionBorderColor = Color.Orange;
            YAxisColor = Color.Green;
            XAxisColor = Color.Red;
            OriginColor = Color.Yellow;
            Version = System.Windows.Forms.Application.ProductVersion.Split('.')[0] + "." + System.Windows.Forms.Application.ProductVersion.Split('.')[1];
            ReadSettings();
        }

        public static void ReadSettings()
        {
            /*try
            {
                ShowFPSInViewPort = (bool)Properties.Settings.Default["ShowFPSInViewPort"];
                ShowScaleInViewPort = (bool)Properties.Settings.Default["ShowScaleInViewPort"];
                GridSize = (int)Properties.Settings.Default["GridSize"];
                SelectionBorderWidth = (int)Properties.Settings.Default["SelectionBorderWidth"];
                SelectionBorderColor = (Color)Properties.Settings.Default["SelectionBorderColor"];
                PanSpeed = (float)Properties.Settings.Default["PanSpeed"];
                YAxisColor = (Color)Properties.Settings.Default["YAxisColor"];
                XAxisColor = (Color)Properties.Settings.Default["XAxisColor"];
                OriginColor = (Color)Properties.Settings.Default["OriginColor"];
            }
            catch
            {
                SaveSettings();
            }*/
        }

        public static void SaveSettings()
        {
            /*Properties.Settings.Default["ShowFPSInViewPort"] = ShowFPSInViewPort;
            Properties.Settings.Default["ShowScaleInViewPort"] = ShowScaleInViewPort;
            Properties.Settings.Default["GridSize"] = GridSize;
            Properties.Settings.Default["SelectionBorderWidth"] = SelectionBorderWidth;
            Properties.Settings.Default["SelectionBorderColor"] = SelectionBorderColor;
            Properties.Settings.Default["PanSpeed"] = PanSpeed;
            Properties.Settings.Default["YAxisColor"] = YAxisColor;
            Properties.Settings.Default["XAxisColor"] = XAxisColor;
            Properties.Settings.Default["OriginColor"] = OriginColor;            
            Properties.Settings.Default.Save();*/
        }
    }
}
