using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Knightrunner.WorkTrack.Database;
using System.Diagnostics;
using Knightrunner.WorkTrack.WinForms;
using Knightrunner.WorkTrack.WinForms.Dialogs;
using Knightrunner.WorkTrack.WinForms.Views;

namespace WorkTrack
{
    public class CustomApplicationContext : ApplicationContext, ISessionContext
    {
        private const int balloonTimeoutMs = 6000;
        private System.ComponentModel.Container components;
        private NotifyIcon notifyIcon;
        private SqlConnection createDatabaseConnection;
        private SqlConnection sqlConnection;

        private ToolStripMenuItem toolStripItemConnect;
        private ToolStripMenuItem toolStripItemCreateDatabase;
        private ToolStripMenuItem toolStripItemTrackWork;
        private ToolStripMenuItem toolStripItemEditWork;
        private ToolStripSeparator toolStripItemSeparator;
        private ToolStripMenuItem toolStripItemExit;
        private ToolStripMenuItem toolStripItemSettings;
        private ToolStripMenuItem toolStripItemProjects;
        private ToolStripMenuItem toolStripItemActivities;
        private ToolStripMenuItem toolStripItemUsers;

        //private TrackWorkSimpleDialog trackWorkSimpleDialog;
        //private TrackWorkDialog trackWorkDialog;
        private ViewHostDialog projectsDialog;
        //private ViewHostDialog activitiesDialog;
        //private ViewHostDialog usersDialog;
        //private ViewHostDialog editWorkDialog;


        public CustomApplicationContext()
        {
            CreateToolStripItems();
            components = new System.ComponentModel.Container();
            notifyIcon = new NotifyIcon(components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = Properties.Resources.ClockProcessing,
                Text = "Work Track",
                Visible = true
            };

            var connectionInfo = SettingsToConnectionInfo(Properties.Settings.Default);

            if (!string.IsNullOrEmpty(connectionInfo.ServerName))
            {
                var failMessage = InitializeConnection(connectionInfo);
                if (failMessage != null)
                {
                    notifyIcon.Icon = Properties.Resources.ClockDisconnected;
                    string msg = "There was an error connecting to the WorkTrack database. " + failMessage;
                    notifyIcon.ShowBalloonTip(balloonTimeoutMs, "WorkTrack", msg, ToolTipIcon.Error);
                }
                else
                {
                    notifyIcon.Icon = Properties.Resources.Clock;
                }
            }
            else
            {
                notifyIcon.Icon = Properties.Resources.ClockDisconnected;
                string msg = "Database connection has not been defined.";
                notifyIcon.ShowBalloonTip(balloonTimeoutMs, "WorkTrack", msg, ToolTipIcon.Warning);
            }

            notifyIcon.ContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(ContextMenuStrip_Opening);

            //notifyIcon.DoubleClick += notifyIcon_DoubleClick;
        }

        private void CreateToolStripItems()
        {
            toolStripItemTrackWork = new ToolStripMenuItem("Track work", null, TrackWorkHandler);
            toolStripItemEditWork = new ToolStripMenuItem("Edit work", null, EditWorkHandler);

            toolStripItemConnect = new ToolStripMenuItem("Set connection", null, ConnectHandler);
            toolStripItemCreateDatabase = new ToolStripMenuItem("Create a new database", null, CreateDatabaseHandler);
            toolStripItemSeparator = new ToolStripSeparator();
            toolStripItemExit = new ToolStripMenuItem("Exit", null, ExitHandler);

            toolStripItemSettings = new ToolStripMenuItem("Settings");
            toolStripItemProjects = new ToolStripMenuItem("Projects", null, SettingsProjectsHandler);
            toolStripItemSettings.DropDownItems.Add(toolStripItemProjects);
            toolStripItemActivities = new ToolStripMenuItem("Activities", null, SettingsActivitiesHandler);
            toolStripItemSettings.DropDownItems.Add(toolStripItemActivities);
            toolStripItemUsers = new ToolStripMenuItem("Users", null, SettingsUsersHandler);
            toolStripItemSettings.DropDownItems.Add(toolStripItemUsers);

        }

        private DatabaseConnectionInfo SettingsToConnectionInfo(Properties.Settings settings)
        {
            var connectionInfo = new DatabaseConnectionInfo
            {
                ServerName = settings.ServerName,
                UseWindowsAuthentication = settings.UseWindowsAuthentication,
                DatabaseName = settings.DatabaseName
            };

            if (!settings.UseWindowsAuthentication)
            {
                connectionInfo.UserName = settings.UserName;
                connectionInfo.EncryptedPassword = settings.Password;
            }

            return connectionInfo;
        }


