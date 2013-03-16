using System;

namespace Knightrunner.Library.Scheduling
{
    public class ScheduleTime : IComparable
    {
        private CrontabEntry entry;
        private DateTime next;


        internal ScheduleTime(CrontabEntry entry, DateTime date)
        {
            this.entry = entry;
            this.next = date;
        }

        public object Tag
        {
            get { return this.entry.Tag; }
        }

        public DateTime Date
        {
            get { return this.next; }
        }

        public TimeSpan Span
        {
            get
            {
                long diff = next.Ticks - DateTime.Now.Ticks;
                if (diff < 0)
                    return new TimeSpan(0, 0, 1);
                return new TimeSpan(diff);
            }
        }
        #region IComparable Members

        public int CompareTo(object obj)
        {
            return this.Date.CompareTo((obj as ScheduleTime).Date);
        }

        #endregion
    }

}
