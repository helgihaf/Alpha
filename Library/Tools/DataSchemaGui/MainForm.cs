using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.Library.Database.Schema;
using Knightrunner.Library.Database.Schema.Verification;
using System.IO;
using Knightrunner.Library.Core;
using Knightrunner.Library.Controls.PageSelection;
using Knightrunner.Library.Database.Schema.Project;
using Knightrunner.Library.Database.Schema.Oracle;
using Knightrunner.Library.Database.Schema.SqlServer;
using Knightrunner.Library.Database.Schema.PetaPoco;

namespace DataSchemaGui
{
    public partial class MainForm : Form, ISchemaTransformationFactory
    {
        private DataSchemaProject project;
        private VerificationContext verificationContext = new VerificationContext();

        public MainForm()
        {
            InitializeComponent();
            verificationContext.MessageAdded += verificationContext_MessageAdded;
            projectView.Initialize(new DetailPageRepository());
        }

        private void verificationContext_MessageAdded(object sender, MessageAddedEventArgs e)
        {
            string line = string.Format(
                "{0,HH:mm:ss} {1} {2}",
                e.Message.DateTime,
                e.Message.Severity,
                e.Message.Text);
            MessageLog.WriteLine(line);
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            ApplySettings(Properties.Settings.Default);
        }


        private void ApplySettings(Properties.Settings settings)
        {
            if (settings.FormWidth > this.MinimumSize.Width)
            {
                this.Width = settings.FormWidth;
            }

            if (settings.FormHeight > this.MinimumSize.Height)
            {
                this.Height = settings.FormHeight;
            }

            if (settings.RecentlyUsedFiles == null)
            {
                settings.RecentlyUsedFiles = new System.Collections.Specialized.StringCollection();
            }
            
            separatorRecentToolStripMenuItem.Visible = settings.RecentlyUsedFiles.Count > 0;
            recentToolStripMenuItem.Visible = settings.RecentlyUsedFiles.Count > 0;
            if (settings.RecentlyUsedFiles.Count > 0)
            {
                foreach (var filePath in settings.RecentlyUsedFiles)
                {
                    var item = new ToolStripMenuItem(filePath, null, RecentlyUsedFileClick);
                    recentToolStripMenuItem.DropDownItems.Add(item);
                }
            }

            projectView.ApplySettings(settings);

        }


        private void RecentlyUsedFileClick(object sender, EventArgs e)
        {
            if (!SaveChanges())
            {
                return;
            }

            OpenFile(((ToolStripMenuItem)sender).Text);
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SaveChanges())
            {
                return;
            }

            if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                OpenFile(openFileDialog.FileName);
            }
        }

        private void OpenFile(string filePath)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                verificationContext.Clear();

                project = new DataSchemaProject();
                project.LoadFrom(filePath, verificationContext, this);

                //dataSchema = new DataSchema();
                //using (StreamReader reader = new StreamReader(filePath))
                //{
                //    dataSchema.LoadDataSchemaFile(reader, verificationContext);
                //}
                //dataSchema.Verify(verificationContext);

                AddRecentlyUsedFile(filePath);

            }
            catch (Exception ex)
            {
                if (!ex.IsCritical())
                {
                    ReportError(string.Format("Error loading file '{0}'.", filePath), ex);
                    return;
                }
                else
                {
                    throw;
                }
            }

            MessageLog.WriteLine("'" + Path.GetFileName(filePath) + "' loaded.");
            projectView.Project = project;

        }

        private void AddRecentlyUsedFile(string filePath)
        {
            var list = Properties.Settings.Default.RecentlyUsedFiles;

            // Make sure the file is the first in the list
            list.Insert(0, filePath);

            // Remove the file if it is already present
            int index = 1;
            while (index < list.Count)
            {
                if (string.Compare(list[index], filePath) == 0)
                {
                    list.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            // Make sure the list does not exceed 10 items
            while (list.Count > 10)
            {
                list.RemoveAt(list.Count - 1);
            }
        }

        private void ReportError(string msg, Exception ex)
        {
            MessageLog.WriteLine(msg);
            if (ex != null)
            {
                MessageLog.WriteLine(ex.Summary(false).ToString());
            }
        }

        private MessageLog MessageLog
        {
            get { return projectView.MessageLog; }
        }


        private bool SaveChanges()
        {
            return true; // for now
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToSettings(Properties.Settings.Default);
            Properties.Settings.Default.Save();
        }

        private void SaveToSettings(Properties.Settings settings)
        {
            bool isMaximized = this.WindowState == FormWindowState.Maximized;
            if (!isMaximized)
            {
                settings.FormWidth = this.Width;
                settings.FormHeight = this.Height;
            }
            projectView.SaveToSettings(settings, isMaximized);
        }



        ISchemaTransformation ISchemaTransformationFactory.Create(string name)
        {
            switch (name)
            {
                case "Oracle":
                    return new OracleSchemaTransformation();
                case "MSSQL":
                    return new SqlServerSchemaTransformation();
                case "PetaPoco":
                    return new PetaPocoTransformation();
                default:
                    throw new ArgumentException("Unknown schema transformation name " + name);
            }
        }
    }
}
