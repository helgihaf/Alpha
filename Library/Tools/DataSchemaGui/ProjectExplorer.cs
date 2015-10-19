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
using System.IO;

namespace DataSchemaGui
{
    public partial class ProjectExplorer : UserControl, ISelector
    {
        private static class ImageKey
        {
            public const string Project = "Project";
            public const string InputFile = "InputFile";
            public const string Transformation = "Transformation";
            public const string TransformationFolder = "TransformationFolder";
        }

        private DataSchemaProject project;
        private bool allowInternalEvents;
        private bool allowExternalEvents;

        public ProjectExplorer()
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
            // Project
            var projectNode = new TreeNode();
            projectNode.Text = project.Name;
            projectNode.ImageKey = ImageKey.Project;
            projectNode.SelectedImageKey = projectNode.ImageKey;
            projectNode.Tag = project;

            // Transformations
            var transformationFolderNode = new TreeNode();
            transformationFolderNode.Text = "Transformations";
            transformationFolderNode.ImageKey = ImageKey.TransformationFolder;
            transformationFolderNode.SelectedImageKey = transformationFolderNode.ImageKey;
            foreach (var transformation in project.Transformations)
            {
                var transformationNode = new TreeNode();
                transformationNode.Text = transformation.Name;
                transformationNode.ImageKey = ImageKey.Transformation;
                transformationNode.SelectedImageKey = transformationNode.ImageKey;
                transformationNode.Tag = transformation;
                transformationFolderNode.Nodes.Add(transformationNode);
            }
            projectNode.Nodes.Add(transformationFolderNode);

            foreach (var inputFilePath in project.InputFiles.OrderBy(i => Path.GetFileName(i)))
            {
                var inputFileNode = new TreeNode();
                inputFileNode.Text = Path.GetFileName(inputFilePath);
                inputFileNode.ImageKey = ImageKey.InputFile;
                inputFileNode.SelectedImageKey = inputFileNode.ImageKey;
                projectNode.Nodes.Add(inputFileNode);
            }


            // Add all to tree
            treeView.Nodes.Clear();
            treeView.Nodes.Add(projectNode);
            projectNode.Expand();
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
