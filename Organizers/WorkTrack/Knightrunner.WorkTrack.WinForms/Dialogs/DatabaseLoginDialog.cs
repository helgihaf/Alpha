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
    public partial class DatabaseLoginDialog : Form
    {
        private DatabaseConnectionInfo connectionInfo = new DatabaseConnectionInfo();

        public DatabaseLoginDialog()
        {
            InitializeComponent();
            comboBoxAuthentication.SelectedIndex = 0;
        }

        public DatabaseConnectionInfo ConnectionInfo
        {
            get { return connectionInfo; }
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

                ConnectionInfoToForm();
            }
        }


        public event EventHandler<ValidatingLoginEventArgs> ValidatingLogin;

        private void UpdateActions()
        {
            var windowsAuthentication = comboBoxAuthentication.SelectedIndex == 0;

            buttonOK.Enabled =
                textBoxServerName.TextLength > 0 &&
                textBoxDatabase.Text.Length > 0 &&
                (
                    windowsAuthentication ||
                    textBoxUserName.TextLength > 0
                );
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

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ConnectionInfoFromForm();
            var failMessage  = OnValidatingLogin();
            if (failMessage == null)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show(this, failMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string OnValidatingLogin()
        {
            string failMessage = null;
            if (ValidatingLogin != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                var eventArgs = new ValidatingLoginEventArgs { ConnectionInfo = this.connectionInfo };
                ValidatingLogin(this, eventArgs);
                failMessage = eventArgs.FailMessage;
            }

            return failMessage;
        }


    }


    public class ValidatingLoginEventArgs : EventArgs
    {
        public DatabaseConnectionInfo ConnectionInfo { get; set; }
        public string FailMessage { get; set; }
    }
}
