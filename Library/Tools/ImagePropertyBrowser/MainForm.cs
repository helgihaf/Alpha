using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Knightrunner.Library.Images;

namespace ImagePropertyBrowser
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshFiles();
        }

        private void RefreshFiles()
        {
            string root = textBoxDir.Text;
            if (string.IsNullOrEmpty(root) || !Directory.Exists(root))
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            listViewFiles.BeginUpdate();
            listViewFiles.Items.Clear();

            foreach (var dir in Directory.GetDirectories(root).OrderBy(f => f))
            {
                var item = new ListViewItem();
                SetListViewItemDir(item, dir);
                listViewFiles.Items.Add(item);
            }

            foreach (var file in Directory.GetFiles(root).OrderBy(f => f))
            {
                var item = new ListViewItem();
                SetListViewItemFile(item, file);
                listViewFiles.Items.Add(item);
            }

            listViewFiles.EndUpdate();
        }

        private void SetListViewItemDir(ListViewItem item, string dir)
        {
            item.SubItems.Clear();
            item.Text = "[" + Path.GetFileName(dir) + "]";
            item.Tag = dir;
        }

        private void SetListViewItemFile(ListViewItem item, string file)
        {
            item.SubItems.Clear();
            item.Text = Path.GetFileName(file);
            item.Tag = file;
        }

        private void ShowProperties(string filePath)
        {
            ImageInfo info = new ImageInfo(filePath);
            propertyGrid.SelectedObject = info;
        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewFiles.SelectedItems.Count == 1)
            {
                ShowProperties(listViewFiles.SelectedItems[0].Tag as string);
            }

        }
    }
}
