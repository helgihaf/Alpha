using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marson.Compare.WinForms
{
    public static class TreeViewExtensions
    {
        public static TreeNode GetNodeByPath(this TreeView treeView, string nodePath)
        {
            string[] keys = nodePath.Split('\\');
            var collection = treeView.Nodes;
            TreeNode node = null;
            for (int i = 0; i < keys.Length; i++)
            {
                node = collection[keys[i]];
                if (node == null)
                {
                    break;
                }
                collection = node.Nodes;
            }

            return node;
        }

        public static string GetFullNamePath(this TreeNode treeNode)
        {
            var list = new List<string>();
            while (treeNode != null)
            {
                list.Add(treeNode.Name);
                treeNode = treeNode.Parent;
            }

            var sb = new StringBuilder();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (sb.Length > 0)
                    sb.Append("\\");
                sb.Append(list[i]);
            }

            return sb.ToString();
        }

    }
}
