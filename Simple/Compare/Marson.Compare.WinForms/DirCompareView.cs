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
    public partial class DirCompareView : UserControl
    {
        public DirCompareView()
        {
            InitializeComponent();
            sideLeft.Link(sideRight);
            sideRight.Link(sideLeft);

            // Debug
            sideLeft.DirPath = @"c:\diskar\likerfi\LISA";
            sideRight.DirPath = @"C:\temp\Drop\e844d636-7f9a-4d23-9dc3-481884cdf1dc\6779cd13-a32d-4e0a-88cd-b7eda6dc7d2d\product";
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            string leftDirPath = sideLeft.DirPath;
            string rightDirPath = sideRight.DirPath;
            if (string.IsNullOrEmpty(leftDirPath) || string.IsNullOrEmpty(rightDirPath))
            {
                return;
            }

            var engine = new Engine();
            var entry = engine.CompareDirectories(leftDirPath, rightDirPath, new CompareOptions());

            sideLeft.ClearItems();
            sideRight.ClearItems();
            LoadEntries(entry, null, null);
        }

        private void LoadEntries(Entry entry, TreeNode leftParentNode, TreeNode rightParentNode)
        { 
            foreach (var childEntry in entry.ChildEntries)
            {
                string color = null;
                var dirEntry = childEntry as DirEntry;
                if (dirEntry != null)
                {
                    if (dirEntry.Left != null)
                    {
                        color = GetFolderColor(dirEntry, CompareStatus.LeftOrphant, CompareStatus.LeftNewer, CompareStatus.RightNewer);
                    }
                    var leftNode = sideLeft.AddItem(leftParentNode, dirEntry, dirEntry.Left, color);

                    if (dirEntry.Right != null)
                    {
                        color = GetFolderColor(dirEntry, CompareStatus.RightOrphant, CompareStatus.RightNewer, CompareStatus.LeftNewer);
                    }
                    var rightNode = sideRight.AddItem(rightParentNode, dirEntry, dirEntry.Right, color);

                    LoadEntries(dirEntry, leftNode, rightNode);
                }
                else
                {
                    var fileEntry = (FileEntry)childEntry;
                    if (fileEntry.Left != null)
                    {
                        color = GetFileColor(fileEntry, CompareStatus.LeftOrphant, CompareStatus.LeftNewer, CompareStatus.RightNewer);
                    }
                    var leftNode = sideLeft.AddItem(leftParentNode, fileEntry, fileEntry.Left, color);

                    if (fileEntry.Right != null)
                    {
                        color = GetFileColor(fileEntry, CompareStatus.RightOrphant, CompareStatus.RightNewer, CompareStatus.LeftNewer);
                    }
                    var rightNode = sideRight.AddItem(rightParentNode, fileEntry, fileEntry.Right, color);
                }
            }
        }

        private string GetFolderColor(DirEntry dirEntry, CompareStatus blueStatus, CompareStatus redStatus, CompareStatus grayStatus)
        {
            var colors = new List<string>();

            if (dirEntry.CompareStatuses.Contains(blueStatus))
            {
                colors.Add("Blue");
            }

            if (dirEntry.CompareStatuses.Contains(redStatus))
            {
                colors.Add("Red");
            }

            if (dirEntry.CompareStatuses.Contains(CompareStatus.NotEqual) ||
                dirEntry.CompareStatuses.Contains(grayStatus) ||
                dirEntry.CompareStatuses.Contains(CompareStatus.Unknown))
            {
                colors.Add("Gray");
            }

            if (colors.Count >= 2)
            {
                return colors[0] + colors[1];
            }
            else if (colors.Count == 1)
            {
                return colors[0];
            }
            else
            {
                return "Black";
            }
        }

        private string GetFileColor(FileEntry fileEntry, CompareStatus blueStatus, CompareStatus redStatus, CompareStatus grayStatus)
        {
            if (fileEntry.CompareStatuses.Contains(blueStatus))
            {
                return "Blue";
            }

            if (fileEntry.CompareStatuses.Contains(redStatus))
            {
                return "Red";
            }

            if (fileEntry.CompareStatuses.Contains(CompareStatus.NotEqual) ||
                fileEntry.CompareStatuses.Contains(grayStatus) ||
                fileEntry.CompareStatuses.Contains(CompareStatus.Unknown))
            {
                return "Gray";
            }

            return "Black";
        }


        private void sideLeft_ExpandingNode(object sender, EntryEventArgs e)
        {
            sideRight.Expand(e.NodePath);
        }

        private void sideLeft_CollapsingNode(object sender, EntryEventArgs e)
        {
            sideRight.Collapse(e.NodePath);
        }

        private void sideRight_ExpandingNode(object sender, EntryEventArgs e)
        {
            sideLeft.Expand(e.NodePath);
        }

        private void sideRight_CollapsingNode(object sender, EntryEventArgs e)
        {
            sideLeft.Collapse(e.NodePath);
        }

        private void sideLeft_SelectedNode(object sender, EntryEventArgs e)
        {
            sideRight.SelectNode(e.NodePath);
        }

        private void sideRight_SelectedNode(object sender, EntryEventArgs e)
        {
            sideLeft.SelectNode(e.NodePath);
        }

        private void sideLeft_Scroll(object sender, ScrollEventArgs e)
        {
            sideRight.VerticalScroll.Value = e.NewValue;
        }
    }
}