        private static void SettingsFromConnectionInfo(DatabaseConnectionInfo connectionInfo, Properties.Settings settings)
        {
            settings.ServerName = connectionInfo.ServerName;
            settings.UseWindowsAuthentication = connectionInfo.UseWindowsAuthentication;
            settings.DatabaseName = connectionInfo.DatabaseName;
            if (!connectionInfo.UseWindowsAuthentication)
            {
                settings.UserName = connectionInfo.UserName;
                if (connectionInfo.RememberPassword)
                {
                    settings.Password = connectionInfo.EncryptedPassword;
                }
            }
            else
            {
                settings.UserName = string.Empty;
                settings.Password = string.Empty;
            }
        }


        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var contextMenu = (ContextMenuStrip)sender;
            PopulateContextMenu(contextMenu);
            e.Cancel = false;
        }

        private void PopulateContextMenu(ContextMenuStrip contextMenu)
        {
            contextMenu.Items.Clear();
            if (sqlConnection != null)
            {
                contextMenu.Items.Add(toolStripItemTrackWork);
                contextMenu.Items.Add(toolStripItemEditWork);
                contextMenu.Items.Add(toolStripItemSettings);
            }
            else
            {
                contextMenu.Items.Add(toolStripItemConnect);
                contextMenu.Items.Add(toolStripItemCreateDatabase);
            }
            contextMenu.Items.Add(toolStripItemSeparator);
            contextMenu.Items.Add(toolStripItemExit);
        }


        private void ConnectHandler(object sender, EventArgs e)
        {
            var connectionInfo = SettingsToConnectionInfo(Properties.Settings.Default);
            
            using (DatabaseLoginDialog dialog = new DatabaseLoginDialog())
            {
                dialog.StartPosition = FormStartPosition.CenterScreen;
                dialog.ConnectionInfo = connectionInfo;
                dialog.ValidatingLogin += new EventHandler<ValidatingLoginEventArgs>(dialog_ValidatingLogin);
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                //!!UpdateDatabase("Updating database...");
                notifyIcon.Icon = Properties.Resources.Clock;
            }
        }

        private void dialog_ValidatingLogin(object sender, ValidatingLoginEventArgs e)
        {
            e.FailMessage = InitializeConnection(e.ConnectionInfo);
            if (e.FailMessage == null)
            {
                SettingsFromConnectionInfo(e.ConnectionInfo, Properties.Settings.Default);
                Properties.Settings.Default.Save();
            }
        }


