using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Collections.ObjectModel;

namespace SimpleSearch
{
	internal class SearchEngine
	{
		private string searchForDirectoryPath;
		private string searchForFileName;
		private List<SearchHit> searchTexts;
		private bool cancelPending;
		private List<SearchFileInfo> results;

		public class SearchHit
		{
			public string Text { get; set; }
			public int Hits { get; set; }
		}

		public string SearchDirectory { get; set; }
		public string DirectoryPath { get; set; }
		public string FileName { get; set; }
		public string [] Texts { get; set; }
		public List<SearchFileInfo> Results
		{
			get { return results; }
		}
		
		public bool Cancelled
		{
			get { return cancelPending; }
		}

		public ReadOnlyCollection<SearchHit> HitList
		{
			get
			{
				if (searchTexts != null)
				{
					return searchTexts.AsReadOnly();
				}
				else
				{
					return null;
				}
			}
		}

		public event EventHandler<SearchEventArgs> SearchingPath;
		public event EventHandler<SearchFoundEventArgs> SearchFound;
		
		public void Search()
		{
			if (string.IsNullOrEmpty(SearchDirectory))
				throw new InvalidOperationException("Directory must be set.");

			if (!string.IsNullOrEmpty(DirectoryPath))
				searchForDirectoryPath = DirectoryPath;
			else
				searchForDirectoryPath = null;

			if (!string.IsNullOrEmpty(FileName))
				searchForFileName = FileName;
			else
				searchForFileName = null;

			if (Texts != null && Texts.Length > 0)
			{
				searchTexts = new List<SearchHit>();
				foreach (string text in Texts)
				{
					searchTexts.Add(new SearchHit { Text = text, Hits = 0 });
				}
			}
			else
				searchTexts = null;
				
			if (searchForDirectoryPath == null && searchForFileName == null && searchTexts == null)
				throw new InvalidOperationException("At least one of DirectoryPath, FileName or Texts must be set.");
		
			if (!Directory.Exists(SearchDirectory))
				throw new InvalidOperationException("Directory \"" + SearchDirectory + "\" does not exist.");

			cancelPending = false;
			results = null;
			
			results = DoSearch(SearchDirectory);
		}


		private List<SearchFileInfo> DoSearch(string searchDir)
		{
			OnSearchingPath(searchDir);
			List<SearchFileInfo> foundFiles = new List<SearchFileInfo>();

			bool checkFiles;

			if (searchForDirectoryPath != null)
			{
				// Check if directory path below the initial search path is found
				string subPath = searchDir.Substring(this.SearchDirectory.Length);
				checkFiles = Wildcard.Match(searchForDirectoryPath, subPath);
			}
			else
			{
				checkFiles = true;
			}

			if (checkFiles)
			{
				string[] files = null;

				// Files in this directory
				try
				{
					if (searchForFileName != null)
						files = Directory.GetFiles(searchDir, searchForFileName);
					else
						files = Directory.GetFiles(searchDir);
				}
				catch (UnauthorizedAccessException)
				{
				}

				if (files != null)
				{
					foreach (string file in files)
					{
						if (cancelPending)
							return foundFiles;

						string searchTextHits;
						if (searchTexts != null)
						{
							OnSearchingPath(file);
							if (SearchForTextInFile(file, searchTexts.Count > 1, out searchTextHits))
							{
								SearchFileInfo fileInfo = CreateSearchFileInfo(file, searchTextHits);
								foundFiles.Add(fileInfo);
								OnSearchFound(fileInfo);
							}
						}
						else
						{
							SearchFileInfo fileInfo = CreateSearchFileInfo(file, null);
							foundFiles.Add(fileInfo);
							OnSearchFound(fileInfo);
						}
					}
				}
			}
			
			// Directories in this directory
			string[] dirs = null;
			try
			{
				 dirs = Directory.GetDirectories(searchDir);
			}
			catch (UnauthorizedAccessException)
			{
			}

			if (dirs != null)
			{
				foreach (string dir in dirs)
				{
					if (cancelPending)
						return foundFiles;

					foundFiles.AddRange(DoSearch(dir));
				}
			}
			
			return foundFiles;
		}

		private SearchFileInfo CreateSearchFileInfo(string file, string searchTextHits)
		{
			FileInfo fileInfo = new FileInfo(file);
			SearchFileInfo searchFileInfo = new SearchFileInfo();
			searchFileInfo.Path = file;
			searchFileInfo.LastWriteTime = fileInfo.LastWriteTime;
			searchFileInfo.SearchTexts = searchTextHits;

			return searchFileInfo;
		}


		private bool SearchForTextInFile(string filePath, bool gatherHits, out string searchTextHits)
		{
			searchTextHits = null;
			Dictionary<string, string> searchTextHitsDictionary = new Dictionary<string, string>();

			bool found = false;
			try
			{
				using (StreamReader reader = new StreamReader(filePath))
				{
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						foreach (SearchHit hit in searchTexts)
						{
							if (line.IndexOf(hit.Text, StringComparison.CurrentCultureIgnoreCase) >= 0)
							{
								if (!gatherHits)
									return true;

								hit.Hits++;
								searchTextHitsDictionary[hit.Text] = null;
								found = true;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is IOException || ex is OutOfMemoryException)
				{
				}
				else
					throw;
			}

			if (searchTextHitsDictionary.Count > 0)
			{
				searchTextHits = Utils.LinesToSeparatedString(';', searchTextHitsDictionary.Keys.ToArray());
			}
			return found;
		}

		private void OnSearchingPath(string path)
		{
			if (SearchingPath != null)
			{
				SearchingPath(this, new SearchEventArgs { Path = path });
			}
		}

		private void OnSearchFound(SearchFileInfo fileInfo)
		{
			if (SearchFound != null)
			{
				SearchFound(this, new SearchFoundEventArgs { FileInfo = fileInfo });
			}
		}

		public void Cancel()
		{
			cancelPending = true;
		}
	}
	
	
	internal class SearchEventArgs : EventArgs
	{
		public string Path { get; set; }
	}


	internal class SearchFoundEventArgs : EventArgs
	{
		public SearchFileInfo FileInfo { get; set; }
	}

	internal class SearchFileInfo
	{
		public string Path { get; set; }
		public DateTime LastWriteTime { get; set; }
		public string SearchTexts { get; set; }
	}
}
