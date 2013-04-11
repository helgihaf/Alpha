using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

namespace WorkTrackLite
{
    public class CustomApplicationContext : ApplicationContext
    {
        private const int balloonTimeoutMs = 6000;
        private System.ComponentModel.Container components;
        private NotifyIcon notifyIcon;
        private Timer timer;
        private bool timerHasTicked;

        private Properties.Settings settings;
        private DataStore dataStore;

        private SettingsForm settingsForm;
        private TrackWorkForm trackWorkForm;
        private WorkOverviewForm workOverviewForm;

        private WorkEntry[] workEntries;

        private string lastLanguage;

        public CustomApplicationContext()
        {
            components = new System.ComponentModel.Container();
            notifyIcon = new NotifyIcon(components)
            {
                ContextMenuStrip = CreateContextMenuStrip(),
                Icon = Properties.Resources.Clock,
                Text = "Work Track Lite",
                Visible = true
            };
            //notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            notifyIcon.BalloonTipClicked += new EventHandler(notifyIcon_BalloonTipClicked);

            settings = Properties.Settings.Default;
            dataStore = new DataStore();
            
            timer = new Timer(components);
            timer.Tick += new EventHandler(timer_Tick);
            ConfigureInitialTimer();
        }



        private ContextMenuStrip CreateContextMenuStrip()
        {
            var contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add(new ToolStripMenuItem(Properties.Resources.TrackWorkMenuItem, null, TrackWorkHandler));
            contextMenuStrip.Items.Add(new ToolStripMenuItem(Properties.Resources.WorkOverviewMenuItem, null, WorkOverviewHandler));
            contextMenuStrip.Items.Add(new ToolStripMenuItem(Properties.Resources.SettingsMenuItem, null, SettingsHandler));
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add(new ToolStripMenuItem(Properties.Resources.ExitMenuItem, null, ExitHandler));

            return contextMenuStrip;
        }


        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            ShowTrackWorkForm();
        }




        private void ExitHandler(object sender, EventArgs e)
        {
            components.Remove(notifyIcon);
            notifyIcon.Dispose();
            Application.Exit();
        }

        private void TrackWorkHandler(object sender, EventArgs e)
        {
            ShowTrackWorkForm();
        }





        private void WorkOverviewHandler(object sender, EventArgs e)
        {
            ShowWorkOverviewForm();
        }


        private void SettingsHandler(object sender, EventArgs e)
        {
            ShowSettingsForm();
        }


        
        private void ShowTrackWorkForm()
        {
            if (trackWorkForm == null)
            {
                trackWorkForm = new TrackWorkForm();
                trackWorkForm.Applied += new EventHandler<TrackWorkEventArgs>(trackWorkForm_Applied);
            }

            if (!trackWorkForm.Visible)
            {
                trackWorkForm.Show();
            }
            trackWorkForm.BringToFront();
            trackWorkForm.Focus();
        }

        private void trackWorkForm_Applied(object sender, TrackWorkEventArgs e)
        {
            dataStore.SaveWorkEntry(e.WorkEntry);
            // Invalidate previously loaded work entries
            workEntries = null;
            // Notify overview window, if visible, of new content
            NotifyOverviewForm();
        }


        private void ShowWorkOverviewForm()
        {
            if (workOverviewForm == null)
            {
                workOverviewForm = new WorkOverviewForm();
            }

            if (workEntries == null)
            {
                workEntries = dataStore.GetWorkEntries();
            }

            workOverviewForm.WorkEntries = workEntries;
            if (!workOverviewForm.Visible)
            {
                workOverviewForm.Show();
            }
            workOverviewForm.BringToFront();
            workOverviewForm.Focus();
        }
        

        private void ShowSettingsForm()
        {
            if (settingsForm == null)
            {
                settingsForm = new SettingsForm();
                settingsForm.DataFilePath = dataStore.DataFilePath;
                settingsForm.Settings = settings;
                settingsForm.Applied += new EventHandler<EventArgs>(settingsDialog_Applied);
            }

            lastLanguage = settings.Language;
            if (!settingsForm.Visible)
            {
                settingsForm.Show();
            }
            settingsForm.BringToFront();
            settingsForm.Focus();
        }


        private void settingsDialog_Applied(object sender, EventArgs e)
        {
            bool needRestart = false;
            needRestart = settings.Language != lastLanguage;

            settings.Save();

            if (needRestart)
            {
                MessageBox.Show(Properties.Resources.RestartNeededBySettings, Properties.Resources.ApplicationTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ConfigureTimer();
        }

        private void ConfigureInitialTimer()
        {
            ConfigureTimer(settings.InitialNotificationIntervalMs);
        }


        private void ConfigureTimer()
        {
            ConfigureTimer(settings.NotificationIntervalMs);
        }


        private void ConfigureTimer(int intervalMs)
        {
            timer.Enabled = false;
            if (settings.EnableNotifications)
            {
                timer.Interval = intervalMs;
                timer.Enabled = settings.EnableNotifications;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            notifyIcon.ShowBalloonTip(settings.BalloonTipTimeoutMs, Properties.Resources.ApplicationTitle, Properties.Resources.BallonTipText, ToolTipIcon.Info);
            if (!timerHasTicked)
            {
                timerHasTicked = true;
                ConfigureTimer();
            }
        }

        private void NotifyOverviewForm()
        {
            if (workOverviewForm != null && workOverviewForm.Visible)
            {
                workOverviewForm.WorkEntries = dataStore.GetWorkEntries();
            }
        }



    }
}
