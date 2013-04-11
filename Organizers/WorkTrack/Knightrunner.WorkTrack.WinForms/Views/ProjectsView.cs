using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.WorkTrack.Database;
using Knightrunner.WorkTrack.WinForms.Dialogs;

namespace Knightrunner.WorkTrack.WinForms.Views
{
    public partial class ProjectsView : BaseView
    {
        private WorkTrackDataContext dataContext;
        private ProjectEditDialog editDialog = new ProjectEditDialog();

        public ProjectsView()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(ProjectsView_Disposed);

            listViewToolbar.AddButton.Click += new EventHandler(AddButton_Click);
            listViewToolbar.EditButton.Click += new EventHandler(EditButton_Click);
            listViewToolbar.DeleteButton.Click += new EventHandler(DeleteButton_Click);
        }

        public override void LoadData()
        {
            Cursor.Current = Cursors.WaitCursor;

            DisposeDataContext();
            dataContext = SessionContext.CreateDataContext();

            listView.BeginUpdate();
            listView.Items.Clear();
            foreach (var project in dataContext.Projects.OrderBy(p => p.Name))
            {
                AddListViewItem(project);
            }
            listView.EndUpdate();

            UpdateActions();
        }

        void ProjectsView_Disposed(object sender, EventArgs e)
        {
            DisposeDataContext();
        }

        private void DisposeDataContext()
        {
            if (dataContext != null)
            {
                dataContext.Dispose();
                dataContext = null;
            }
        }

        private void SetListViewItem(ListViewItem item, Project project)
        {
            item.SubItems.Clear();
            item.Text = project.Name;
            item.SubItems.Add(project.Description);
            item.SubItems.Add(DatabaseUtilities.BoolToYesNoString(project.Active));
            item.SubItems.Add(project.ExternalCode);
            item.Tag = project;
        }


        private void AddListViewItem(Project project)
        {
            var item = new ListViewItem();
            SetListViewItem(item, project);
            listView.Items.Add(item);
        }


        private void UpdateActions()
        {
            int selectedCount = listView.SelectedIndices.Count;

            listViewToolbar.EditButton.Enabled = selectedCount == 1;
            listViewToolbar.DeleteButton.Enabled = selectedCount >= 1;
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }


        private void AddButton_Click(object sender, EventArgs e)
        {
            Project project = new Project { Id = Guid.NewGuid(), Active = true };
            editDialog.DialogContext = new DialogContext<Project> { SessionContext = this.SessionContext, DataContext = this.dataContext, Item = project };
            if (editDialog.ShowDialog(this) == DialogResult.OK)
            {
                dataContext.Projects.InsertOnSubmit(project);
                dataContext.SubmitChanges();
                listView.BeginUpdate();
                AddListViewItem(project);
                listView.EndUpdate();
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var listViewItem = listView.SelectedItems[0];
            var project = listViewItem.Tag as Project;
            editDialog.DialogContext = new DialogContext<Project> { SessionContext = this.SessionContext, DataContext = this.dataContext, Item = project };
            if (editDialog.ShowDialog(this) == DialogResult.OK)
            {
                dataContext.SubmitChanges();
                listView.BeginUpdate();
                SetListViewItem(listViewItem, project);
                listView.EndUpdate();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                return;
            }

            var itemsToDelete = new List<ListViewItem>();
            foreach (ListViewItem item in listView.SelectedItems)
            {
                itemsToDelete.Add(item);
            }

            string msg;
            if (itemsToDelete.Count == 1)
            {
                msg = string.Format(Properties.Resources.DeleteProject, ((Project)(itemsToDelete[0].Tag)).Name);
            }
            else
            {
                msg = string.Format(Properties.Resources.DeleteManyProjects, itemsToDelete.Count);
            }

            if (MessageBox.Show(this, msg, Properties.Resources.DeleteHeader, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }

            bool reloadData = false;
            Cursor.Current = Cursors.WaitCursor;
            listView.BeginUpdate();
            try
            {
                foreach (var item in itemsToDelete)
                {
                    var project = (Project)item.Tag;
                    dataContext.Activities.DeleteAllOnSubmit(project.Activities);
                    dataContext.Projects.DeleteOnSubmit(project);
                    listView.Items.Remove(item);
                }
                try
                {
                    dataContext.SubmitChanges();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    string exMessage;
                    if (ex.Number == DatabaseErrors.ForeignKeyReference)
                    {
                        exMessage = "One or more projects being deleted are being used by work tracking data. Set the project as inactive or delete the corresponding work tracking data.";
                    }
                    else
                    {
                        exMessage = ex.Message;
                    }
                    
                    var errorMsg = string.Format("An error occurred when deleting. {0}", exMessage);
                    MessageBox.Show(this, errorMsg, Properties.Resources.DeleteHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reloadData = true;
                }
            }
            finally
            {
                listView.EndUpdate();
            }

            if (reloadData)
            {
                LoadData();
            }
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            EditButton_Click(listViewToolbar.EditButton, e);
        }


    }
}
