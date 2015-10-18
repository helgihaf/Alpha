using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LineCounter
{
	class Engine
	{
		public List<string> Directories { get; private set; }
		public List<string> Wildcards { get; private set; }

		public Engine()
		{
			Directories = new List<string>();
			Wildcards = new List<string>();
		}

		public LineCountResult Execute()
		{
			var regexList = new List<Regex>();
			foreach (var wildcard in Wildcards)
			{
				regexList.Add(new Regex(WildcardToRegex(wildcard)));
			}

			var result = new LineCountResult();

			foreach (var dir in Directories)
			{
				foreach (var filePath in Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories))
				{
					foreach (var regex in regexList)
					{
						if (regex.IsMatch(filePath))
						{
							CountLines(filePath, result);
						}
					}
				}
			}

			return result;
		}

		private void CountLines(string filePath, LineCountResult result)
		{
			var lines = File.ReadAllLines(filePath);
			foreach (var line in lines )
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					result.BlankLines++;
				}
			}
			result.TotalLines += lines.Length;
			result.TotalFiles++;
		}

		public static string WildcardToRegex(string pattern)
		{
			return "^" + Regex.Escape(pattern)
							  .Replace(@"\*", ".*")
							  .Replace(@"\?", ".")
					   + "$";
		}
	}
}
