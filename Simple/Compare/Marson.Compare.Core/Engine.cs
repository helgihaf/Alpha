using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marson.Compare.Core
{
    public class Engine
    {
        public Entry CompareDirectories(string leftDirPath, string rightDirPath, CompareOptions options)
        {
            var dirEntry = new DirEntry { Left = new DirItem(leftDirPath), Right = new DirItem(rightDirPath) };
            dirEntry.Populate();
            dirEntry.Compare(FileComparerFactory.Create(options));
            return dirEntry;
        }
    }
}
