using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Knightrunner.WorkTrack.Model;
using System.ServiceModel;

namespace Knightrunner.WorkTrack.UI
{
    public partial class ProjectView : BaseView
    {
        private List<Project> loadedProjects;
        public ProjectView()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            try
            {
                IWorkTrackService service = CreateWorkTrackService();
                this.loadedProjects = new List<Project>(service.GetProjects());
                bindingSource.DataSource = DeepClone(this.loadedProjects);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().FullName + " " + ex.Message);
            }
        }

        private IWorkTrackService CreateWorkTrackService()
        {
            ChannelFactory<IWorkTrackService> channelFactory = new ChannelFactory<IWorkTrackService>("BasicHttpBinding_IWorkTrackService");
            return channelFactory.CreateChannel();
        }

        private List<Project> DeepClone(List<Project> list)
        {
            var result = new List<Project>();
            foreach (var project in list)
            {
                result.Add(project.Clone());
            }

            return result;
        }

        private void bindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            //Log("AddingNew");
        }

        private void Log(string p)
        {
            Debug.WriteLine(DateTime.Now.ToString("s") + " " + p);
        }

        private void bindingSource_CurrentChanged(object sender, EventArgs e)
        {
            Log("CurrentChanged");
        }

        private void bindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            //Log("CurrentItemChanged");
        }

        private void bindingSource_DataError(object sender, BindingManagerDataErrorEventArgs e)
        {
            Log("DataError");
        }

        private void bindingSource_DataMemberChanged(object sender, EventArgs e)
        {
            //Log("DataMemberChanged");

        }

        private void bindingSource_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            //Log("BindingComplete");

        }

        private void bindingSource_DataSourceChanged(object sender, EventArgs e)
        {
            //Log("DataSourceChanged");

        }

        private void bindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            Log("ListChanged " + e.ListChangedType.ToString());
        }

        private void UpdateActions()
        {
            //bool enableSave = changedItems.Count > 0 || deletedItems.Count > 0;
            //if (!enableSave)
            //{
            //    enableSave = AreAddedItems();
            //}

            //saveToolStripButton.Enabled = enableSave;
        }


        private void bindingSource_PositionChanged(object sender, EventArgs e)
        {
            //Log("PositionChanged");

        }
    }
}
