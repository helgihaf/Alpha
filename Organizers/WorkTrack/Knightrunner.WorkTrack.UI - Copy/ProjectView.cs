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
                CopyLoadedItems(projects);
                enableChangeTracking = true;
                UpdateActions();
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

        private void bindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (enableChangeTracking)
            {
                var service = CreateWorkTrackService();
                var project = service.CreateProject();
                project.Id = Guid.NewGuid();
                e.NewObject = project;
            }
        }


        private void bindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateActions();
        }

        private void UpdateActions()
        {
            saveToolStripButton.Enabled = IsDirty();
        }




        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            IWorkTrackService service = CreateWorkTrackService();
            foreach (var submission in GetSubmissions<Project>((List<Project>)bindingSource.DataSource))
            {
                switch (submission.SubmissionType)
                {
                    case SubmissionType.Insert:
                        service.InsertProject(submission.NewItem);
                        break;
                    case SubmissionType.Update:
                        service.UpdateProject(submission.NewItem, submission.OldItem);
                        break;
                    case SubmissionType.Delete:
                        service.DeleteProject(submission.OldItem);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
