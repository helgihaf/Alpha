using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marson.Compare.Core
{
    public abstract class Entry
    {
        public Entry()
        {
            ChildEntries = new List<Entry>();
            CompareStatuses = new HashSet<CompareStatus>();
        }

        public string Name
        {
            get
            {
                string name;
                if (Left != null)
                    name = Left.Name;
                else
                    name = Right.Name;
                return name;
            }
        }

        public Item Left { get; internal set; }
        public Item Right { get; internal set; }
        public List<Entry> ChildEntries { get; private set; }
        public HashSet<CompareStatus> CompareStatuses { get; private set; }

        internal abstract void Compare(IFileComparer filecomparer);
    }
}
