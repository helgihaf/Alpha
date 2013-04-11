using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
using System.IO;

namespace WorkTrackLite
{
    public partial class SettingsForm : Form
    {
        private class LanguageSelection
        {
            public CultureInfo CultureInfo { get; set; }
            public override string ToString()
            {
                return CultureInfo.Parent.NativeName;
            }
        }

        public SettingsForm()
        {
            InitializeComponent();

            var executingAssembly = Assembly.GetExecutingAssembly();

            labelVersion.Text = string.Format(Properties.Resources.VersionFormat, executingAssembly.GetName().Version.ToString());

            comboBoxLanguage.Items.Add(new LanguageSelection { CultureInfo = CultureInfo.GetCultureInfo("en-US") });
            comboBoxLanguage.Items.Add(new LanguageSelection { CultureInfo = CultureInfo.GetCultureInfo("is-IS") });
            comboBoxLanguage.SelectedIndex = 0;
        }

        internal Properties.Settings Settings { get; set; }

        public event EventHandler<EventArgs> Applied;

        private void SettingsDialog_Shown(object sender, EventArgs e)
        {
            this.Settings = Properties.Settings.Default;
            ItemToForm();
            UpdateActions();
        }

        private void ItemToForm()
        {
            checkBoxNotifications.Checked = this.Settings.EnableNotifications;
            numericUpDownMinutes.Value = this.Settings.NotificationIntervalMs / 60000;
            Language = this.Settings.Language;
        }


        private void ItemFromForm()
        {
            this.Settings.EnableNotifications = checkBoxNotifications.Checked;
            this.Settings.NotificationIntervalMs = Convert.ToInt32(numericUpDownMinutes.Value) * 60000;
            this.Settings.Language = Language;
        }

        public string DataFilePath
        {
            get { return textBoxDataFile.Text; }
            set { textBoxDataFile.Text = value; }
        }

        private string Language
        {
            get { return ((LanguageSelection)comboBoxLanguage.SelectedItem).CultureInfo.Name; }
            set
            {
                for (int i = 0; i < comboBoxLanguage.Items.Count; i++)
                {
                    if (((LanguageSelection)comboBoxLanguage.Items[i]).CultureInfo.Name == value)
                    {
                        comboBoxLanguage.SelectedIndex = i;
                        break;
                    }
                }
            }
        }


        private void checkBoxNotifications_CheckedChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }

        private void UpdateActions()
        {
            numericUpDownMinutes.Enabled = checkBoxNotifications.Checked;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            ItemFromForm();
            this.Hide();
            OnApplied();
        }

        
        private void OnApplied()
        {
            if (Applied != null)
            {
                Applied(this, EventArgs.Empty);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

    }
}
