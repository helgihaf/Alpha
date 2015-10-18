using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineCounter
{
	class Program
	{
		static void Main(string[] args)
		{
			var engine = new Engine();
			engine.Directories.Add(args[0]);
			engine.Wildcards.AddRange(args[1].Split(';'));
			Console.WriteLine("Counting...");
			var result = engine.Execute();
			Console.WriteLine("Result:");
			Console.WriteLine("  Total files:        {0}", result.TotalFiles);
			Console.WriteLine("  Total lines:        {0}", result.TotalLines);
			Console.WriteLine("  Blank lines:        {0}", result.BlankLines);
			Console.WriteLine("  Non-blank lines:    {0}", result.TotalLines - result.BlankLines);
		}
	}
}
