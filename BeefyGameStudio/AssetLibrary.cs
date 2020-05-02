using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeefyGameEngine;

namespace BeefyGameStudio
{
    public partial class AssetLibraryForm : Form
    {
        ListView libView;
        BeefyAssetLibrary lib;
        GameViewport viewport;
        bool direct;

        public AssetLibraryForm(BeefyAssetLibrary bal, ListView view, GameViewport vp, bool directAdd)
        {
            InitializeComponent();
            lib = bal;
            libView = view;
            viewport = vp;
            direct = directAdd; 
            assetListView.LargeImageList = new ImageList();
        }

        private void AssetLibraryForm_Load(object sender, EventArgs e)
        {
            assetListView.LargeImageList = libView.LargeImageList;
            foreach (ListViewItem lvi in libView.Items)
            {
                assetListView.Items.Add((ListViewItem)lvi.Clone());
            }
        }

        private void AddSelected()
        {
            List<IBeefyAsset> selected = new List<IBeefyAsset>();
            foreach (ListViewItem lvi in assetListView.SelectedItems)
            {
                selected.Add(lib.GetAssetByID(lvi.Text));
            }
            if (direct)
                viewport.AddBeefyObjects(selected, viewport.MousePos);
            else
                viewport.AddNewObjects(selected);
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            AddSelected();
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AssetListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (assetListView.SelectedItems != null)
            {
                ConfirmButton.Enabled = true;
            }
            else
            {
                ConfirmButton.Enabled = false;
            }
        }

        private void AssetListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (assetListView.SelectedItems != null)
            {
                AddSelected();
                Close();
            }
        }

        private void AssetLibraryForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
