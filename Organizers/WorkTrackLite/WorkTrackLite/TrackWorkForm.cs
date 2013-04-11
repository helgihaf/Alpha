using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkTrackLite
{
    public partial class TrackWorkForm : Form
    {
        private WorkEntry lastWorkEntry;

        public TrackWorkForm()
        {
            InitializeComponent();
        }

        private void TrackWorkForm_VisibleChanged(object sender, EventArgs e)
        {
            textBoxWorkDescription.Focus();
            textBoxWorkDescription.SelectAll();
        }

        public event EventHandler<TrackWorkEventArgs> Applied;

        private void buttonOk_Click(object sender, EventArgs e)
        {
            lastWorkEntry = new WorkEntry();
            lastWorkEntry.DateTime = DateTime.Now;
            lastWorkEntry.Description = textBoxWorkDescription.Text;
            OnApplied(lastWorkEntry);
            this.Hide();
        }

        private void OnApplied(WorkEntry lastWorkEntry)
        {
            if (Applied != null)
            {
                Applied(this, new TrackWorkEventArgs { WorkEntry = lastWorkEntry });
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void TrackWorkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

    }


    public class TrackWorkEventArgs : EventArgs
    {
        public WorkEntry WorkEntry { get; set; }
    }
}
