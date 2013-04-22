using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Knightrunner.WorkTrack2013.Contract
{
    [DataContract]
    public class Reminder
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public bool Enabled { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Seconds, 0-59. Example values: "7" (on second 7), "*/5" (every 5 seconds), "30-50" (on seconds
        /// 30, 31,..,50), "41,51" (on seconds 41 and 51).
        /// </summary>
        [DataMember]
        public string Seconds { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Minutes, 0-59. Example values: "*" (every minute), "7" (on minute 7), "*/5" (every 5 minutes),
        /// "10-20" (on minutes 10,11,..,20), "10,40" (on minutes 10 and 40).
        /// </summary>
        [DataMember]
        public string Minutes { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Hours 0-23. Example values: "*" (every hour), "7" (on hour 7), "*/5" (every 5 hours), "9-17"
        /// (on hours 9,10,..,17), "11,15" (on hours 11 and 15).
        /// </summary>
        [DataMember]
        public string Hours { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Months, 1-12. Example values: "*" (every month), "7" (on month 7), "*/5" (every 5 months),
        /// "9-11" (on months 9,10,11,12), "1,7" (on months 1 and 7).
        /// </summary>
        [DataMember]
        public string Months { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Days of month, 1-N. Example values: "*" (every day), "7" (on the 7th), "*/5" (every 5 days
        /// within month), "9-12" (on days 9,10,11,12), "1,7" (on days 1 and 7), "L" (last day of
        /// every month).
        /// </summary>
        [DataMember]
        public string DaysOfMonth { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Days of week, 0-6, where 0 is Sunday. Example values: "*" (every day), "6" (on saturday),
        /// "*/2" (on every other day of the week), "1-6" (on mon,tue,wed,thu,fri), "1,6" (on
        /// monday and friday), "5L" (last friday of every week).
        /// </summary>
        [DataMember]
        public string DaysOfWeek { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Years, 0-2499. Example values: "*" (every year), "2010" (on year 2010), "*/5" (every 5 years),
        /// "2009-2012" (on years 2009,2010,2011), "2012,2013" (on eyars 2012 and 2013).
        /// </summary>
        [DataMember]
        public string Years { get; set; }
    }
}