using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.WorkTrack.Database;

namespace Knightrunner.WorkTrack.UI.Dialogs
{
    public partial class TrackWorkSimpleDialog : DialogBase
    {
        private ISessionContext sessionContext;
        private WorkTrackDataContext dataContext;

        private TrackWorkSimpleDialog()
        {
            InitializeComponent();
        }

        public TrackWorkSimpleDialog(ISessionContext sessionContext)
            : this()
        {
            this.sessionContext = sessionContext;
            dataContext = sessionContext.CreateDataContext();
        }

        private void TrackWorkSimpleDialog_Load(object sender, EventArgs e)
        {
            if (sessionContext == null)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            comboBoxText.Items.Clear();
            comboBoxText.Items.AddRange
            (
                (from entry in dataContext.WorkEntries
                 where entry.User == sessionContext.UserId
                 orderby entry.Start descending
                 select entry.Text).Distinct().Take(10).ToArray()
            );

            comboBoxText.Focus();
            UpdateActions();
        }

        private void UpdateActions()
        {
            okCancelControl.ButtonOK.Enabled = comboBoxText.Text.Length > 0;
        }

        private void okCancelControl_ButtonOKClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            WorkEntry entry = new WorkEntry
            {
                Id = Guid.NewGuid(),
                User = sessionContext.UserId,
                Start = DateTime.Now,
                Text = comboBoxText.Text
            };
            dataContext.AddWorkEntry(entry);
            dataContext.SubmitChanges();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }


    }
}
