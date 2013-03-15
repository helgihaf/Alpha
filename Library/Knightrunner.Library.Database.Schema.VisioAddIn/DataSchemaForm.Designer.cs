namespace Knightrunner.Library.Database.Schema.VisioAddIn
{
    partial class DataSchemaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataSchemaForm));
            this.comboBoxModelFilePath = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDataTypesFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDataSchemaFilePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSqlScriptFilePath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxLinqToSqlFilePath = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buttonClose = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBoxInput.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxModelFilePath
            // 
            resources.ApplyResources(this.comboBoxModelFilePath, "comboBoxModelFilePath");
            this.comboBoxModelFilePath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModelFilePath.FormattingEnabled = true;
            this.comboBoxModelFilePath.Name = "comboBoxModelFilePath";
            this.comboBoxModelFilePath.SelectedIndexChanged += new System.EventHandler(this.comboBoxModelFilePath_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxDataTypesFilePath
            // 
            resources.ApplyResources(this.textBoxDataTypesFilePath, "textBoxDataTypesFilePath");
            this.textBoxDataTypesFilePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxDataTypesFilePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.textBoxDataTypesFilePath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Knightrunner.Library.Database.Schema.VisioAddIn.Properties.Settings.Default, "DataTypesFilePath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxDataTypesFilePath.Name = "textBoxDataTypesFilePath";
            this.textBoxDataTypesFilePath.Text = global::Knightrunner.Library.Database.Schema.VisioAddIn.Properties.Settings.Default.DataTypesFilePath;
            this.textBoxDataTypesFilePath.TextChanged += new System.EventHandler(this.textBoxDataTypesFilePath_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxDataSchemaFilePath
            // 
            resources.ApplyResources(this.textBoxDataSchemaFilePath, "textBoxDataSchemaFilePath");
            this.textBoxDataSchemaFilePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxDataSchemaFilePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.textBoxDataSchemaFilePath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Knightrunner.Library.Database.Schema.VisioAddIn.Properties.Settings.Default, "DataSchemaFilePath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxDataSchemaFilePath.Name = "textBoxDataSchemaFilePath";
            this.textBoxDataSchemaFilePath.Text = global::Knightrunner.Library.Database.Schema.VisioAddIn.Properties.Settings.Default.DataSchemaFilePath;
            this.textBoxDataSchemaFilePath.TextChanged += new System.EventHandler(this.textBoxDataSchemaFilePath_TextChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBoxSqlScriptFilePath
            // 
            resources.ApplyResources(this.textBoxSqlScriptFilePath, "textBoxSqlScriptFilePath");
            this.textBoxSqlScriptFilePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxSqlScriptFilePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.textBoxSqlScriptFilePath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Knightrunner.Library.Database.Schema.VisioAddIn.Properties.Settings.Default, "SqlScriptFilePath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxSqlScriptFilePath.Name = "textBoxSqlScriptFilePath";
            this.textBoxSqlScriptFilePath.Text = global::Knightrunner.Library.Database.Schema.VisioAddIn.Properties.Settings.Default.SqlScriptFilePath;
            this.textBoxSqlScriptFilePath.TextChanged += new System.EventHandler(this.textBoxSqlScriptFilePath_TextChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxLinqToSqlFilePath
            // 
            resources.ApplyResources(this.textBoxLinqToSqlFilePath, "textBoxLinqToSqlFilePath");
            this.textBoxLinqToSqlFilePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxLinqToSqlFilePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.textBoxLinqToSqlFilePath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Knightrunner.Library.Database.Schema.VisioAddIn.Properties.Settings.Default, "LinqToSqlFilePath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxLinqToSqlFilePath.Name = "textBoxLinqToSqlFilePath";
            this.textBoxLinqToSqlFilePath.Text = global::Knightrunner.Library.Database.Schema.VisioAddIn.Properties.Settings.Default.LinqToSqlFilePath;
            this.textBoxLinqToSqlFilePath.TextChanged += new System.EventHandler(this.textBoxLinq_TextChanged);
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBoxInput
            // 
            resources.ApplyResources(this.groupBoxInput, "groupBoxInput");
            this.groupBoxInput.Controls.Add(this.comboBoxModelFilePath);
            this.groupBoxInput.Controls.Add(this.label1);
            this.groupBoxInput.Controls.Add(this.textBoxDataTypesFilePath);
            this.groupBoxInput.Controls.Add(this.label2);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.TabStop = false;
            // 
            // groupBoxOutput
            // 
            resources.ApplyResources(this.groupBoxOutput, "groupBoxOutput");
            this.groupBoxOutput.Controls.Add(this.textBoxSqlScriptFilePath);
            this.groupBoxOutput.Controls.Add(this.textBoxDataSchemaFilePath);
            this.groupBoxOutput.Controls.Add(this.label5);
            this.groupBoxOutput.Controls.Add(this.label3);
            this.groupBoxOutput.Controls.Add(this.textBoxLinqToSqlFilePath);
            this.groupBoxOutput.Controls.Add(this.label4);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.TabStop = false;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.textBoxLog);
            this.groupBox3.Controls.Add(this.progressBar);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // textBoxLog
            // 
            resources.ApplyResources(this.textBoxLog, "textBoxLog");
            this.textBoxLog.BackColor = System.Drawing.Color.White;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // DataSchemaForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.groupBoxOutput);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonClose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataSchemaForm";
            this.Load += new System.EventHandler(this.DataSchemaForm_Load);
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.groupBoxOutput.ResumeLayout(false);
            this.groupBoxOutput.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxModelFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDataTypesFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDataSchemaFilePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSqlScriptFilePath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxLinqToSqlFilePath;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button buttonClose;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}