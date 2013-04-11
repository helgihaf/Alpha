using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Knightrunner.WorkTrack.WinForms.Dialogs
{
    public partial class ConnectionWizard : DialogBase
    {
        public ConnectionWizard()
        {
            InitializeComponent();
        }

        private void ConnectionWizard_Load(object sender, EventArgs e)
        {
            PrepareWelcome();
        }


        private void PrepareWelcome()
        {
            buttonBack.Visible = false;
            buttonNext.Visible = true;
            buttonFinish.Visible = false;

            databaseLoginControl.Visible = false;
            labelTitle.Text = "Welcome to Knightrunner WorkTrack";
        }

        private void PrepareDatabaseLogin()
        {
            buttonBack.Visible = true;
            buttonNext.Visible = false;
            buttonFinish.Visible = true;
            buttonFinish.Enabled = false;

            databaseLoginControl.Visible = true;
            if (radioButtonNewDatabase.Checked)
            {
                labelTitle.Text = "Create new database";
            }
            else
            {
                labelTitle.Text = "Connect to existing database";
            }
        }

        private void databaseLoginControl_ValidationStatusChanged(object sender, Controls.ValidationStatusEventArgs e)
        {
            buttonFinish.Enabled = e.CanValidate;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            PrepareWelcome();
        }


        private void buttonNext_Click(object sender, EventArgs e)
        {
            PrepareDatabaseLogin();
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            // ...
        }


    }
}
