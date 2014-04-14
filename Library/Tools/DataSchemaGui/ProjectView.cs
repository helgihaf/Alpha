using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.Library.Controls.PageSelection;
using Knightrunner.Library.Database.Schema;
using Knightrunner.Library.Database.Schema.Project;

namespace DataSchemaGui
{
    public partial class ProjectView : BaseDirector
    {
        private DataSchemaProject project;

        public ProjectView()
        {
            InitializeComponent();
            projectExplorer.Initialize(this);
        }

        internal void ApplySettings(Properties.Settings settings)
        {
            if (settings.SplitterDistance > 4)
                this.splitContainer.SplitterDistance = settings.SplitterDistance;
        }

        internal void SaveToSettings(Properties.Settings settings, bool isMaximized)
        {
            if (!isMaximized)
            {
                settings.SplitterDistance = splitContainer.SplitterDistance;
            }
        }

        public DataSchemaProject Project
        {
            get { return project; }
            set
            {
                if (object.ReferenceEquals(project, value))
                {
                    return;
                }

                if (!LeaveCurrentPage())
                {
                    throw new InvalidOperationException("Cannot hide current page");
                }

                project = value;
                projectExplorer.Project = value;
            }
        }


        public override Control PageParentControl
        {
            get { return titlePanel; }
        }

        public MessageLog MessageLog
        {
            get { return messageLog; }
        }



        public override bool ShowObjectPage(object dataObject)
        {
            var result = base.ShowObjectPage(dataObject);
            if (result)
            {
                string caption = string.Empty;
                Image image = null;
                if (CurrentPage != null)
                {
                    caption = CurrentPage.Caption;
                    image = projectExplorer.GetCurrentImage();
                }
                titlePanel.Text = caption;
                titlePanel.Image = image;
            }


            return result;
        }
    }
}
