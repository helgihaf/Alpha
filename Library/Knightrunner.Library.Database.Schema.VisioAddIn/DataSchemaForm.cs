using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Knightrunner.Library.Database.Schema.VisioAddIn
{
    public partial class DataSchemaForm : Form
    {
        private readonly ProgressBarStyle defaultProgressBarStyle;
        private ModelTransformer transformer;
    
        public DataSchemaForm()
        {
            InitializeComponent();
            defaultProgressBarStyle = progressBar.Style;
        }

        public string ModelFilePath
        {
            get { return (string)comboBoxModelFilePath.SelectedItem; }
        }

        public string DataTypesFilePath
        {
            get { return textBoxDataTypesFilePath.Text; }
        }

        public string DataSchemaFilePath
        {
            get { return textBoxDataSchemaFilePath.Text; }
        }

        public string SqlScriptFilePath
        {
            get { return textBoxSqlScriptFilePath.Text; }
        }

        public string LinqToSqlFilePath
        {
            get { return textBoxLinqToSqlFilePath.Text; }
        }

        private void DataSchemaForm_Load(object sender, EventArgs e)
        {
            comboBoxModelFilePath.Items.Clear();
            string[] modelNames = ModelTransformer.GetModels();
            comboBoxModelFilePath.Items.AddRange(modelNames);
            if (modelNames.Length == 1)
            {
                comboBoxModelFilePath.SelectedItem = modelNames[0];
                comboBoxModelFilePath.Enabled = false;
            }

            buttonOk.Visible = true;
            buttonCancel.Visible = true;
            buttonClose.Visible = false;
            groupBoxInput.Enabled = true;
            groupBoxOutput.Enabled = true;

            UpdateActions();
        }

        private void UpdateActions()
        {
            buttonOk.Enabled =
                comboBoxModelFilePath.SelectedItem != null &&
                textBoxDataTypesFilePath.TextLength > 0 &&
                textBoxDataSchemaFilePath.TextLength > 0 &&
                textBoxSqlScriptFilePath.TextLength > 0 &&
                textBoxLinqToSqlFilePath.TextLength > 0;
        }

        private void comboBoxModelFilePath_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }

        private void textBoxDataTypesFilePath_TextChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }

        private void textBoxDataSchemaFilePath_TextChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }

        private void textBoxSqlScriptFilePath_TextChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }

        private void textBoxLinq_TextChanged(object sender, EventArgs e)
        {
            UpdateActions();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            StartGenerate();
        }

        private void StartGenerate()
        {
            buttonOk.Enabled = false;
            Cursor = Cursors.AppStarting;
            groupBoxInput.Enabled = false;
            groupBoxOutput.Enabled = false;
            progressBar.Style = ProgressBarStyle.Marquee;

            transformer = new ModelTransformer();
            transformer.ModelFilePath = this.ModelFilePath;
            transformer.DataTypesFilePath = this.DataTypesFilePath;
            transformer.DataSchemaFilePath = this.DataSchemaFilePath;
            transformer.SqlScriptFilePath = this.SqlScriptFilePath;
            transformer.LinqToSqlFilePath = this.LinqToSqlFilePath;
            transformer.StartTransform(backgroundWorker);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
                buttonCancel.Enabled = false;
            }
            else
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                LogException(e.Error);
            }
            else if (e.Cancelled)
            {
                LogLine("Operation cancelled by user");
            }
            else
            {
                LogLine("Operation completed");
            }

            buttonOk.Visible = false;
            buttonCancel.Visible = false;
            buttonClose.Visible = true;
            progressBar.Style = defaultProgressBarStyle;
            progressBar.Value = progressBar.Maximum;
            Cursor = Cursors.Default;
        }

        private void LogException(Exception exception)
        {
            string indent = string.Empty;
            while (exception != null)
            {
                LogLine(indent + "Error: " + exception.GetType().FullName + ":");
                LogLine(indent + exception.Message);
                exception = exception.InnerException;
                indent += "    ";
            }
        }

        private void LogLine(string line)
        {
            textBoxLog.AppendText(line + Environment.NewLine);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string msg = e.UserState as string;
            if (msg != null)
            {
                LogLine(msg);
            }

            if (e.ProgressPercentage > 0)
            {
                if (progressBar.Style != defaultProgressBarStyle)
                {
                    progressBar.Style = defaultProgressBarStyle;
                }
                progressBar.Value = e.ProgressPercentage;
            }
        }
    }
}
