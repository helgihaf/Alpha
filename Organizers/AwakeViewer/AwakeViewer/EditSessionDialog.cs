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
    public partial class EditSessionDialog : Form
    {
        private Session session;

        public EditSessionDialog()
        {
            InitializeComponent();

            ComboUtils.PopulateComboBoxItems<SessionCategory>(comboBoxCategory);
        }


        public DialogResult ShowDialog(IWin32Window owner, Session session)
        {
            this.session = session;
            SessionToForm();
            return ShowDialog(owner);
        }

        private void SessionToForm()
        {
            SetDateTime(session.Start, dtpStartDate, dtpStartTime);
            if (session.End.HasValue)
                SetDateTime(session.End.Value, dtpEndDate, dtpEndTime);
            else
            {
                DateTime endOfDay = new DateTime(session.Start.Year, session.Start.Month, session.Start.Day);
                endOfDay = endOfDay.AddDays(1).AddSeconds(-1);
                SetDateTime(endOfDay, dtpEndDate, dtpEndTime);
            }
            ComboUtils.SetComboSelection(comboBoxCategory, (int)session.Category);
            textBoxText.Text = session.Text;
            UpdateDuration();
        }

        private void SessionFromForm()
        {
            session.Start = GetDateTime(dtpStartDate, dtpStartTime);
            session.End = GetDateTime(dtpEndDate, dtpEndTime);
            session.Category = (SessionCategory)ComboUtils.GetComboSelection(comboBoxCategory);
            if (textBoxText.TextLength > 0)
                session.Text = textBoxText.Text;
            else
                session.Text = null;
        }


        private void UpdateDuration()
        {
            textBoxDuration.Text = Formatter.FormatHours((GetDateTime(dtpEndDate, dtpEndTime) - GetDateTime(dtpStartDate, dtpStartTime)).TotalHours);
        }

        private void SetDateTime(DateTime dateTime, DateTimePicker dtpDate, DateTimePicker dtpTime)
        {
            dtpDate.Value = dateTime.Date;
            dtpTime.Value = dateTime;
        }

        private DateTime GetDateTime(DateTimePicker dtpDate, DateTimePicker dtpTime)
        {
            return dtpDate.Value.Date + dtpTime.Value.TimeOfDay;
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateDuration();
        }

        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            UpdateDuration();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateDuration();
        }

        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            UpdateDuration();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SessionFromForm();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }



    }
}
