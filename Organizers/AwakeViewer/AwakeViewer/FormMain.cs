using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace AwakeViewer
{
    public partial class FormMain : Form
    {
        private SessionCollection sessions;

        public FormMain()
        {
            InitializeComponent();
        }


        private void FormMain_Activated(object sender, EventArgs e)
        {
            if (sessions == null)
            {
                LoadSessions();
                bindingSource.DataSource = sessions;
                dataGridView.DataSource = bindingSource;
            }
        }

        private void LoadSessions()
        {
            SetStatus("Loading sessions...");
            sessions = SessionCollection.Load();
            SetStatus("Updating with event log...");
            sessions.UpdateFromEventLog();
            ClearStatus();
        }

        private void SetStatus(string msg)
        {
            toolStripStatusLabel.Text = msg;
        }

        private void ClearStatus()
        {
            toolStripStatusLabel.Text = string.Empty;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            sessions.Save();
        }

        private void bindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            Log("bindingSource_AddingNew");
        }

        private void bindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            Log("bindingSource_CurrentItemChanged");
        }

        private void bindingSource_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            Log("bindingSource_BindingComplete");
        }

        private void bindingSource_CurrentChanged(object sender, EventArgs e)
        {
            Log("bindingSource_CurrentChanged");

        }

        private void bindingSource_DataError(object sender, BindingManagerDataErrorEventArgs e)
        {
            Log("bindingSource_DataError");

        }

        private void bindingSource_DataMemberChanged(object sender, EventArgs e)
        {
            Log("bindingSource_DataMemberChanged");

        }

        private void bindingSource_DataSourceChanged(object sender, EventArgs e)
        {
            Log("bindingSource_DataSourceChanged");

        }

        private void bindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            Session session = bindingSource.Current as Session;
            if (session != null)
            {
                sessions.Remove(session);
                sessions.Add(session);
            }

        }

        private void bindingSource_PositionChanged(object sender, EventArgs e)
        {
            Log("bindingSource_PositionChanged");

        }

        private void Log(string p)
        {
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.ffff") + " " + p);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            bindingSource.RemoveCurrent();
        }

    }
}
