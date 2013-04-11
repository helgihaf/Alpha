using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace AwakeViewer
{
    public partial class SessionView : UserControl
    {
        private SessionCollection sessions;
        private bool privatePendingChanges;

        public SessionView()
        {
            InitializeComponent();
        }


        public void LoadData(SessionCollection sessions)
        {
            this.sessions = sessions;
            RefreshListView();
        }

        public event EventHandler<EventArgs> PendingChangesChanged;

        public bool PendingChanges
        {
            get { return privatePendingChanges; }
            set
            {
                if (privatePendingChanges != value)
                {
                    privatePendingChanges = value;
                    OnPendingChangesChanged();
                }
            }
        }


        private void RefreshListView()
        {
            listView.BeginUpdate();
            listView.Items.Clear();
            if (sessions != null)
            {
                foreach (var session in sessions)
                {
                    ListViewItem item = new ListViewItem();
                    SetListViewItem(item, session);
                    listView.Items.Add(item);
                }
                listView.EndUpdate();
            }
            PendingChanges = false;
            UpdateActions();
        }

        private void UpdateActions()
        {
            int selectedCount = listView.SelectedIndices.Count;
            buttonEdit.Enabled = selectedCount == 1;
            buttonDelete.Enabled = selectedCount >= 1;
            buttonMerge.Enabled = selectedCount >= 2 && CanMergeSelection();
        }


        private void OnPendingChangesChanged()
        {
            if (PendingChangesChanged != null)
            {
                PendingChangesChanged(this, EventArgs.Empty);
            }
        }



        private void SetListViewItem(ListViewItem item, Session session)
        {
            item.SubItems.Clear();
            item.Text = Formatter.FormatDateTime(session.Start);
            item.SubItems.Add(Formatter.FormatDayOfWeek(session.Start));
            item.SubItems.Add(ValueOrEmpty(session.End));
            
            string durationString;
            if (session.End.HasValue)
            {
                var duration = session.End.Value - session.Start;
                durationString = Formatter.FormatHours(duration.TotalHours);
            }
            else
            {
                durationString = string.Empty;
            }
            item.SubItems.Add(durationString);

            item.SubItems.Add(session.Category.ToString());
            item.SubItems.Add(session.Text);
            item.Tag = session;

            if (session.IsWeekend)
            {
                MarkItemAsWeekend(item);
            }
            else if (session.IsOffHours)
            {
                MarkItemAsOffHours(item);
            }
        }

        private void MarkItemAsWeekend(ListViewItem item)
        {
            item.BackColor = Color.FromArgb(255, 220, 220);
        }

        private void MarkItemAsOffHours(ListViewItem item)
        {
            item.BackColor = Color.FromArgb(255, 255, 220);
        }

        private string ValueOrEmpty(DateTime? dateTime)
        {
            if (dateTime.HasValue)
                return Formatter.FormatDateTime(dateTime.Value);
            else
                return string.Empty;
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        private void DeleteSelected()
        {
            listView.BeginUpdate();
            int firstIndex = listView.SelectedIndices[0];
            while (listView.SelectedIndices.Count > 0)
            {
                DeleteItem(listView.SelectedIndices[0]);
            }
            if (firstIndex < listView.Items.Count)
            {
                ListViewItem item = listView.Items[firstIndex];
                item.Selected = true;
                item.EnsureVisible();
            }
            listView.EndUpdate();
        }

        private void DeleteItem(int index)
        {
            Session session = (Session)listView.Items[index].Tag;
            listView.Items.RemoveAt(index);
            sessions.Remove(session);
            PendingChanges = true;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = listView.SelectedItems[0];

            Session session = (Session)selectedItem.Tag;
            using (EditSessionDialog dialog = new EditSessionDialog())
            {
                if (dialog.ShowDialog(this, session) == DialogResult.OK)
                {
                    SetListViewItem(selectedItem, session);
                    PendingChanges = true;
                }
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            Session session = new Session();
            session.Start = DateTime.Now;
            session.Category = SessionCategory.New;
            using (EditSessionDialog dialog = new EditSessionDialog())
            {
                if (dialog.ShowDialog(this, session) == DialogResult.OK)
                {
                    sessions.Add(session);
                    RefreshListView();
                    // Select the newly created item
                    for (int i = 0; i < listView.Items.Count; i++)
                    {
                        ListViewItem item = listView.Items[i];
                        if (object.ReferenceEquals(item.Tag, session))
                        {
                            item.Selected = true;
                            item.EnsureVisible();
                            break;
                        }
                    }
                    PendingChanges = true;
                }
            }
        }

        private bool CanMergeSelection()
        {
            // Check if the indexes in the selection are continuous
            bool isContinuous = true;
            int index = listView.SelectedIndices[0];
            for (int i = 1; i < listView.SelectedIndices.Count; i++)
            {
                index++;
                if (index != listView.SelectedIndices[i])
                {
                    isContinuous = false;
                    break;
                }
            }

            return isContinuous;
        }

        private void buttonMerge_Click(object sender, EventArgs e)
        {
            // Merge: Delete all selected items except the first one, put last End datetime found in
            // the End of first item. All texts are combined.
            listView.BeginUpdate();
            
            ListViewItem targetListViewItem = listView.SelectedItems[0];
            Session target = (Session)targetListViewItem.Tag;
            
            while (listView.SelectedItems.Count > 1)
            {
                ListViewItem sourceListViewItem = listView.SelectedItems[1];
                Session source = (Session)sourceListViewItem.Tag;
                if (source.End.HasValue && (!target.End.HasValue || target.End.Value < source.End.Value))
                {
                    target.End = source.End.Value;
                }

                if (source.Text != null)
                {
                    if (target.Text != null)
                        target.Text += " " + source.Text;
                    else
                        target.Text = source.Text;
                }

                listView.Items.Remove(sourceListViewItem);
                sessions.Remove(source);
                PendingChanges = true;
            }

            SetListViewItem(targetListViewItem, target);
            listView.EndUpdate();
        }

        private void listView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && buttonDelete.Enabled)
            {
                DeleteSelected();
            }
        }



    }
}
