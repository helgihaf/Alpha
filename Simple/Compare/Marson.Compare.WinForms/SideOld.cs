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
using Aga.Controls.Tree;

namespace Marson.Compare.WinForms
{
    public partial class SideOld : UserControl
    {
        private TreeModel treeModel;

        public SideOld()
        {
            InitializeComponent();
        }

        internal void LoadData(Entry entry, Func<Entry, Item> itemSelector)
        {
            treeModel = new TreeModel();
            treeView.Model = treeModel;
            LoadModel(entry, itemSelector, treeModel.Root);
        }

        private void LoadModel(Entry entry, Func<Entry, Item> itemSelector, Node parentNode)
        {
            foreach (var childEntry in entry.ChildEntries)
            {
                var item = itemSelector(childEntry);
                string text = string.Empty;
                if (item != null)
                {
                    text = item.Name;
                }
                var node = new Node(text);
                LoadModel(childEntry, itemSelector, node);
                parentNode.Nodes.Add(node);
            }
        }

        private void treeView_Expanded(object sender, TreeViewAdvEventArgs e)
        {

        }
    }
}
