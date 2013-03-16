using System;
using System.Collections.Generic;
using System.Globalization;

namespace Knightrunner.Library.Scheduling
{
    internal class CronCalendarBuilder
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Gets thes next CrontabEntry in the given Array. 
        /// We care only for one cause only need to know the next execution time.
        /// </summary>
        /// <param name="cebs"></param>
        /// <returns></returns>
        public CrontabEntry GetNextCrontabEntry(CrontabEntryList cebs)
        {
            long[] times = new long[cebs.Count];
            int value = 0;

            int index = 0;

            for (int i = 0; i < cebs.Count; i++)
            {
                times[i] = Next(cebs[i]).Date.Ticks;
            }

            long number = times[index];

            for (int i = 0; i < times.Length; i++)
            {
                if (times[i] < number)
                {
                    number = times[i];
                    value = i;
                }
            }
            return cebs[value];
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Builds a date from the next valid crontab entries
        /// </summary>
        /// <param name="cebs"></param>
        /// <returns>the next valid date</returns>
        public ScheduleTime[] Next(CrontabEntryList cebs)
        {
            return Next(cebs, DateTime.Now);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        ///  Builds a date from the next valid crontab entries
        ///  after the given date
        /// </summary>
        /// <param name="cebs">Entries to choose from</param>
        /// <param name="afterDate">Date after wich next valid date is calculated from</param>
        /// <returns>the next valid date</returns>
        public ScheduleTime[] Next(CrontabEntryList cebs, DateTime afterDate)
        {
            ScheduleTime[] dates = new ScheduleTime[cebs.Count];
            for (int i = 0; i < dates.Length; i++)
            {
                dates[i] = Next(cebs[i], afterDate);
            }
            Array.Sort(dates);

            List<ScheduleTime> retList = new List<ScheduleTime>();
            ScheduleTime first = dates[0];
            retList.Add(first);
            for (int i = 1; i < dates.Length; i++)
            {
                if (dates[i].CompareTo(first) == 0)
                    retList.Add(dates[i]);
                else
                    break;
            }
            return (ScheduleTime[])retList.ToArray();
        }

        ///
        /// This method builds a Date from a CrontabEntry. launching the same 
        /// method with now as parameter
        ///

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// This method builds a Date from a CrontabEntry. launching the same 
        /// method with now as parameter.
        /// </summary>
        /// <param name="ceb"></param>
        /// <returns></returns>
        public ScheduleTime Next(CrontabEntry ceb)
        {
            DateTime now = DateTime.Now;
            return Next(ceb, now);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Builds a Date from a CrontabEntry and from a starting Date 
        /// </summary>
        /// <param name="ceb"></param>
        /// <param name="afterDate"></param>
        /// <returns></returns>
        public ScheduleTime Next(CrontabEntry ceb, DateTime afterDate)
        {
            Calendar cal = CultureInfo.InvariantCulture.Calendar;
            DateTime after = new DateTime(afterDate.Ticks);


            int second = GetNextIndex(ceb.SecondFlags, after.Second);
            if (second == -1)
            {
                second = GetNextIndex(ceb.SecondFlags, 0);
                after = cal.AddMinutes(after, 1);
            }


            int minute = GetNextIndex(ceb.MinuteFlags, after.Minute);
            if (minute == -1)
            {
                second = GetNextIndex(ceb.SecondFlags, 0);
                minute = GetNextIndex(ceb.MinuteFlags, 0);
                after = cal.AddHours(after, 1);
            }

            int hour = GetNextIndex(ceb.HourFlags, after.Hour);
            if (hour == -1)
            {
                second = GetNextIndex(ceb.SecondFlags, 0);
                minute = GetNextIndex(ceb.MinuteFlags, 0);
                hour = GetNextIndex(ceb.HourFlags, 0);
                after = cal.AddDays(after, 1);
            }

            int dayOfMonthIndex = GetNextIndex(ceb.DayOfMonthFlags, after.Day - 1);
            if (dayOfMonthIndex == -1)
            {
                second = GetNextIndex(ceb.SecondFlags, 0);
                minute = GetNextIndex(ceb.MinuteFlags, 0);
                hour = GetNextIndex(ceb.HourFlags, 0);

                switch (ceb.MonthSequence)
                {
                    // NOTE: day is numbered from 0 to (N-1)
                    
                    case Sequence.First:
                        dayOfMonthIndex = 0;
                        break;
                    case Sequence.Second:
                        dayOfMonthIndex = 1;
                        break;
                    case Sequence.Third:
                        dayOfMonthIndex = 2;
                        break;
                    case Sequence.Fourth:
                        dayOfMonthIndex = 3;
                        break;
                    case Sequence.Last:
                        dayOfMonthIndex = DateTime.DaysInMonth(after.Year, after.Month) - 1;
                        break;
                    default:
                        dayOfMonthIndex = GetNextIndex(ceb.DayOfMonthFlags, 0);
                        after = cal.AddMonths(after, 1);
                        break;
                }
            }

            bool dayMatchRealDate = false;
            while (!dayMatchRealDate)
            {
                if (CheckDayValidInMonth(dayOfMonthIndex + 1, after.Month, after.Year))
                {
                    dayMatchRealDate = true;
                }
                else
                {
                    after = cal.AddMonths(after, 1);
                }
            }

            int monthIndex = GetNextIndex(ceb.MonthFlags, after.Month - 1);
            if (monthIndex == -1)
            {
                second = GetNextIndex(ceb.SecondFlags, 0);
                minute = GetNextIndex(ceb.MinuteFlags, 0);
                hour = GetNextIndex(ceb.HourFlags, 0);
                dayOfMonthIndex = GetNextIndex(ceb.DayOfMonthFlags, 0);
                monthIndex = GetNextIndex(ceb.MonthFlags, 0);
                after = cal.AddYears(after, 1);
            }

            int year = GetNextIndex(ceb.YearFlags, after.Year);
            if (year == -1)
            {
                second = GetNextIndex(ceb.SecondFlags, 0);
                minute = GetNextIndex(ceb.MinuteFlags, 0);
                hour = GetNextIndex(ceb.HourFlags, 0);
                dayOfMonthIndex = GetNextIndex(ceb.DayOfMonthFlags, 0);
                monthIndex = GetNextIndex(ceb.MonthFlags, 0);
                year = GetNextIndex(ceb.YearFlags, 0);
            }

            DateTime byMonthDays = GetTime(second, minute, hour, dayOfMonthIndex + 1, monthIndex + 1, year);
            DateTime calendar = new DateTime(byMonthDays.Ticks);

            bool[] bDaysOfWeek = ceb.DayOfWeekFlags;
            int dow = (int)calendar.DayOfWeek;

            if (bDaysOfWeek[dow])
            {
                if (ceb.WeekSequence == Sequence.Undefined)
                {
                    return new ScheduleTime(ceb, calendar);
                }
                else
                {
                    if (ceb.WeekSequence != Sequence.Last)
                        throw new NotImplementedException("Implemenation missing for WeekSequence");

                    int remainingDaysOfMonth = DateTime.DaysInMonth(calendar.Year, calendar.Month) - calendar.Day;
                    if (remainingDaysOfMonth <= 6)
                    {
                        return new ScheduleTime(ceb, calendar);
                    }
                    else
                    {
                        calendar = calendar.AddDays(remainingDaysOfMonth - 6);
                        return Next(ceb, calendar);
                    }
                }
            }
            else
            {
                calendar = calendar.AddDays(1);
                return Next(ceb, calendar);
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Builds a Date from a CrontabEntry and from a starting Date.
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="minutes"></param>
        /// <param name="hour"></param>
        /// <param name="dayOfMonth"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private static DateTime GetTime(int seconds,
            int minutes,
            int hour,
            int dayOfMonth,
            int month,
            int year)
        {
            try
            {
                return new DateTime(year, month, dayOfMonth, hour, minutes, seconds, 0);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// This method says wich is next index of this array.
        /// </summary>
        private static int GetNextIndex(bool[] array, int start)
        {
            for (int i = start; i < array.Length; i++)
            {
                if (array[i]) return i;
            }
            return -1;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// This says if this month has this day or not, basically this problem
        /// occurrs with 31 days in months with less days.
        /// </summary>
        private static bool CheckDayValidInMonth(int day, int month, int year)
        {
            try
            {
                DateTime dt = new DateTime(year, month, day);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }

}
