using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LineCounter
{
	class LineCountResult
	{
		public int TotalFiles { get; set; }
		public int TotalLines { get; set; }
		public int BlankLines { get; set; }
	}
}
