using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.WorkTrack.Database;

namespace Knightrunner.WorkTrack.UI.Views
{
    public partial class WorkEntriesEditView : UserControl
    {
        private ISessionContext sessionContext;
        private WorkTrackDataContext dataContext;

        public WorkEntriesEditView()
        {
            InitializeComponent();
        }


        public void LoadData(ISessionContext sessionContext)
        {
            this.sessionContext = sessionContext;
            var today = DateTime.Today;
            monthCalendar.SelectionRange = new SelectionRange(today, today);
            RefreshData();
        }

        private void RefreshData()
        {
            if (dataContext != null)
            {
                dataContext.Dispose();
                dataContext = null;
            }
            dataContext = sessionContext.CreateDataContext();

            listView.BeginUpdate();
            listView.Items.Clear();

            var start = monthCalendar.SelectionRange.Start;
            var end = monthCalendar.SelectionRange.End.AddDays(1);

            var entries =
                from entry in dataContext.WorkEntries
                where entry.Start >= start && entry.Start < end
                orderby entry.Start
                select entry;

            foreach (var entry in entries)
            {
                var item = new ListViewItem();
                SetListViewItem(item, entry);
                listView.Items.Add(item);
            }

            listView.EndUpdate();
            UpdateActions();
        }

        private void SetListViewItem(ListViewItem item, WorkEntry entry)
        {
            item.SubItems.Clear();
            item.Text = entry.Start.ToString("T");
            string endString = string.Empty;
            if (entry.End.HasValue)
            {
                if (entry.Start.Date <= entry.End.Value.Date)
                {
                    endString = entry.End.Value.ToString("T");
                }
                else
                {
                    endString = entry.End.Value.ToString();
                }
            }
            item.SubItems.Add(endString);

            string projectString = string.Empty;
            if (entry.ProjectEntity != null)
            {
                projectString = entry.ProjectEntity.Name;
            }
            item.SubItems.Add(projectString);

            string activityString = string.Empty;
            if (entry.ActivityEntity != null)
            {
                activityString = entry.ActivityEntity.Name;
            }
            item.SubItems.Add(activityString);

            item.SubItems.Add(entry.Text);

            item.Tag = entry;
        }

        private void UpdateActions()
        {
            var changeSet = dataContext.GetChangeSet();
            buttonSave.Enabled = changeSet.Deletes.Count > 0 || changeSet.Inserts.Count > 0 || changeSet.Updates.Count > 0;
            buttonCancel.Enabled = buttonSave.Enabled;
        }
    }
}
