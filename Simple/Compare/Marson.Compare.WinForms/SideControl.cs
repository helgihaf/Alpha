using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Marson.Compare.Core;

namespace Marson.Compare.WinForms
{
    public partial class SideControl : UserControl
    {
        private bool eventsEnabled = true;


        public SideControl()
        {
            InitializeComponent();
        }

        public string DirPath
        {
            get
            {
                return textBoxDirectory.Text;
            }
            set
            {
                textBoxDirectory.Text = value;
            }
        }

        public void Link(SideControl other)
        {
            if (object.ReferenceEquals(this, other))
            {
                throw new ArgumentException("Cannot link control to itself");
            }
            treeView.AddLinkedTreeView(other.treeView);
        }

        public event EventHandler<EntryEventArgs> ExpandingNode;
        public event EventHandler<EntryEventArgs> CollapsingNode;
        public event EventHandler<EntryEventArgs> SelectedNode;



        public void ClearItems()
        {
            treeView.Nodes.Clear();
        }

        public TreeNode AddItem(TreeNode parent, Entry entry, Item item, string color)
        {
            var node = new TreeNode();
            node.Tag = entry;
            node.Name = entry.Name;
            string text = string.Empty;
            if (item != null)
            {
                text = item.Name;
                string imageKeyBase = entry is DirEntry ? "Folder" : "Diamond";
                node.ImageKey = imageKeyBase + color + ".png";
                if (entry is FileEntry && color != null)
                {
                    node.ForeColor = Color.FromName(color);
                }
            }
            else
            {
                node.ImageKey = "Blank.png";
            }
            node.Text = text;
            node.SelectedImageKey = node.ImageKey;
            TreeNodeCollection collection = parent != null ? parent.Nodes : treeView.Nodes;
            collection.Add(node);

            return node;
        }

        private void toolStrip1_Resize(object sender, EventArgs e)
        {
            textBoxDirectory.Width = toolStrip.ClientRectangle.Width - buttonBrowse.Width * 2;
        }



        //public void LoadData(Entry entry, Func<Entry, Item> itemSelector)
        //{
        //    treeView.Nodes.Clear();
        //    treeView.BeginUpdate();
        //    LoadModel(entry, itemSelector, treeView.Nodes);
        //    treeView.EndUpdate();
        //}

        //private void LoadModel(Entry entry, Func<Entry, Item> itemSelector, TreeNodeCollection collection)
        //{
        //    foreach (var childEntry in entry.ChildEntries)
        //    {
        //        var node = new TreeNode();
        //        node.Tag = childEntry;
        //        node.Name = childEntry.Name;
        //        var item = itemSelector(childEntry);
        //        string text = string.Empty;
        //        if (item != null)
        //        {
        //            text = item.Name;
        //            if (item is DirItem)
        //            {
        //                node.ImageKey = "FolderBlack.png";
        //            }
        //            else if (item is FileItem)
        //            {
        //                node.ImageKey = "DiamondBlack.png";
        //            }
        //        }
        //        else
        //        {
        //            node.ImageKey = "Blank.png";
        //        }
        //        node.Text = text;
        //        node.SelectedImageKey = node.ImageKey;
        //        LoadModel(childEntry, itemSelector, node.Nodes);
        //        collection.Add(node);
        //    }
        //}

        private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (eventsEnabled)
            {
                OnExpandingNode(e.Node.GetFullNamePath(), (Entry)e.Node.Tag);
            }
        }

        private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (eventsEnabled)
            {
                OnCollapsingNode(e.Node.GetFullNamePath(), (Entry)e.Node.Tag);
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (eventsEnabled)
            {
                OnSelectedNode(e.Node.GetFullNamePath(), (Entry)e.Node.Tag);
            }
        }

        public void Expand(string nodePath)
        {
            eventsEnabled = false;
            var node = treeView.GetNodeByPath(nodePath);
            node.Expand();
            eventsEnabled = true;
        }

        public void Collapse(string nodePath)
        {
            var node = treeView.GetNodeByPath(nodePath);
            node.Collapse();
        }

        public void SelectNode(string nodePath)
        {
            var node = treeView.GetNodeByPath(nodePath);
            treeView.SelectedNode = node;
        }


        private void OnExpandingNode(string nodePath, Entry entry)
        {
            if (ExpandingNode != null)
            {
                var e = new EntryEventArgs { NodePath = nodePath, Entry = entry };
                ExpandingNode(this, e);
            }
        }

        private void OnCollapsingNode(string nodePath, Entry entry)
        {
            if (CollapsingNode != null)
            {
                var e = new EntryEventArgs { NodePath = nodePath, Entry = entry };
                CollapsingNode(this, e);
            }
        }

        private void OnSelectedNode(string nodePath, Entry entry)
        {
            if (SelectedNode != null)
            {
                var e = new EntryEventArgs { NodePath = nodePath, Entry = entry };
                SelectedNode(this, e);
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = textBoxDirectory.Text;
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxDirectory.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
