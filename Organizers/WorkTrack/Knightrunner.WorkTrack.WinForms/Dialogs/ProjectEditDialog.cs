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
    public partial class ProjectEditDialog : DialogBase
    {
        private DialogContext<Project> dialogContext;

        public ProjectEditDialog()
        {
            InitializeComponent();
            warningProvider.Icon = Properties.Resources.Warning;
        }


        private void UpdateActions()
        {
            buttonOK.Enabled = textBoxName.TextLength > 0;
              
        }


        public DialogContext<Project> DialogContext
        {
            get { return dialogContext; }

            set
            {
                if (object.ReferenceEquals(dialogContext, value))
                {
                    return;
                }

                dialogContext = value;
                ItemToForm();
            }

        }

        private void ItemToForm()
        {
            var project = dialogContext.Item;
            textBoxName.Text = project.Name;
            textBoxDescription.Text = project.Description;
            checkBoxActive.Checked = project.Active;
            textBoxExternalCode.Text = project.ExternalCode;
            UpdateActions();
        }

        private void ItemFromForm()
        {
            var project = dialogContext.Item;
            project.Name = textBoxName.Text;
            project.Description = WinFormUtilities.TextOrNull(textBoxDescription.Text);
            project.Active = checkBoxActive.Checked;
            project.ExternalCode = WinFormUtilities.TextOrNull(textBoxExternalCode.Text);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ItemFromForm();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }

        private void textBoxName_Validating(object sender, CancelEventArgs e)
        {
            string msg = string.Empty;
            if (textBoxName.TextLength > 0 && this.dialogContext.DataContext.ProjectNameExists(textBoxName.Text))
            {
                msg = "A project with this name already exists";
            }
            warningProvider.SetError(textBoxName, msg);
        }

        private void textBoxExternalCode_Validating(object sender, CancelEventArgs e)
        {
            string msg = string.Empty;
            if (textBoxExternalCode.TextLength > 0 && this.dialogContext.DataContext.ProjectExternalCodeExists(textBoxExternalCode.Text))
            {
                msg = "A project with this external code already exists";
            }
            warningProvider.SetError(textBoxExternalCode, msg);
        }


    }
}
