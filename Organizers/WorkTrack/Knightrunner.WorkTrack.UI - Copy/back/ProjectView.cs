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

namespace Knightrunner.WorkTrack.UI
{
    public partial class ProjectView : BaseView
    {
        private bool enableChangeTracking;
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
                WorkTrackServiceReference.WorkTrackServiceClient client = new WorkTrackServiceReference.WorkTrackServiceClient("BasicHttpBinding_IWorkTrackService");
                var projects = new List<Project>(client.GetProjects());
                bindingSource.DataSource = projects;
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
            if (enableChangeTracking)
            {
                if (e.ListChangedType == ListChangedType.ItemChanged)
                {
                    var item = (Project)bindingSource.CurrencyManager.List[e.NewIndex];
                    if (item.Id != Guid.Empty)
                    {
                        changedItems[item.Id] = item;
                    }
                }
                else if (e.ListChangedType == ListChangedType.ItemDeleted)
                {
                    var item = (Project)bindingSource.CurrencyManager.List[e.NewIndex];
                    if (item.Id != Guid.Empty)
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
            bool enableSave = changedItems.Count > 0 || deletedItems.Count > 0;
            if (!enableSave)
            {
                enableSave = AreAddedItems();
            }

            saveToolStripButton.Enabled = enableSave;
        }

        private bool AreAddedItems()
        {
            foreach (Project project in (IEnumerable<Project>)bindingSource.DataSource)
            {
                if (project.Id == Guid.Empty)
                {
                    return true;
                }
            }

            return false;
        }



        private void bindingSource_PositionChanged(object sender, EventArgs e)
        {
            //Log("PositionChanged");

        }
    }
}
