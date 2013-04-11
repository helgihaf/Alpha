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
    public partial class ActivitiesView : BaseView
    {
        private WorkTrackDataContext dataContext;
        private ActivityEditDialog editDialog = new ActivityEditDialog();

        public ActivitiesView()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(ActivitiesView_Disposed);

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
            foreach (var activity in dataContext.Activities.OrderBy(a => a.ProjectEntity.Name).ThenBy(a => a.Name))
            {
                AddListViewItem(activity);
            }
            listView.EndUpdate();

            UpdateActions();
        }

        void ActivitiesView_Disposed(object sender, EventArgs e)
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

        private void SetListViewItem(ListViewItem item, Activity activity)
        {
            item.SubItems.Clear();
            item.Text = activity.ProjectEntity.Name;
            item.SubItems.Add(activity.Name);
            item.SubItems.Add(activity.Description);
            item.SubItems.Add(DatabaseUtilities.BoolToYesNoString(activity.Active));
            item.SubItems.Add(activity.ExternalCode);
            item.Tag = activity;
        }

        private void AddListViewItem(Activity activity)
        {
            var item = new ListViewItem();
            SetListViewItem(item, activity);
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
            Activity activity = new Activity { Id = Guid.NewGuid(), Active = true };
            editDialog.DialogContext = new DialogContext<Activity> { SessionContext = this.SessionContext, DataContext = this.dataContext, Item = activity };
            if (editDialog.ShowDialog(this) == DialogResult.OK)
            {
                dataContext.Activities.InsertOnSubmit(activity);
                dataContext.SubmitChanges();
                listView.BeginUpdate();
                AddListViewItem(activity);
                listView.EndUpdate();
            }
        }


        private void EditButton_Click(object sender, EventArgs e)
        {
            var listViewItem = listView.SelectedItems[0];
            var activity = listViewItem.Tag as Activity;
            editDialog.DialogContext = new DialogContext<Activity> { SessionContext = this.SessionContext, DataContext = this.dataContext, Item = activity };
            if (editDialog.ShowDialog(this) == DialogResult.OK)
            {
                dataContext.SubmitChanges();
                listView.BeginUpdate();
                SetListViewItem(listViewItem, activity);
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
                msg = string.Format(Properties.Resources.DeleteActivity, ((Activity)(itemsToDelete[0].Tag)).Name);
            }
            else
            {
                msg = string.Format(Properties.Resources.DeleteManyActivities, itemsToDelete.Count);
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
                    var activity = (Activity)item.Tag;
                    dataContext.Activities.DeleteOnSubmit(activity);
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
                        exMessage = "One or more activities being deleted are being used by work tracking data. Set the activities as inactive or delete the corresponding work tracking data.";
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


    }
}
