using System;
using System.Collections.Generic;

namespace Knightrunner.Library.Scheduling
{
    //---------------------------------------------------------------------------------------------
    /// <summary>
    /// The ScheduleEngine class holds a list ScheduleEntry objects and using those entries
    /// can figure out the next DateTime of an event.
    /// </summary>
    public class ScheduleEngine
    {
        private CrontabEntryList entries = new CrontabEntryList();
        private CronCalendarBuilder builder = new CronCalendarBuilder();

        public ScheduleEngine()
        {
        }

        public ScheduleEngine(ScheduleEntry entry)
        {
            entries.Add(CrontabEntry.Create(entry));
        }

        public ScheduleEngine(IEnumerable<ScheduleEntry> entries)
        {
            foreach (ScheduleEntry entry in entries)
            {
                this.entries.Add(CrontabEntry.Create(entry));
            }
        }


        public void Add(ScheduleEntry scheduleEntry)
        {
            entries.Add(CrontabEntry.Create(scheduleEntry));
        }

        public void Add(string cronString, object tag)
        {
            ScheduleEntry scheduleEntry = ScheduleEntry.FromChronString(cronString);
            scheduleEntry.Tag = tag;
            entries.Add(CrontabEntry.Create(scheduleEntry));
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the next valid date/time from the schedule(s)
        /// </summary>
        /// <returns></returns>
        public ScheduleTime[] Next()
        {
            return builder.Next(entries);
        }


        public ScheduleTime[] Next(DateTime fromDate)
        {
            return builder.Next(entries, fromDate);
        }

        public ScheduleTime[] Next(object tag)
        {
            CrontabEntryList list = entries.GetTagList(tag);
            if (list != null)
            {
                return builder.Next(list);
            }
            return null;
        }

        public TimeSpan NextSpan()
        {
            ScheduleTime[] crons = Next();
            return crons[0].Span;
        }

        public TimeSpan NextSpan(DateTime fromDate)
        {
            ScheduleTime[] crons = Next(fromDate);
            return crons[0].Span;
        }

        public DateTime NextDateTime()
        {
            ScheduleTime[] crons = Next();
            return crons[0].Date;
        }

        public DateTime NextDateTime(DateTime fromDate)
        {
            ScheduleTime[] crons = Next(fromDate);
            return crons[0].Date;
        }


        public void DumpUpcoming(int numberOfDates)
        {
            ScheduleTime[] times;
            DateTime date = DateTime.UtcNow;
            for (int i = 0; i < numberOfDates; i++)
            {
                times = Next(date);

                if (times[0].Date > DateTime.MinValue)
                {
                    date = times[0].Date;
                    Console.WriteLine("Next schedule time : " + date.ToString("R"));
                }
                else
                {
                    break;
                }
            }
        }

    }
}
