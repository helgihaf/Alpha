using System.Collections.Generic;

namespace Knightrunner.Library.Scheduling
{
    internal class CrontabEntryList : List<CrontabEntry>
    {
        public CrontabEntryList GetTagList(object tag)
        {
            CrontabEntryList list = new CrontabEntryList();
            foreach (CrontabEntry entry in this)
            {
                if (entry.Tag.Equals(tag))
                {
                    list.Add(entry);
                }
            }
            return list;
        }

    }
}
