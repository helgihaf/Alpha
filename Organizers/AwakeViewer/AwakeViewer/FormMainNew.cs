using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwakeViewer
{
    public partial class FormMainNew : Form
    {
        private SessionCollection sessions;

        private delegate void SetStatusDelegate(string msg);
        private SetStatusDelegate setStatusDelegate;


        public FormMainNew()
        {
            InitializeComponent();
            setStatusDelegate = new SetStatusDelegate(SetStatus);
        }

        private void FormMainNew_Load(object sender, EventArgs e)
        {
            if (sessions == null)
            {
                sessionView.Enabled = false;
                backgroundWorker.RunWorkerAsync();

            }
        }

        private void SetStatus(string msg)
        {
            if (!this.InvokeRequired)
            {
                toolStripStatusLabel.Text = msg;
            }
            else
            {
                this.Invoke(setStatusDelegate, msg);
            }
        }


        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            SetStatus("Loading sessions...");
            sessions = SessionCollection.Load();
            SetStatus("Updating with event log...");
            sessions.UpdateFromEventLog();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetStatus(null);
            sessionView.Enabled = true;
            sessionView.LoadData(sessions);

            if (e.Error != null)
            {
                ShowError("An error occurred while loading sessions.", e.Error);
            }
        }

        private void ShowError(string msg, Exception exception)
        {
            string exceptionText = Formatter.FormatExceptionString(exception);
            string message = msg + Environment.NewLine + Environment.NewLine + exceptionText;
            MessageBox.Show(this, message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveSession();
        }

        private bool SaveSession()
        {
            bool result = false;

            if (sessions != null)
            {
                SetStatus("Saving...");
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    sessions.Save();
                    result = true;
                    sessionView.PendingChanges = false;
                }
                catch (Exception ex)
                {
                    ShowError("An error occurred during save.", ex);
                }
                finally
                {
                    SetStatus(null);
                    this.Cursor = Cursors.Default;
                }
            }

            return result;
        }

        private void sessionView_PendingChangesChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = sessionView.PendingChanges;
        }

        private void FormMainNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sessionView.PendingChanges)
            {
                string msg = "You have unsaved changes in your session list. Do you want to save your changes?";
                var result = MessageBox.Show(this, msg, this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    bool saved = SaveSession();
                    if (!saved)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}
