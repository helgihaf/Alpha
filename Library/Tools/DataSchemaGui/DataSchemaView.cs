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

namespace DataSchemaGui
{
    public partial class DataSchemaView : BaseDirector
    {
        private DataSchema dataSchema;

        public DataSchemaView()
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

        public DataSchema DataSchema
        {
            get { return dataSchema; }
            set
            {
                if (object.ReferenceEquals(dataSchema, value))
                {
                    return;
                }

                if (!LeaveCurrentPage())
                {
                    throw new InvalidOperationException("Cannot hide current page");
                }

                dataSchema = value;
                projectExplorer.DataSchema = value;
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
