using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.Library.Database.Schema;
using Knightrunner.Library.Controls.PageSelection;
using Knightrunner.Library.Database.Schema.Project;

namespace DataSchemaGui
{
    public partial class DataSchemaExplorer : UserControl, ISelector
    {
        private const string ImageKeyDataSchema = "DataSchema";
        private const string ImageKeyTable = "Table";
        private const string ImageKeyIndex = "Index";
        private const string ImageKeyPrimaryKey = "PrimaryKey";
        private const string ImageKeyForeignKey = "ForeignKey";
        private const string ImageKeyFolder = "Folder";
        private const string ImageKeyTargetSystem = "TargetSystem";
        private const string ImageKeyColumnType = "DataType";

        private DataSchemaProject project;
        private bool allowInternalEvents;
        private bool allowExternalEvents;

        public DataSchemaExplorer()
        {
            InitializeComponent();
        }


        public void Initialize(IDirector director)
        {
            this.Director = director;
        }

        public IDirector Director { get; private set; }


        public DataSchemaProject Project
        {
            get
            {
                return project;
            }

            set
            {
                if (!object.ReferenceEquals(project, value))
                {
                    project = value;
                    RefreshExplorer();
                }
            }
        }



        private void RefreshExplorer()
        {
            allowExternalEvents = false;
            allowInternalEvents = false;
            treeView.BeginUpdate();

            BuildTree();   

            treeView.EndUpdate();
            UpdateActions();

            allowInternalEvents = true;
            allowExternalEvents = true;
        }

        private void UpdateActions()
        {
            //throw new NotImplementedException();
        }

        
        private void BuildTree()
        {
            // Data schema
            var dataSchemaNode = new TreeNode();
            dataSchemaNode.Text = project.Name;
            dataSchemaNode.ImageKey = ImageKeyDataSchema;
            dataSchemaNode.SelectedImageKey = dataSchemaNode.ImageKey;
            dataSchemaNode.Tag = project;
            
            // Targets
            var targetsNode = new TreeNode();
            targetsNode.Text = "Target systems";
            targetsNode.ImageKey = ImageKeyFolder;
            targetsNode.SelectedImageKey = targetsNode.ImageKey;
            BuildTargetSystemNodes(targetsNode);
            dataSchemaNode.Nodes.Add(targetsNode);

            // Column types
            var columnTypesNode = new TreeNode();
            columnTypesNode.Text = "Column types";
            columnTypesNode.ImageKey = ImageKeyFolder;
            columnTypesNode.SelectedImageKey = columnTypesNode.ImageKey;
            BuildColumnTypeNodes(columnTypesNode);
            dataSchemaNode.Nodes.Add(columnTypesNode);

            // Tables
            var tablesNode = new TreeNode();
            tablesNode.Text = "Tables";
            tablesNode.ImageKey = ImageKeyFolder;
            tablesNode.SelectedImageKey = tablesNode.ImageKey;
            BuildTableNodes(tablesNode);
            dataSchemaNode.Nodes.Add(tablesNode);
            tablesNode.Expand();

            // Add all to tree
            treeView.Nodes.Clear();
            treeView.Nodes.Add(dataSchemaNode);
            dataSchemaNode.Expand();
        }

        private void BuildTargetSystemNodes(TreeNode parentNode)
        {
            foreach (var targetSystem in project.TargetSystems.OrderBy(ts => ts.Name))
            {
                var node = new TreeNode();
                SetTargetSystemNode(node, targetSystem);
                parentNode.Nodes.Add(node);
            }
        }

        private void SetTargetSystemNode(TreeNode node, TargetSystem targetSystem)
        {
            node.Text = targetSystem.Name;
            node.ImageKey = ImageKeyTargetSystem;
            node.SelectedImageKey = node.ImageKey;
            node.Tag = targetSystem;
        }

        private void BuildColumnTypeNodes(TreeNode parentNode)
        {
            foreach (var columnType in project.ColumnTypes.OrderBy(ct => ct.Name))
            {
                var node = new TreeNode();
                SetColumnTypeNode(node, columnType);
                parentNode.Nodes.Add(node);
            }
        }

        private void SetColumnTypeNode(TreeNode node, ColumnType columnType)
        {
            node.Text = columnType.Name;
            node.ImageKey = ImageKeyColumnType;
            node.SelectedImageKey = node.ImageKey;
            node.Tag = columnType;
        }

        private void BuildTableNodes(TreeNode parentNode)
        {
            foreach (var table in project.Tables.OrderBy(tab => tab.Name))
            {
                var node = new TreeNode();
                SetTableNode(node, table);

                foreach (var index in table.Indices)
                {
                    BuildIndexNodes(table, node);
                }

                parentNode.Nodes.Add(node);
            }
        }

        private void SetTableNode(TreeNode node, Table table)
        {
            node.Text = table.Name;
            node.ImageKey = ImageKeyTable;
            node.SelectedImageKey = node.ImageKey;
            node.Tag = table;
        }


        private void BuildIndexNodes(Table table, TreeNode parentNode)
        {
            foreach (var index in table.Indices.OrderBy(idx => idx.Name))
            {
                var node = new TreeNode();
                SetIndexNode(node, index);
                parentNode.Nodes.Add(node);
            }
        }

        private void SetIndexNode(TreeNode node, Index index)
        {
            node.Text = index.Name;
            node.ImageKey = ImageKeyIndex;
            node.SelectedImageKey = node.ImageKey;
            node.Tag = index;
        }

        private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = !Director.LeaveCurrentPage();
        }


        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Director.ShowObjectPage(e.Node.Tag);
        }




        internal Image GetCurrentImage()
        {
            Image result = null;

            if (treeView.SelectedNode != null)
            {
                var key = treeView.SelectedNode.ImageKey;
                if (key != null)
                {
                    result = imageList.Images[key];
                }
            }

            return result;
        }

    }
}
