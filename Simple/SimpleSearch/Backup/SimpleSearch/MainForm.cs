using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace SimpleSearch
{
	public partial class MainForm : Form
	{
		private int maxDirectoryItems = 10;
		private SearchEngine searchEngine;
		private ListViewColumnSorter lvwColumnSorter;
		private char seperatorChar;
		
		public MainForm()
		{
			InitializeComponent();
			// Create an instance of a ListView column sorter and assign it to the ListView control.
			lvwColumnSorter = new ListViewColumnSorter();
			lvwColumnSorter.CompareItems += new EventHandler<ListViewColumnSorterCompareEventArgs>(lvwColumnSorter_CompareItems);
			this.listViewResults.ListViewItemSorter = lvwColumnSorter;

			searchEngine = new SearchEngine();
			searchEngine.SearchingPath += new EventHandler<SearchEventArgs>(SearchEngineSearchingPath);
			searchEngine.SearchFound += new EventHandler<SearchFoundEventArgs>(SearchEngineSearchFound);

			if (Properties.Settings.Default.DirectoryItems == null)
				Properties.Settings.Default.DirectoryItems = new StringCollection();
			comboBoxDirectory.DataSource = Properties.Settings.Default.DirectoryItems;

			ShowHidePreviewPane();
		}

		private void lvwColumnSorter_CompareItems(object sender, ListViewColumnSorterCompareEventArgs e)
		{
			if (e.Column != 2)
			{
				e.CompareResult = string.Compare(e.ItemX.SubItems[e.Column].Text, e.ItemY.SubItems[e.Column].Text, true);
			}
			else
			{
				SearchFileInfo infoX = (SearchFileInfo)e.ItemX.Tag;
				SearchFileInfo infoY = (SearchFileInfo)e.ItemY.Tag;
				e.CompareResult = DateTime.Compare(infoX.LastWriteTime, infoY.LastWriteTime);
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			SetInputState(true);
			SetStatusText(string.Empty);
		}

		private void SetInputState(bool inputState)
		{
			comboBoxDirectory.Enabled = inputState;
			textBoxDirPath.Enabled = inputState;
			textBoxFileName.Enabled = inputState;
			textBoxText.Enabled = inputState;
			buttonTextOptions.Enabled = inputState;
			buttonBrowse.Enabled = inputState;
			buttonSearch.Visible = inputState;
			buttonCancel.Visible = !inputState;
			if (buttonCancel.Visible)
				buttonCancel.Enabled = true;
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			this.Validate();
			if (comboBoxDirectory.Text.Length > 0)
				folderBrowserDialog.SelectedPath = comboBoxDirectory.Text;
			
			if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
			{
				comboBoxDirectory.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void TextChangedHandler(object sender, EventArgs e)
		{
			UpdateSearchButtonState();
		}

		private void UpdateSearchButtonState()
		{
			buttonSearch.Enabled =
				comboBoxDirectory.Text.Length > 0 &&
				(textBoxDirPath.Text.Length > 0 || textBoxFileName.Text.Length > 0 || textBoxText.Text.Length > 0);
		}

		private void buttonSearch_Click(object sender, EventArgs e)
		{
			this.Validate();
			AddDirectoryToCombo();
			Search();
		}

		private void Search()
		{
			listViewResults.Items.Clear();

			searchEngine.SearchDirectory = comboBoxDirectory.Text;
			searchEngine.DirectoryPath = textBoxDirPath.Text;
			searchEngine.FileName = textBoxFileName.Text;
			searchEngine.Texts = GetSearchTexts();
			
			SetInputState(false);
			
			this.Cursor = Cursors.AppStarting;
			backgroundWorker.RunWorkerAsync();
		}

		private string[] GetSearchTexts()
		{
			if (labelUsingMultiText.Visible)
			{
				return textBoxText.Text.Split(seperatorChar);
			}
			else if (textBoxText.Text.Length > 0)
			{
				return new string[] { textBoxText.Text };
			}
			else
				return null;
		}

		private void SetStatusText(string text)
		{
			toolStripStatusLabel.Text = text;
		}

		private void SearchEngineSearchingPath(object sender, SearchEventArgs e)
		{
			backgroundWorker.ReportProgress(0, e);
		}

		private void SearchEngineSearchFound(object sender, SearchFoundEventArgs e)
		{
			backgroundWorker.ReportProgress(1, e);
		}
		
		private void AddResult(SearchFileInfo fileInfo)
		{
			ListViewItem item = new ListViewItem();
			item.Tag = fileInfo;
			item.Text = Path.GetFileName(fileInfo.Path);
			item.SubItems.Add(Path.GetDirectoryName(fileInfo.Path));
			item.SubItems.Add(fileInfo.LastWriteTime.ToString());
			item.SubItems.Add(fileInfo.SearchTexts);
			listViewResults.Items.Add(item);
		}

		private SearchFileInfo GetSelectedFile()
		{
			SearchFileInfo fileInfo = null;
			if (listViewResults.SelectedItems.Count > 0)
			{
				fileInfo = (SearchFileInfo)listViewResults.SelectedItems[0].Tag;
			}

			return fileInfo;
		}



		private SearchFileInfo[] GetSelectedFiles()
		{
			List<SearchFileInfo> fileInfos = new List<SearchFileInfo>();
			for (int i = 0; i < listViewResults.SelectedItems.Count; i++)
			{
				SearchFileInfo fileInfo = (SearchFileInfo)listViewResults.SelectedItems[i].Tag;
				fileInfos.Add(fileInfo);
			}

			return fileInfos.ToArray();
		}


		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Properties.Settings.Default.Save();
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			searchEngine.Search();
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			int resultCount = 0;
			if (searchEngine.Results != null)
				resultCount = searchEngine.Results.Count;
				
			if (e.Cancelled || searchEngine.Cancelled)
			{
				SetStatusText("Cancelled, found " + resultCount.ToString() + " files.");
			}
			else if (e.Error != null)
			{
				MessageBox.Show
				(
					this,
					"An error occurred during search. " + Environment.NewLine +
					e.Error.GetType().ToString() + ": " + e.Error.Message,
					"Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Stop
				);
				SetStatusText("Error.");
			}
			else
			{
				SetStatusText("Found " + resultCount.ToString() + " files.");
				ReadOnlyCollection<SearchEngine.SearchHit> hitList = searchEngine.HitList;
				if (labelUsingMultiText.Visible && hitList != null && hitList.Count > 0)
					ShowHitListWindow(hitList);
			}
			SetInputState(true);
			this.Cursor = Cursors.Default;
		}

		private void ShowHitListWindow(ReadOnlyCollection<SearchEngine.SearchHit> hitList)
		{
			TextForm textForm = new TextForm();
			foreach (SearchEngine.SearchHit hit in hitList)
			{
				textForm.MainTextBox.AppendText(hit.Text + " " + hit.Hits.ToString() + Environment.NewLine);
			}
			textForm.Show();
		}

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			SearchEventArgs searchEventArgs = e.UserState as SearchEventArgs;
			SearchFoundEventArgs searchFoundEventArgs = e.UserState as SearchFoundEventArgs;

			if (searchEventArgs != null)
				SetStatusText("Searching " + searchEventArgs.Path);
			else if (searchFoundEventArgs != null)
				AddResult(searchFoundEventArgs.FileInfo);
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			searchEngine.Cancel();
			buttonCancel.Enabled = false;
			SetStatusText("Cancelling...");
		}

		private void listViewResults_ItemDrag(object sender, ItemDragEventArgs e)
		{
			SearchFileInfo[] fileInfos = GetSelectedFiles();
			if (fileInfos != null && fileInfos.Length > 0)
			{
				List<string> filePaths = new List<string>();
				foreach (SearchFileInfo fileInfo in fileInfos)
					filePaths.Add(fileInfo.Path);
				DoDragDrop(new DataObject(DataFormats.FileDrop, filePaths.ToArray()), DragDropEffects.Copy | DragDropEffects.Link);
			}
		}

		private void listViewResults_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			// Determine if clicked column is already the column that is being sorted.
			if (e.Column == lvwColumnSorter.SortColumn)
			{
				// Reverse the current sort direction for this column.
				if (lvwColumnSorter.Order == SortOrder.Ascending)
				{
					lvwColumnSorter.Order = SortOrder.Descending;
				}
				else
				{
					lvwColumnSorter.Order = SortOrder.Ascending;
				}
			}
			else
			{
				// Set the column number that is to be sorted; default to ascending.
				lvwColumnSorter.SortColumn = e.Column;
				lvwColumnSorter.Order = SortOrder.Ascending;
			}

			// Perform the sort with these new sort options.
			this.listViewResults.Sort();
		}


		private void AddDirectoryToCombo()
		{
			string directory = comboBoxDirectory.Text.Trim();
			StringCollection directoryItems = Properties.Settings.Default.DirectoryItems;
			foreach (string s in directoryItems)
			{
				if (string.Compare(directory, s, true) == 0)
					return;
			}

			while (directoryItems.Count >= maxDirectoryItems)
				directoryItems.RemoveAt(directoryItems.Count - 1);

			directoryItems.Insert(0, directory);
			comboBoxDirectory.DataSource = null; 
			comboBoxDirectory.DataSource = directoryItems;
			comboBoxDirectory.Text = directory;
		}

		private void listViewResults_DoubleClick(object sender, EventArgs e)
		{
			OpenDirectory();
		}

		private void opendirectoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenDirectory();
		}

		private void openfileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFile();
		}

		private void openInNotepadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenInNotepad();
		}

		private void copyPathToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CopyPath();
		}


		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CopyFileName();
		}


		private void OpenDirectory()
		{
			SearchFileInfo fileInfo = GetSelectedFile();
			if (fileInfo != null)
			{
				OpenDirectory(Path.GetDirectoryName(fileInfo.Path));
			}
		}

		private void OpenDirectory(string dirPath)
		{
			Process.Start(dirPath);
		}


		private void OpenFile()
		{
			SearchFileInfo fileInfo = GetSelectedFile();
			if (fileInfo != null)
			{
				OpenFile(fileInfo.Path);
			}
		}

		private void OpenFile(string dirPath)
		{
			Process.Start(dirPath);
		}


		private void OpenInNotepad()
		{
			SearchFileInfo fileInfo = GetSelectedFile();
			if (fileInfo != null)
			{
				ProcessStartInfo info = new ProcessStartInfo();
				info.FileName = "notepad.exe";
				info.Arguments = "\"" + fileInfo.Path + "\"";
				Process.Start(info);
			}
		}

		private void CopyPath()
		{
			SearchFileInfo[] fileInfos = GetSelectedFiles();
			StringBuilder sb = new StringBuilder();
			foreach (SearchFileInfo fileInfo in fileInfos)
			{
				if (sb.Length > 0)
					sb.Append(Environment.NewLine);
				sb.Append(fileInfo.Path);
			}

			if (sb.Length > 0)
				Clipboard.SetText(sb.ToString());
		}

		private void CopyFileName()
		{
			SearchFileInfo[] fileInfos = GetSelectedFiles();
			StringBuilder sb = new StringBuilder();
			foreach (SearchFileInfo fileInfo in fileInfos)
			{
				if (sb.Length > 0)
					sb.Append(Environment.NewLine);
				sb.Append(Path.GetFileName(fileInfo.Path));
			}

			if (sb.Length > 0)
				Clipboard.SetText(sb.ToString());
		}

		private void buttonTextOptions_Click(object sender, EventArgs e)
		{
			this.Validate();
			SearchTextForm form = new SearchTextForm();
			if (textBoxText.Text.Length > 0)
			{
				form.SearchText = textBoxText.Text;
			}

			form.MultiText = Properties.Settings.Default.MultiText;
			form.SeperatorChar = Properties.Settings.Default.SeperatorChar;
			if (form.ShowDialog(this) == DialogResult.OK)
			{
				Properties.Settings.Default.MultiText = form.MultiText;
				Properties.Settings.Default.SeperatorChar = form.SeperatorChar;

				if (form.MultiText)
				{
					labelUsingMultiText.Text = string.Format("Multiple text search using {0} as seperator", form.SeperatorChar);
					labelUsingMultiText.Visible = true;
					seperatorChar = form.SeperatorChar;
				}
				labelUsingMultiText.Visible = form.MultiText;
				textBoxText.Text = form.SearchText;
			}
		}

		private void copyEntirelineToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CopyLine();
		}

		private void CopyLine()
		{
			SearchFileInfo[] fileInfos = GetSelectedFiles();
			StringBuilder sb = new StringBuilder();
			foreach (SearchFileInfo fileInfo in fileInfos)
			{
				if (sb.Length > 0)
					sb.Append(Environment.NewLine);
				sb.Append(fileInfo.Path + ";");
				sb.Append(fileInfo.LastWriteTime.ToString() + ";");
				sb.Append(fileInfo.SearchTexts);
			}

			if (sb.Length > 0)
				Clipboard.SetText(sb.ToString());
		}

		private void checkBoxShowPreview_CheckedChanged(object sender, EventArgs e)
		{
			ShowHidePreviewPane();
		}

		private void ShowHidePreviewPane()
		{
			splitContainer.Panel2Collapsed = !checkBoxShowPreview.Checked;
			ShowPreviewFile();
		}

		private void listViewResults_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (checkBoxShowPreview.Checked)
			{
				ShowPreviewFile();
			}
		}

		private void ShowPreviewFile()
		{
			SearchFileInfo fileInfo = GetSelectedFile();
			if (fileInfo == null)
				return;

			if (!File.Exists(fileInfo.Path))
				return;

			richTextBoxPreview.Text = File.ReadAllText(fileInfo.Path);

			HighlighSearchText();
		}

		private void HighlighSearchText()
		{
			string[] searchTexts = GetSearchTexts();
			
			if (searchTexts == null)
				return;

			string text = richTextBoxPreview.Text;

			int firstSelectIndex = -1;
			int mainIndex = 0;

			while (mainIndex < text.Length)
			{
				int selectIndex = int.MaxValue;
				int selectLength = 0;
				foreach (string searchText in searchTexts)
				{
					int index = text.IndexOf(searchText, mainIndex, StringComparison.CurrentCultureIgnoreCase);
					if (index >= 0 && index < selectIndex)
					{
						selectIndex = index;
						selectLength = searchText.Length;
					}
				}

				if (selectIndex == int.MaxValue)
				{
					mainIndex = text.Length;
				}
				else
				{
					richTextBoxPreview.Select(selectIndex, selectLength);
					//richTextBoxPreview.SelectionFont = new Font(richTextBoxPreview.Font, FontStyle.Bold);
					richTextBoxPreview.SelectionBackColor = Color.Blue;
					richTextBoxPreview.SelectionColor = Color.White;
					if (firstSelectIndex == -1)
						firstSelectIndex = selectIndex;
					
					mainIndex = selectIndex + selectLength;
				}
			}

			if (firstSelectIndex != -1)
			{
				richTextBoxPreview.Select(firstSelectIndex, 0);
				richTextBoxPreview.ScrollToCaret();
			}
		}


		
	}
}
