using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Knightrunner.WorkTrack.WinForms.Controls
{
    public partial class DatabaseLoginControl : UserControl
    {
        private DatabaseConnectionInfo connectionInfo = new DatabaseConnectionInfo();
        private bool? lastValidationStatus;

        public DatabaseLoginControl()
        {
            InitializeComponent();
            comboBoxAuthentication.SelectedIndex = 0;
        }

        
        public DatabaseConnectionInfo ConnectionInfo
        {
            get
            {
                ConnectionInfoFromForm();
                return connectionInfo;
            }
            set
            {
                if (value != null)
                {
                    connectionInfo = value;
                }
                else
                {
                    connectionInfo = new DatabaseConnectionInfo();
                }
                lastValidationStatus = null;
                ConnectionInfoToForm();
            }
        }


        public event EventHandler<ValidationStatusEventArgs> ValidationStatusChanged;

        private void UpdateActions()
        {
            var windowsAuthentication = comboBoxAuthentication.SelectedIndex == 0;

            var validationStatus =
                textBoxServerName.TextLength > 0 &&
                textBoxDatabase.Text.Length > 0 &&
                (
                    windowsAuthentication ||
                    textBoxUserName.TextLength > 0
                );
            OnValidationStatusChanged(validationStatus);
            textBoxUserName.Enabled = !windowsAuthentication;
            textBoxPassword.Enabled = !windowsAuthentication;
            checkBoxRembemberPassword.Enabled = !windowsAuthentication;
        }


        private void ConnectionInfoToForm()
        {
            textBoxServerName.Text = connectionInfo.ServerName;
            comboBoxAuthentication.SelectedIndex = connectionInfo.UseWindowsAuthentication ? 0 : 1;
            SetUserNameAndPassword(connectionInfo.UseWindowsAuthentication);
            textBoxDatabase.Text = connectionInfo.DatabaseName;
        }

        private void SetUserNameAndPassword(bool useWindowsAuthentication)
        {
            if (useWindowsAuthentication)
            {
                textBoxUserName.Text = Environment.UserDomainName + "\\" + Environment.UserName;
                textBoxPassword.Clear();
                checkBoxRembemberPassword.Checked = false;
            }
            else
            {
                textBoxUserName.Text = connectionInfo.UserName;
                textBoxPassword.Text = connectionInfo.Password;
                if (textBoxPassword.TextLength > 0)
                {
                    checkBoxRembemberPassword.Checked = true;
                }
            }
        }


        private void ConnectionInfoFromForm()
        {
            connectionInfo.ServerName = textBoxServerName.Text;
            connectionInfo.UseWindowsAuthentication = comboBoxAuthentication.SelectedIndex == 0;
            if (connectionInfo.UseWindowsAuthentication)
            {
                connectionInfo.UserName = null;
                connectionInfo.Password = null;
            }
            else
            {
                connectionInfo.UserName = textBoxUserName.Text;
                connectionInfo.Password = textBoxPassword.Text;
            }
            connectionInfo.RememberPassword = checkBoxRembemberPassword.Checked;
            connectionInfo.DatabaseName = textBoxDatabase.Text;
        }

        private void textBoxServerName_TextChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }

        private void comboBoxAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetUserNameAndPassword(comboBoxAuthentication.SelectedIndex == 0);
            UpdateActions();
        }

        private void textBoxUserName_TextChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }

        private void textBoxDatabase_TextChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }


        private void OnValidationStatusChanged(bool validationStatus)
        {
            if (lastValidationStatus == null || lastValidationStatus != validationStatus)
            {
                lastValidationStatus = validationStatus;
                if (ValidationStatusChanged != null)
                {
                    ValidationStatusChanged(this, new ValidationStatusEventArgs { CanValidate = validationStatus });
                }
            }
        }

    }


    public class ValidationStatusEventArgs : EventArgs
    {
        public bool CanValidate { get; set; }
    }

}
