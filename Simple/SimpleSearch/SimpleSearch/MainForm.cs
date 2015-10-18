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
		private int maxStringItemsInCombo = 10;
		private SearchEngine searchEngine;
		private ListViewColumnSorter lvwColumnSorter;
		private char seperatorChar;
		private FileImageLoader imageLoader;

		private int lastSelectIndex;
		private int lastSelectLength;


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

			// Apply settings to combo boxes
			Properties.Settings.Default.DirectoryItems = ApplyComboBoxSetting(comboBoxDirectory, Properties.Settings.Default.DirectoryItems);
			Properties.Settings.Default.DirectorySubPaths = ApplyComboBoxSetting(comboBoxDirPath, Properties.Settings.Default.DirectorySubPaths);
			Properties.Settings.Default.FileNames = ApplyComboBoxSetting(comboBoxFileName, Properties.Settings.Default.FileNames);
			Properties.Settings.Default.Texts = ApplyComboBoxSetting(comboBoxText, Properties.Settings.Default.Texts);

			ShowHidePreviewPane();

			imageLoader = new FileImageLoader();
			imageLoader.LoadCompleted += new EventHandler<LoadCompletedEventArgs>(imageLoader_LoadCompleted);
		}


		private StringCollection ApplyComboBoxSetting(ComboBox comboBox, StringCollection stringCollection)
		{
			if (stringCollection == null)
				stringCollection = new StringCollection();
			comboBox.DataSource = stringCollection;

			return stringCollection;
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
			comboBoxDirPath.Enabled = inputState;
			comboBoxFileName.Enabled = inputState;
			comboBoxText.Enabled = inputState;
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
				(comboBoxDirPath.Text.Length > 0 || comboBoxFileName.Text.Length > 0 || comboBoxText.Text.Length > 0);
		}

		private void buttonSearch_Click(object sender, EventArgs e)
		{
			this.Validate();
			AddStringToCombo(comboBoxDirectory);
			AddStringToCombo(comboBoxDirPath);
			AddStringToCombo(comboBoxFileName);
			AddStringToCombo(comboBoxText);
			Search();
		}

		private void Search()
		{
			Cursor.Current = Cursors.WaitCursor;

			imageLoader.Enabled = false;
			imageLoader.ClearRequests();

			listViewResults.Items.Clear();
			ClearImageList();

			searchEngine.SearchDirectory = comboBoxDirectory.Text;
			searchEngine.DirectoryPath = comboBoxDirPath.Text;
			searchEngine.FileName = comboBoxFileName.Text;
			searchEngine.Texts = GetSearchTexts();
			
			SetInputState(false);

			progressBar.Visible = true;
			backgroundWorker.RunWorkerAsync();
		}

		private void ClearImageList()
		{
			imageLoader.ClearRequests();
			listViewResults.SmallImageList = null;
			foreach (Image image in imageList.Images)
			{
				image.Dispose();
			}
			imageList.Images.Clear();
			listViewResults.SmallImageList = imageList;
		}

		private string[] GetSearchTexts()
		{
			if (labelUsingMultiText.Visible)
			{
				return comboBoxText.Text.Split(seperatorChar);
			}
			else if (comboBoxText.Text.Length > 0)
			{
				return new string[] { comboBoxText.Text };
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

		private bool showImage = true;
		private void AddResult(SearchFileInfo fileInfo)
		{
			pendingSearches.Add(fileInfo);
            if (!timer.Enabled)
            {
                timer.Enabled = true;
            }
        }

		private List<SearchFileInfo> pendingSearches = new List<SearchFileInfo>();
		private void timer_Tick(object sender, EventArgs e)
		{
			AddPendingSearches();
			timer.Enabled = false;
		}

		private void AddPendingSearches()
		{
			if (pendingSearches.Count == 0)
			{
				return;
			}
			Cursor.Current = Cursors.WaitCursor;
			var items = new ListViewItem[pendingSearches.Count];
			for (int i = 0; i < pendingSearches.Count; i++)
            {
                var searchFileInfo = pendingSearches[i];
                ListViewItem item = CreateListViewItem(searchFileInfo);
                items[i] = item;
            }
            listViewResults.Items.AddRange(items);
			pendingSearches.Clear();
		}

        private ListViewItem CreateListViewItem(SearchFileInfo searchFileInfo)
        {
            ListViewItem item = new ListViewItem();
            item.Tag = searchFileInfo;
            item.Text = Path.GetFileName(searchFileInfo.Path);
            item.SubItems.Add(Path.GetDirectoryName(searchFileInfo.Path));
            item.SubItems.Add(searchFileInfo.LastWriteTime.ToString());
            item.SubItems.Add(searchFileInfo.SearchTexts);
            if (showImage)
            {
                imageLoader.AddRequest(searchFileInfo.Path, item);
            }

            return item;
        }

        private void imageLoader_LoadCompleted(object sender, LoadCompletedEventArgs e)
		{
			this.Invoke(new SetImageDelegate(SetImage), (ListViewItem)e.Tag, e.Image);
		}

		private delegate void SetImageDelegate(ListViewItem item, Image image);
		private void SetImage(ListViewItem item, Image image)
		{
			if (image != null)
			{
				imageList.Images.Add(image);
				item.ImageIndex = imageList.Images.Count - 1;
			}
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
			if (imageLoader != null)
				imageLoader.Dispose();
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

			AddPendingSearches();
			timer.Enabled = false;

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
				{
					ShowHitListWindow(hitList);
				}
			}
			imageLoader.Enabled = true;
			SetInputState(true);
			progressBar.Visible = false;
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

        private DateTime lastStatusUpdate = DateTime.MinValue;

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			SearchEventArgs searchEventArgs = e.UserState as SearchEventArgs;
			SearchFoundEventArgs searchFoundEventArgs = e.UserState as SearchFoundEventArgs;

            if (searchEventArgs != null)
            {
                var now = DateTime.Now;
                if (lastStatusUpdate.AddSeconds(1) < now)
                {
                    SetStatusText("Searching " + searchEventArgs.Path);
                    lastStatusUpdate = now;
                }
            }
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


		//private void AddDirectoryToCombo()
		//{
		//    string directory = comboBoxDirectory.Text.Trim();
		//    StringCollection directoryItems = Properties.Settings.Default.DirectoryItems;
		//    foreach (string s in directoryItems)
		//    {
		//        if (string.Compare(directory, s, true) == 0)
		//            return;
		//    }

		//    while (directoryItems.Count >= maxStringItemsInCombo)
		//        directoryItems.RemoveAt(directoryItems.Count - 1);

		//    directoryItems.Insert(0, directory);
		//    comboBoxDirectory.DataSource = null; 
		//    comboBoxDirectory.DataSource = directoryItems;
		//    comboBoxDirectory.Text = directory;
		//}


		private void AddStringToCombo(ComboBox comboBox)
		{
			string textProper = comboBox.Text.Trim();

			// Make sure that:
			// - The current string is the first string in the combo box
			// - The string only appears once
			// - There are no more than maxStringitemsInCombo in the combo box

			StringCollection stringItems = (StringCollection)comboBox.DataSource;

			for (int i = 0; i < stringItems.Count; i++)
			{
				if (string.Compare(textProper, stringItems[i], StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					if (i == 0)
					{
						// the string is already at the top, our work is done
						return;
					}
					stringItems.RemoveAt(i);
					break;
				}
			}

			stringItems.Insert(0, textProper);

			while (stringItems.Count >= maxStringItemsInCombo)
			{
				stringItems.RemoveAt(stringItems.Count - 1);
			}

			// Refresh the data source
			comboBox.DataSource = null;
			comboBox.DataSource = stringItems;
			comboBox.SelectedIndex = 0;
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
			if (comboBoxText.Text.Length > 0)
			{
				form.SearchText = comboBoxText.Text;
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
				comboBoxText.Text = form.SearchText;
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
			lastSelectIndex = -1;
			lastSelectLength = 0;
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
					HighlightSearchTerm(selectIndex, selectLength);
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
			lastSelectIndex = firstSelectIndex;
		}

		private void toolStripButtonFindFirst_Click(object sender, EventArgs e)
		{
			SelectText(0, false);
		}

		private void toolStripButtonFindPrevious_Click(object sender, EventArgs e)
		{
			SelectText(lastSelectIndex - 1, true);
		}

		private void toolStripButtonFindNext_Click(object sender, EventArgs e)
		{
			SelectText(lastSelectIndex + 1, false);
		}

		private void SelectText(int mainIndex, bool searchBackwards)
		{
			SetStatusText(null);
			RestoreLastSelected();
			string[] searchTexts = GetSearchTexts();

			if (searchTexts == null)
				return;

			string text = richTextBoxPreview.Text;
			int selectIndex = int.MaxValue;
			bool found = false;
			foreach (string searchText in searchTexts)
			{
				int index;
				if (!searchBackwards)
				{
					index = text.IndexOf(searchText, mainIndex, StringComparison.CurrentCultureIgnoreCase);
				}
				else
				{
					index = text.LastIndexOf(searchText, mainIndex, StringComparison.CurrentCultureIgnoreCase);
				}
				if (index >= 0 && index < selectIndex)
				{
					lastSelectIndex = index;
					lastSelectLength = searchText.Length;
					HighlightCurrentSearchTerm(lastSelectIndex, lastSelectLength);
					richTextBoxPreview.ScrollToCaret();
					found = true;
					break;
				}
			}

			if (!found)
			{
				SetStatusText("Not found");
			}
		}

		private void RestoreLastSelected()
		{
			if (lastSelectIndex != -1)
			{
				HighlightSearchTerm(lastSelectIndex, lastSelectLength);
			}
		}

		private void HighlightSearchTerm(int index, int length)
		{
			richTextBoxPreview.Select(index, length);
			richTextBoxPreview.SelectionBackColor = Color.LightGreen;
			richTextBoxPreview.SelectionColor = Color.Black;
		}


		private void HighlightCurrentSearchTerm(int index, int length)
		{
			richTextBoxPreview.Select(index, length);
			richTextBoxPreview.SelectionBackColor = Color.Blue;
			richTextBoxPreview.SelectionColor = Color.White;
		}

		
	}
}
