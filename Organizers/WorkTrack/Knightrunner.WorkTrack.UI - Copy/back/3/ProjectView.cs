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
        private bool enableChangeTracking;
        private Dictionary<Guid, Project> loadedItems = new Dictionary<Guid, Project>();
        private Dictionary<Guid, Project> addedItems = new Dictionary<Guid, Project>();
        private Dictionary<Guid, Project> changedItems = new Dictionary<Guid, Project>();
        private Dictionary<Guid, Project> deletedItems = new Dictionary<Guid, Project>();

        public ProjectView()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            try
            {
                enableChangeTracking = false;
                IWorkTrackService service = CreateWorkTrackService();
                var projects = new List<Project>(service.GetProjects());
                bindingSource.DataSource = projects;
                CopyProjects(loadedItems, projects);
                changedItems.Clear();
                deletedItems.Clear();
                enableChangeTracking = true;
                UpdateActions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().FullName + " " + ex.Message);
            }
        }

        private void CopyProjects(Dictionary<Guid, Project> loadedItems, List<Project> projects)
        {
            foreach (var project in projects)
            {
                var clone = project.Clone();
                loadedItems.Add(clone.Id, clone);
            }
        }


        private IWorkTrackService CreateWorkTrackService()
        {
            ChannelFactory<IWorkTrackService> channelFactory = new ChannelFactory<IWorkTrackService>("BasicHttpBinding_IWorkTrackService");
            return channelFactory.CreateChannel();
        }

        private void bindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (enableChangeTracking)
            {
                var service = CreateWorkTrackService();
                var project = service.CreateProject();
                project.Id = Guid.NewGuid();
                addedItems.Add(project.Id, project);
                e.NewObject = project;
            }
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
            Log("DataMemberChanged");

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
            if (enableChangeTracking)
            {
                if (e.ListChangedType == ListChangedType.ItemChanged)
                {
                    var item = (Project)bindingSource.CurrencyManager.List[e.NewIndex];
                    if (!addedItems.ContainsKey(item.Id))
                    {
                        changedItems[item.Id] = item;
                    }
                }
                else if (e.ListChangedType == ListChangedType.ItemDeleted)
                {
                    var item = (Project)bindingSource.CurrencyManager.List[e.NewIndex];
                    if (addedItems.ContainsKey(item.Id))
                    {
                        addedItems.Remove(item.Id);
                    }
                    else
                    {
                        changedItems.Remove(item.Id);
                        deletedItems[item.Id] = item;
                    }
                }

                UpdateActions();
            }
        }

        private void UpdateActions()
        {
            bool enableSave = changedItems.Count > 0 || deletedItems.Count > 0 || addedItems.Count > 0;
            saveToolStripButton.Enabled = enableSave;
        }



        private void bindingSource_PositionChanged(object sender, EventArgs e)
        {
            //Log("PositionChanged");

        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            IWorkTrackService service = CreateWorkTrackService();
            foreach (var item in addedItems.Values)
            {
                service.InsertProject(item);
            }
            foreach (var item in changedItems.Values)
            {
                service.UpdateProject(item, loadedItems[item.Id]);
            }
            foreach (var item in deletedItems.Values)
            {
                service.DeleteProject(item);
            }
        }
    }
}
