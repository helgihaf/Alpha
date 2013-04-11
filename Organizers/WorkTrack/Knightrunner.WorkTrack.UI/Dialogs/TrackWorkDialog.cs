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
    public partial class TrackWorkDialog : DialogBase
    {
        private ISessionContext sessionContext;
        private WorkTrackDataContext dataContext;

        private TrackWorkDialog()
        {
            InitializeComponent();
        }

        public TrackWorkDialog(ISessionContext sessionContext)
            : this()
        {
            this.sessionContext = sessionContext;
            this.dataContext = sessionContext.CreateDataContext();
        }

        private void TrackWorkDialog_Load(object sender, EventArgs e)
        {
            PopulateProjects();
            comboBoxActivity.Items.Clear();
            comboBoxActivity.Items.Add(new DataSelector { Text = "(none)", Object = null });
            comboBoxActivity.SelectedIndex = 0;
            UpdateActions();
        }

        private void TrackWorkDialog_Shown(object sender, EventArgs e)
        {
            dateTimeStart.Value = DateTime.Now;
            dateTimeEnd.Value = DateTime.Now.AddMinutes(30);
        }

        private void PopulateProjects()
        {
            var projects =
                from project in dataContext.Projects
                where project.Active
                orderby project.Name
                select project;

            comboBoxProject.Items.Clear();
            comboBoxProject.Items.Add(new DataSelector { Text = "(none)", Object = null });
            foreach (var project in projects)
            {
                comboBoxProject.Items.Add(new DataSelector { Text = project.Name, Object = project });
            }
            comboBoxProject.SelectedIndex = 0;
        }


        private void PopulateActivities()
        {
            var selectedDataSelector = (DataSelector)comboBoxProject.SelectedItem;
            var selectedProject = selectedDataSelector.Object as Project;

            List<Activity> selectionActivities = new List<Activity>();
            if (selectedProject != null)
            {
                selectionActivities.AddRange
                (
                    from activity in dataContext.Activities
                    where activity.Project == selectedProject.Id && activity.Active
                    orderby activity.Name
                    select activity
                );
            }

            selectionActivities.AddRange
            (
                from activity in dataContext.Activities
                where activity.Project == null && activity.Active
                orderby activity.Name
                select activity
            );

            comboBoxActivity.Items.Clear();
            comboBoxActivity.Items.Add(new DataSelector { Text = "(none)", Object = null });
            foreach (var activity in selectionActivities)
            {
                comboBoxActivity.Items.Add(new DataSelector { Text = activity.Name, Object = activity });
            }
            comboBoxActivity.SelectedIndex = 0;
        }


        private void UpdateActions()
        {
            okCancelControl.ButtonOK.Enabled =
                comboBoxText.Text.Length > 0 &&
                (!checkBoxEnd.Checked || dateTimeStart.Value < dateTimeEnd.Value);
        }

        private void comboBoxProject_SelectedValueChanged(object sender, EventArgs e)
        {
            PopulateActivities();
        }

        private void okCancelControl_ButtonOKClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            WorkEntry entry = new WorkEntry
            {
                Id = Guid.NewGuid(),
                User = sessionContext.UserId,
                Start = dateTimeStart.Value,
                Text = comboBoxText.Text
            };
            var project = GetSelection(comboBoxProject) as Project;
            if (project != null)
            {
                entry.Project = project.Id;
            }
            var activity = GetSelection(comboBoxActivity) as Activity;
            if (activity != null)
            {
                entry.Activity = activity.Id;
            }
            if (checkBoxEnd.Checked)
            {
                entry.End = dateTimeEnd.Value;
            }
            dataContext.AddWorkEntry(entry);
            dataContext.SubmitChanges();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Hide();
        }

        private object GetSelection(ComboBox comboBox)
        {
            var selection = (DataSelector)comboBox.SelectedItem;
            return selection.Object;
        }

        private void comboBoxText_TextChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }

        private void dateTimeStart_ValueChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }

        private void checkBoxEnd_CheckedChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }

        private void dateTimeEnd_ValueChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }

        private void okCancelControl_ButtonCancelClick(object sender, EventArgs e)
        {
            Hide();
        }


    }
}
