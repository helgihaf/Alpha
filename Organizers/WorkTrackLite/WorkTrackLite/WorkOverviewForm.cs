using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace WorkTrackLite
{
    public partial class WorkOverviewForm : Form
    {
        private WorkEntry[] workEntries;
        private bool dataRefreshNeeded;

        public WorkOverviewForm()
        {
            InitializeComponent();
        }

        public WorkEntry[] WorkEntries
        {
            get { return workEntries; }
            set
            {
                if (!object.ReferenceEquals(workEntries, value))
                {
                    workEntries = value;
                    dataRefreshNeeded = true;
                }
            }
        }

        private void WorkOverviewForm_Activated(object sender, EventArgs e)
        {
            if (dataRefreshNeeded)
            {
                RefreshData();
                dataRefreshNeeded = false;
            }
        }

        private void RefreshData()
        {
            Cursor.Current = Cursors.WaitCursor;

            // Make dates bold that have entries
            HashSet<DateTime> boldDates = new HashSet<DateTime>();
            foreach (var workEntry in workEntries)
            {
                boldDates.Add(workEntry.DateTime.Date);
            }
            monthCalendar.BoldedDates = boldDates.ToArray();
            RefreshListViewData();
        }


        private void RefreshListViewData()
        {
            listView.BeginUpdate();
            listView.Items.Clear();

            var dateFrom = monthCalendar.SelectionStart;
            var dateTo = monthCalendar.SelectionEnd;

            foreach (var workEntry in workEntries)
            {
                var date = workEntry.DateTime.Date;
                if (date >= dateFrom && date <= dateTo)
                {
                    var item = new ListViewItem();
                    SetListViewItem(item, workEntry);
                    listView.Items.Add(item);
                }
            }

            listView.EndUpdate();
        }

        private void SetListViewItem(ListViewItem item, WorkEntry workEntry)
        {
            item.SubItems.Clear();
            item.Text = workEntry.DateTime.ToShortDateString();
            item.SubItems.Add(workEntry.DateTime.ToShortTimeString());
            item.SubItems.Add(workEntry.Description);
            item.Tag = workEntry;
        }

        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            RefreshListViewData();
        }

        private void WorkOverviewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            CopySelectedLines();
        }

        private void copySelectedLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedLines();
        }

        private void CopySelectedLines()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListViewItem item in listView.SelectedItems)
            {
                sb.AppendLine(WorkEntryClipboardString((WorkEntry)item.Tag));
            }
            Clipboard.SetText(sb.ToString());
        }

        private string WorkEntryClipboardString(WorkEntry workEntry)
        {
            return workEntry.DateTime.ToString(CultureInfo.CurrentCulture) + "\t" + workEntry.Description;
        }



    }
}
