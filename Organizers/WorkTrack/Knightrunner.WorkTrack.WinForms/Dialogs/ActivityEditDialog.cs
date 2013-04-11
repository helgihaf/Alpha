using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.WorkTrack.Database;

namespace Knightrunner.WorkTrack.WinForms.Dialogs
{
    public partial class ActivityEditDialog : DialogBase
    {
        private DialogContext<Activity> dialogContext;

        public ActivityEditDialog()
        {
            InitializeComponent();
        }

        private void UpdateActions()
        {
            buttonOK.Enabled = textBoxName.TextLength > 0;

        }


        public DialogContext<Activity> DialogContext
        {
            get { return dialogContext; }

            set
            {
                if (object.ReferenceEquals(dialogContext, value))
                {
                    return;
                }

                PopulateProjectComboBox();
                dialogContext = value;
                ItemToForm();
            }

        }

        private void PopulateProjectComboBox()
        {
            comboBoxProject.Items.Clear();
            comboBoxProject.Items.Add(new DataSelector { Text = "(none)" });
            var projects =
                from project in dialogContext.DataContext.Projects
                where project.Active
                orderby project.Name
                select project;
            //!!
            //foreach (var project in projects)
            //{
            //    comboBoxProject.Items.Add(
            //}
        }

        private void ItemToForm()
        {
            var activity = dialogContext.Item;
            textBoxName.Text = activity.Name;
            textBoxDescription.Text = activity.Description;
            checkBoxActive.Checked = activity.Active;
            textBoxExternalCode.Text = activity.ExternalCode;
            UpdateActions();
        }

        private void ItemFromForm()
        {
            var activity = dialogContext.Item;
            activity.Name = textBoxName.Text;
            activity.Description = WinFormUtilities.TextOrNull(textBoxDescription.Text);
            activity.Active = checkBoxActive.Checked;
            activity.ExternalCode = WinFormUtilities.TextOrNull(textBoxExternalCode.Text);
        }


    }
}