        private void CreateDatabaseHandler(object sender, EventArgs e)
        {
            var connectionInfo = SettingsToConnectionInfo(Properties.Settings.Default);

            using (DatabaseLoginDialog dialog = new DatabaseLoginDialog())
            {
                dialog.Text = "Create Database";
                dialog.StartPosition = FormStartPosition.CenterScreen;
                dialog.ConnectionInfo = connectionInfo;
                dialog.ValidatingLogin += new EventHandler<ValidatingLoginEventArgs>(dialog_ValidatingCreateDatabaseLogin);

                createDatabaseConnection = null;
                try
                {
                    if (dialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    //using (var cmd = createDatabaseConnection.CreateCommand())
                    //{
                    //    cmd.CommandText = "CREATE DATABASE [@database]";
                    //    cmd.Parameters.Add(new SqlParameter("@database", databaseName));
                    //    messageContext = "There was an error creating the database.";
                    //    cmd.ExecuteNonQuery();
                    //}
                }
                finally
                {
                    if (createDatabaseConnection != null)
                    {
                        createDatabaseConnection.Dispose();
                        createDatabaseConnection = null;
                    }
                }
                //??UpdateDatabase("Creating database...");
                //??notifyIcon.Icon = Properties.Resources.Clock;
            }
        }



        private void dialog_ValidatingCreateDatabaseLogin(object sender, ValidatingLoginEventArgs e)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(e.ConnectionInfo.ConnectionString);
            var databaseName = builder.InitialCatalog;
            builder.InitialCatalog = null;
            try
            {
                this.createDatabaseConnection = new SqlConnection(builder.ToString());
                this.createDatabaseConnection.Open();
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException || ex is SqlException)
                {
                    e.FailMessage = "There was an error connecting to the database. " + ex.Message;
                }
                else
                {
                    throw;
                }
            }
        }


        private string InitializeConnection(DatabaseConnectionInfo connectionInfo)
        {
            string result = null;

            try
            {
                SqlConnection connection = new SqlConnection(connectionInfo.ConnectionString);
                connection.Open();
                this.sqlConnection = connection;
                this.UserId = GetUserId();
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException || ex is SqlException)
                {
                    result = "There was an error connecting to the database. " + ex.Message;
                }
                else
                {
                    throw;
                }
            }

            return result;
        }

        private Guid GetUserId()
        {
            // Create or find user
            var windowsAccount = Environment.UserDomainName + "\\" + Environment.UserName;
            using (WorkTrackDataContext dataContext = CreateDataContext())
            {
                var user =
                    (from dataUser in dataContext.Users
                     where dataUser.WindowsAccount == windowsAccount
                     select dataUser).FirstOrDefault();

                if (user == null)
                {
                    user = new User
                    {
                        Id = Guid.NewGuid(),
                        Active = true,
                        WindowsAccount = windowsAccount,
                        FullName = windowsAccount
                    };
                    dataContext.Users.InsertOnSubmit(user);
                    dataContext.SubmitChanges();
                }

                return user.Id;
            }

        }


        private void ExitHandler(object sender, EventArgs e)
        {
            components.Remove(notifyIcon);
            notifyIcon.Dispose();
            Application.Exit();
        }

        private void TrackWorkHandler(object sender, EventArgs e)
        {
            //ShowTrackWorkDialog();
            //ShowTrackWorkSimpleWindow();
        }

        //private void ShowTrackWorkSimpleDialog()
        //{
        //    if (trackWorkSimpleDialog == null)
        //    {
        //        trackWorkSimpleDialog = new TrackWorkSimpleDialog(this);
        //        trackWorkSimpleDialog.StartPosition = FormStartPosition.CenterScreen;
        //    }

        //    trackWorkSimpleDialog.ShowDialog();
        //}

        //private void ShowTrackWorkSimpleWindow()
        //{
        //    if (trackWorkSimpleWindow == null)
        //    {
        //        trackWorkSimpleWindow = new TrackWorkSimpleWindow(this);
        //    }
        //    trackWorkSimpleWindow.Show();
        //}


        //private void ShowTrackWorkDialog()
        //{
        //    if (trackWorkDialog == null)
        //    {
        //        trackWorkDialog = new TrackWorkDialog(this);
        //        trackWorkDialog.StartPosition = FormStartPosition.CenterScreen;
        //    }

        //    if (!trackWorkDialog.Visible)
        //    {
        //        trackWorkDialog.Show();
        //    }
        //    else
        //    {
        //        trackWorkDialog.BringToFront();
        //    }
        //}


        private void ShowView(ref ViewHostDialog viewHostDialog, Type viewType)
        {
            if (viewHostDialog == null)
            {
                var view = Activator.CreateInstance(viewType) as IView;
                view.SessionContext = this;
                viewHostDialog = new ViewHostDialog();
                viewHostDialog.Controls.Add(view as Control);
                viewHostDialog.Dock = DockStyle.Fill;
                viewHostDialog.Text = "Projects";
            }

            if (!viewHostDialog.Visible)
            {
                var projectsView = (ProjectsView)viewHostDialog.Controls[0];
                projectsView.LoadData();
                viewHostDialog.Show();
            }
            else
            {
                viewHostDialog.BringToFront();
            }
        }


        private void SettingsProjectsHandler(object sender, EventArgs e)
        {
            ShowView(ref projectsDialog, typeof(ProjectsView));
        }
        
        private void SettingsActivitiesHandler(object sender, EventArgs e)
        {
        //    if (activitiesDialog == null)
        //    {
        //        activitiesDialog = new ViewHostDialog();
        //        var activitiesView = new ActivitiesView();
        //        activitiesDialog.Controls.Add(activitiesView);
        //        activitiesView.Dock = DockStyle.Fill;
        //        activitiesDialog.Text = "Activities";
        //    }

        //    if (!activitiesDialog.Visible)
        //    {
        //        var activitiesView = (ActivitiesView)activitiesDialog.Controls[0];
        //        activitiesView.LoadData(this);
        //        activitiesDialog.Show();
        //    }
        //    else
        //    {
        //        activitiesDialog.BringToFront();
        //    }
        }

        private void SettingsUsersHandler(object sender, EventArgs e)
        {
            //if (usersDialog == null)
            //{
            //    usersDialog = new ViewHostDialog();
            //    var usersView = new UsersView();
            //    usersDialog.Controls.Add(usersView);
            //    usersView.Dock = DockStyle.Fill;
            //    usersDialog.Text = "Users";
            //}

            //if (!usersDialog.Visible)
            //{
            //    var usersView = (UsersView)usersDialog.Controls[0];
            //    usersView.LoadData(this);
            //    usersDialog.Show();
            //}
            //else
            //{
            //    usersDialog.BringToFront();
            //}
        }


        private void EditWorkHandler(object sender, EventArgs e)
        {
            //ShowEditWorkDialog();
        }

        //private void ShowEditWorkDialog()
        //{
        //    if (editWorkDialog == null)
        //    {
        //        editWorkDialog = new ViewHostDialog();
        //        var editWorkView = new WorkEntriesEditView();
        //        editWorkDialog.Controls.Add(editWorkView);
        //        editWorkView.Dock = DockStyle.Fill;
        //        editWorkDialog.Text = "Users";
        //    }

        //    if (!editWorkDialog.Visible)
        //    {
        //        var editWorkView = (WorkEntriesEditView)editWorkDialog.Controls[0];
        //        editWorkView.LoadData(this);
        //        editWorkDialog.Show();
        //    }
        //    else
        //    {
        //        editWorkDialog.BringToFront();
        //    }
        //}





        public Guid UserId { get; private set; }

        public Knightrunner.WorkTrack.Database.WorkTrackDataContext CreateDataContext()
        {
            return new Knightrunner.WorkTrack.Database.WorkTrackDataContext(sqlConnection);
        }
    }
}
