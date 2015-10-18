using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marson.Compare.Core
{
    public abstract class Item
    {
        public Item(string fullPath)
        {
            if (fullPath == null)
            {
                throw new ArgumentNullException("fullPath");
            }
            this.FullPath = fullPath;
            this.Name = Path.GetFileName(fullPath);
        }

        public string Name { get; private set; }
        public string FullPath { get; private set; }
    }
}
