using System;

namespace Knightrunner.Library.Scheduling
{
    //---------------------------------------------------------------------------------------------
    /// <summary>
    /// The ScheduleEntry class represents the points in time at which something occurrs. It closely
    /// follows the Unix cron scemantics.
    /// </summary>
    public class ScheduleEntry : ICloneable
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ScheduleEntry()
        {
            Seconds = "0";
            Hours = "*";
            Minutes = "*";
            Months = "*";
            DaysOfWeek = "*";
            DaysOfMonth = "*";
            Years = "*";
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Seconds, 0-59. Example values: "7" (on second 7), "*/5" (every 5 seconds), "30-50" (on seconds
        /// 30, 31,..,50), "41,51" (on seconds 41 and 51).
        /// </summary>
        public string Seconds { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Minutes, 0-59. Example values: "*" (every minute), "7" (on minute 7), "*/5" (every 5 minutes),
        /// "10-20" (on minutes 10,11,..,20), "10,40" (on minutes 10 and 40).
        /// </summary>
        public string Minutes { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Hours 0-23. Example values: "*" (every hour), "7" (on hour 7), "*/5" (every 5 hours), "9-17"
        /// (on hours 9,10,..,17), "11,15" (on hours 11 and 15).
        /// </summary>
        public string Hours { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Months, 1-12. Example values: "*" (every month), "7" (on month 7), "*/5" (every 5 months),
        /// "9-11" (on months 9,10,11,12), "1,7" (on months 1 and 7).
        /// </summary>
        public string Months { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Days of month, 1-N. Example values: "*" (every day), "7" (on the 7th), "*/5" (every 5 days
        /// within month), "9-12" (on days 9,10,11,12), "1,7" (on days 1 and 7), "L" (last day of
        /// every month).
        /// </summary>
        public string DaysOfMonth { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Days of week, 0-6, where 0 is Sunday. Example values: "*" (every day), "6" (on saturday),
        /// "*/2" (on every other day of the week), "1-6" (on mon,tue,wed,thu,fri), "1,6" (on
        /// monday and friday), "5L" (last friday of every week).
        /// </summary>
        public string DaysOfWeek { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Years, 0-2499. Example values: "*" (every year), "2010" (on year 2010), "*/5" (every 5 years),
        /// "2009-2012" (on years 2009,2010,2011), "2012,2013" (on eyars 2012 and 2013).
        /// </summary>
        public string Years { get; set; }


        public object Tag { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Parses a standard cron string into the schedule.
        /// </summary>
        /// <param name="cronString">Standard cron string.</param>
        /// <returns>A new schedule object based on the specifed string.</returns>
        /// <remarks>
        /// A cron string is made up of a series of fields separated by a space. 
        /// There are normally seven fields in one entry but here only five fields
        //  will be used. The fields are:
        /// second minute hour dom month dow 
        /// 
        /// second  This controls what second of the minute the command will run on,
        ///         and is between '0' and '59'
        ///	minute	This controls what minute of the hour the command will run on,
        ///			and is between '0' and '59'
        ///	hour	This controls what hour the command will run on, and is specified in
        ///			the 24 hour clock, values must be between 0 and 23 (0 is midnight)
        ///	dom		This is the Day of Month, that you want the command run on, e.g. to
        ///			run a command on the 19th of each month, the dom would be 19.
        ///	month	This is the month a specified command will run on, it may be specified
        ///			numerically (1-12), or as the name of the month (e.g. May)
        ///	dow		This is the Day of Week that you want a command to be run on, it can
        ///			also be numeric (0-6) , with sunday as 0 and so on.
        ///
        ///	If you don't wish to specify a value for a field, just place a * in the 
        ///	field.
        ///
        ///	e.g.
        ///	*/5 * * * * *	"Is run every 5 seconds"
        ///	0 01 * * * *	"Is run at one min past every hour"
        ///	0 17 8 * * *	"Is run daily at 8:17:00 am"
        ///	0 17 20 * * *	"Is run daily at 8:17:00 pm"
        ///	0 00 4 * * 0	"Is run at 4 am every Sunday"
        ///	5 42 4 1 * *	"Is run 4:42:05 am every 1st of the month"
        ///	0 01 * 19 07 *	"Is run hourly on the 19th of July"
        ///
        ///	If both the dom and dow are specified, the run time is when both apply ( watch out!)
        ///
        /// * 12 16 * 1		"Is run only when its the 16th and its monday ( next monday 16th )
        ///
        /// The Cron also accepts lists in the fields. Lists can be in the form, 1,2,3 
        ///	(meaning 1 and 2 and 3) or 1-3 (also meaning 1 and 2 and 3).
        ///
        /// 0 59 11 * * 1,2,3,4,5  Will run at 11:59:00 Monday, Tuesday, Wednesday, Thursday and Friday,
        ///	as will:
        /// 0 59 11 * * 1-5
        ///
        ///	Cron also supports 'step' values.
        ///	A value of */2 in the dom field would mean the command runs every two days
        /// and likewise, */5 in the hours field would mean the command runs every 
        ///	5 hours.
        ///	e.g. 
        ///	0 * 12 10-16/2 * * is the same as:
        /// 0 * 12 10,12,14,16 * * 
        ///
        /// 0 */15 9-17 * * * Will run  every 15 mins between the hours or 9am and 5pm
        /// </remarks>
        /// <exception cref="ArgumentException">cronString is null, empty or is incorrectly
        /// formatted</exception>
        public static ScheduleEntry FromChronString(string cronString)
        {
            if (string.IsNullOrEmpty(cronString))
            {
                throw new ArgumentException("cronString");
            }

            ScheduleEntry schedule = new ScheduleEntry();

            string[] tokens = cronString.Split(" \t".ToCharArray());
            int numTokens = tokens.Length;
            
            // Must have at least 6 token
            if (numTokens < 6)
            {
                throw new ArgumentException("The number of items must be 6 or more ", "cronString");
            }

            for (int i = 0; i < numTokens; i++)
            {
                string token = tokens[i];
                switch (i)
                {
                    // Seconds
                    case 0:
                        schedule.Seconds = token;
                        break;
                    case 1:     // Minutes
                        schedule.Minutes = token;
                        break;
                    case 2:     // Hours
                        schedule.Hours = token;
                        break;
                    case 3:     // Days of month
                        schedule.DaysOfMonth = token;
                        break;
                    case 4:     // Months
                        schedule.Months = token;
                        break;
                    case 5:     // Days of week
                        schedule.DaysOfWeek = token;
                        break;
                    case 6:     // Years
                        schedule.Years = token;
                        break;

                    default:
                        break;
                }
            }


            return schedule;
        }


        #region ICloneable Members

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
