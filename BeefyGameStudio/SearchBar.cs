using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeefyGameStudio
{
    public partial class SearchBar : Form
    {
        GameViewport view;

        public SearchBar(GameViewport viewport)
        {
            InitializeComponent();
            view = viewport;
            //this.LostFocus += new EventHandler(SearchBar_LostFocus);
        }

        private void Init()
        {
            this.Location = view.Editor.GetAbsoluteMousePosition;
            searchBox.ForeColor = Color.Gray;
            searchBox.Text = "Search for commands...";
            TopMost = true;
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            //TODO : Search commands
        }

        private void SearchBox_Click(object sender, EventArgs e)
        {
            searchBox.ForeColor = Color.Black;
            searchBox.Text = "";
        }

        private void SearchBar_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void SearchBox_Leave(object sender, EventArgs e)
        {
            if (searchBox.Text=="")
                Init();
        }

        void SearchBar_LostFocus(object sender, EventArgs e)
        {
           // Hide();
        }

        private void SearchBar_Leave(object sender, EventArgs e)
        {
            //Hide();
        }

        private void SearchBar_Shown(object sender, EventArgs e)
        {
            Init();
            TopMost = false;
        }
    }
}
