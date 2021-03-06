﻿namespace SimpleSearch
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			SimpleSearch.Properties.Settings settings1 = new SimpleSearch.Properties.Settings();
			SimpleSearch.Properties.Settings settings2 = new SimpleSearch.Properties.Settings();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.textBoxDirPath = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.labelUsingMultiText = new System.Windows.Forms.Label();
			this.buttonTextOptions = new System.Windows.Forms.Button();
			this.comboBoxDirectory = new System.Windows.Forms.ComboBox();
			this.textBoxText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxFileName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonSearch = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.listViewResults = new System.Windows.Forms.ListView();
			this.columnHeaderFileName = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderDirectory = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderModifyDate = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderSearchTextHits = new System.Windows.Forms.ColumnHeader();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.opendirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openInNotepadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.copyPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyEntirelineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.checkBoxShowPreview = new System.Windows.Forms.CheckBox();
			this.richTextBoxPreview = new System.Windows.Forms.RichTextBox();
			this.panel1.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.textBoxDirPath);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.labelUsingMultiText);
			this.panel1.Controls.Add(this.buttonTextOptions);
			this.panel1.Controls.Add(this.comboBoxDirectory);
			this.panel1.Controls.Add(this.textBoxText);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.textBoxFileName);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.buttonBrowse);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.buttonSearch);
			this.panel1.Controls.Add(this.buttonCancel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(761, 125);
			this.panel1.TabIndex = 0;
			// 
			// textBoxDirPath
			// 
			settings1.DirectoryItems = null;
			settings1.FileName = "";
			settings1.MultiText = false;
			settings1.SearchDirectory = "";
			settings1.SeperatorChar = ';';
			settings1.SettingsKey = "";
			settings1.Text = "";
			this.textBoxDirPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", settings1, "FileName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBoxDirPath.Location = new System.Drawing.Point(12, 82);
			this.textBoxDirPath.Name = "textBoxDirPath";
			this.textBoxDirPath.Size = new System.Drawing.Size(195, 20);
			this.textBoxDirPath.TabIndex = 2;
			this.textBoxDirPath.Text = settings1.FileName;
			this.toolTip.SetToolTip(this.textBoxDirPath, "File name or a part of file name. Use * or ? to specify wildcards.");
			this.textBoxDirPath.TextChanged += new System.EventHandler(this.TextChangedHandler);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 67);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(182, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Directory path with optional wildcards";
			// 
			// labelUsingMultiText
			// 
			this.labelUsingMultiText.AutoSize = true;
			this.labelUsingMultiText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.labelUsingMultiText.Location = new System.Drawing.Point(419, 106);
			this.labelUsingMultiText.Name = "labelUsingMultiText";
			this.labelUsingMultiText.Size = new System.Drawing.Size(35, 13);
			this.labelUsingMultiText.TabIndex = 11;
			this.labelUsingMultiText.Text = "label4";
			this.labelUsingMultiText.Visible = false;
			// 
			// buttonTextOptions
			// 
			this.buttonTextOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonTextOptions.Location = new System.Drawing.Point(644, 81);
			this.buttonTextOptions.Name = "buttonTextOptions";
			this.buttonTextOptions.Size = new System.Drawing.Size(24, 23);
			this.buttonTextOptions.TabIndex = 5;
			this.buttonTextOptions.Text = "...";
			this.buttonTextOptions.UseVisualStyleBackColor = true;
			this.buttonTextOptions.Click += new System.EventHandler(this.buttonTextOptions_Click);
			// 
			// comboBoxDirectory
			// 
			this.comboBoxDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxDirectory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.comboBoxDirectory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.comboBoxDirectory.DropDownHeight = 150;
			this.comboBoxDirectory.FormattingEnabled = true;
			this.comboBoxDirectory.IntegralHeight = false;
			this.comboBoxDirectory.Location = new System.Drawing.Point(15, 25);
			this.comboBoxDirectory.Name = "comboBoxDirectory";
			this.comboBoxDirectory.Size = new System.Drawing.Size(653, 21);
			this.comboBoxDirectory.TabIndex = 0;
			this.comboBoxDirectory.TextChanged += new System.EventHandler(this.TextChangedHandler);
			// 
			// textBoxText
			// 
			this.textBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			settings2.DirectoryItems = null;
			settings2.FileName = "";
			settings2.MultiText = false;
			settings2.SearchDirectory = "";
			settings2.SeperatorChar = ';';
			settings2.SettingsKey = "";
			settings2.Text = "";
			this.textBoxText.DataBindings.Add(new System.Windows.Forms.Binding("Text", settings2, "Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBoxText.Location = new System.Drawing.Point(422, 83);
			this.textBoxText.Name = "textBoxText";
			this.textBoxText.Size = new System.Drawing.Size(216, 20);
			this.textBoxText.TabIndex = 4;
			this.textBoxText.Text = settings2.Text;
			this.toolTip.SetToolTip(this.textBoxText, "Text to search for, case is ignored.");
			this.textBoxText.TextChanged += new System.EventHandler(this.TextChangedHandler);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(419, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(28, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Text";
			// 
			// textBoxFileName
			// 
			this.textBoxFileName.DataBindings.Add(new System.Windows.Forms.Binding("Text", settings2, "FileName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBoxFileName.Location = new System.Drawing.Point(213, 82);
			this.textBoxFileName.Name = "textBoxFileName";
			this.textBoxFileName.Size = new System.Drawing.Size(203, 20);
			this.textBoxFileName.TabIndex = 3;
			this.textBoxFileName.Text = settings2.FileName;
			this.toolTip.SetToolTip(this.textBoxFileName, "File name or a part of file name. Use * or ? to specify wildcards.");
			this.textBoxFileName.TextChanged += new System.EventHandler(this.TextChangedHandler);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(210, 66);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(161, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "File name with optional wildcards";
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowse.Location = new System.Drawing.Point(674, 23);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowse.TabIndex = 1;
			this.buttonBrowse.Text = "&Browse";
			this.toolTip.SetToolTip(this.buttonBrowse, "Browse for a directory to search");
			this.buttonBrowse.UseVisualStyleBackColor = true;
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Directory";
			// 
			// buttonSearch
			// 
			this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSearch.Location = new System.Drawing.Point(674, 81);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(75, 23);
			this.buttonSearch.TabIndex = 6;
			this.buttonSearch.Text = "&Search";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(674, 80);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 424);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(761, 22);
			this.statusStrip.TabIndex = 2;
			this.statusStrip.Text = "statusStrip1";
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(118, 17);
			this.toolStripStatusLabel.Text = "toolStripStatusLabel1";
			// 
			// listViewResults
			// 
			this.listViewResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFileName,
            this.columnHeaderDirectory,
            this.columnHeaderModifyDate,
            this.columnHeaderSearchTextHits});
			this.listViewResults.ContextMenuStrip = this.contextMenuStrip;
			this.listViewResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewResults.FullRowSelect = true;
			this.listViewResults.Location = new System.Drawing.Point(0, 0);
			this.listViewResults.Name = "listViewResults";
			this.listViewResults.Size = new System.Drawing.Size(761, 132);
			this.listViewResults.TabIndex = 0;
			this.listViewResults.UseCompatibleStateImageBehavior = false;
			this.listViewResults.View = System.Windows.Forms.View.Details;
			this.listViewResults.SelectedIndexChanged += new System.EventHandler(this.listViewResults_SelectedIndexChanged);
			this.listViewResults.DoubleClick += new System.EventHandler(this.listViewResults_DoubleClick);
			this.listViewResults.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewResults_ColumnClick);
			this.listViewResults.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listViewResults_ItemDrag);
			// 
			// columnHeaderFileName
			// 
			this.columnHeaderFileName.Text = "File Name";
			this.columnHeaderFileName.Width = 200;
			// 
			// columnHeaderDirectory
			// 
			this.columnHeaderDirectory.Text = "Directory";
			this.columnHeaderDirectory.Width = 400;
			// 
			// columnHeaderModifyDate
			// 
			this.columnHeaderModifyDate.Text = "Modify Date";
			this.columnHeaderModifyDate.Width = 119;
			// 
			// columnHeaderSearchTextHits
			// 
			this.columnHeaderSearchTextHits.Text = "Search Text Hits";
			this.columnHeaderSearchTextHits.Width = 200;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opendirectoryToolStripMenuItem,
            this.openfileToolStripMenuItem,
            this.openInNotepadToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copyPathToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.copyEntirelineToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(172, 142);
			// 
			// opendirectoryToolStripMenuItem
			// 
			this.opendirectoryToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.opendirectoryToolStripMenuItem.Name = "opendirectoryToolStripMenuItem";
			this.opendirectoryToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.opendirectoryToolStripMenuItem.Text = "Open &directory";
			this.opendirectoryToolStripMenuItem.Click += new System.EventHandler(this.opendirectoryToolStripMenuItem_Click);
			// 
			// openfileToolStripMenuItem
			// 
			this.openfileToolStripMenuItem.Name = "openfileToolStripMenuItem";
			this.openfileToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.openfileToolStripMenuItem.Text = "Open &file";
			this.openfileToolStripMenuItem.Click += new System.EventHandler(this.openfileToolStripMenuItem_Click);
			// 
			// openInNotepadToolStripMenuItem
			// 
			this.openInNotepadToolStripMenuItem.Name = "openInNotepadToolStripMenuItem";
			this.openInNotepadToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.openInNotepadToolStripMenuItem.Text = "Open in &Notepad";
			this.openInNotepadToolStripMenuItem.Click += new System.EventHandler(this.openInNotepadToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 6);
			// 
			// copyPathToolStripMenuItem
			// 
			this.copyPathToolStripMenuItem.Name = "copyPathToolStripMenuItem";
			this.copyPathToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyPathToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.copyPathToolStripMenuItem.Text = "Copy &path";
			this.copyPathToolStripMenuItem.Click += new System.EventHandler(this.copyPathToolStripMenuItem_Click);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.copyToolStripMenuItem.Text = "&Copy file name";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// copyEntirelineToolStripMenuItem
			// 
			this.copyEntirelineToolStripMenuItem.Name = "copyEntirelineToolStripMenuItem";
			this.copyEntirelineToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.copyEntirelineToolStripMenuItem.Text = "Copy entire &line";
			this.copyEntirelineToolStripMenuItem.Click += new System.EventHandler(this.copyEntirelineToolStripMenuItem_Click);
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 125);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.listViewResults);
			this.splitContainer.Panel1.Controls.Add(this.checkBoxShowPreview);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.richTextBoxPreview);
			this.splitContainer.Size = new System.Drawing.Size(761, 299);
			this.splitContainer.SplitterDistance = 149;
			this.splitContainer.TabIndex = 1;
			// 
			// checkBoxShowPreview
			// 
			this.checkBoxShowPreview.AutoSize = true;
			this.checkBoxShowPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.checkBoxShowPreview.Location = new System.Drawing.Point(0, 132);
			this.checkBoxShowPreview.Name = "checkBoxShowPreview";
			this.checkBoxShowPreview.Size = new System.Drawing.Size(761, 17);
			this.checkBoxShowPreview.TabIndex = 1;
			this.checkBoxShowPreview.Text = "&Preview pane";
			this.checkBoxShowPreview.UseVisualStyleBackColor = true;
			this.checkBoxShowPreview.CheckedChanged += new System.EventHandler(this.checkBoxShowPreview_CheckedChanged);
			// 
			// richTextBoxPreview
			// 
			this.richTextBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.richTextBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxPreview.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBoxPreview.Location = new System.Drawing.Point(0, 0);
			this.richTextBoxPreview.Name = "richTextBoxPreview";
			this.richTextBoxPreview.ReadOnly = true;
			this.richTextBoxPreview.Size = new System.Drawing.Size(761, 146);
			this.richTextBoxPreview.TabIndex = 0;
			this.richTextBoxPreview.Text = "";
			this.richTextBoxPreview.WordWrap = false;
			// 
			// MainForm
			// 
			this.AcceptButton = this.buttonSearch;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(761, 446);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Knightrunner SimpleSearch";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.contextMenuStrip.ResumeLayout(false);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonSearch;
		private System.Windows.Forms.TextBox textBoxText;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxFileName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.ListView listViewResults;
		private System.Windows.Forms.ColumnHeader columnHeaderFileName;
		private System.Windows.Forms.ColumnHeader columnHeaderDirectory;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.ColumnHeader columnHeaderModifyDate;
		private System.Windows.Forms.ComboBox comboBoxDirectory;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem opendirectoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openfileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openInNotepadToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem copyPathToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Button buttonTextOptions;
		private System.Windows.Forms.Label labelUsingMultiText;
		private System.Windows.Forms.ColumnHeader columnHeaderSearchTextHits;
		private System.Windows.Forms.ToolStripMenuItem copyEntirelineToolStripMenuItem;
		private System.Windows.Forms.TextBox textBoxDirPath;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.CheckBox checkBoxShowPreview;
		private System.Windows.Forms.RichTextBox richTextBoxPreview;
	}
}

