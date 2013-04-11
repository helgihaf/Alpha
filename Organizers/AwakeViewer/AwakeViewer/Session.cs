using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AwakeViewer
{
	public class Session
	{
		public DateTime Start { get; set; }
		public DateTime? End { get; set; }

		//public TimeSpan Duration
		//{
		//    get
		//    {
		//        if (End.HasValue)
		//        {
		//            return End.Value - Start;
		//        }
		//        else
		//        {
		//            return TimeSpan.Zero;
		//        }
		//    }
		//}

		[XmlIgnore]
		public decimal Duration
		{
			get
			{
				if (End.HasValue)
				{
					return Convert.ToDecimal(Math.Round((End.Value - Start).TotalHours,1));
				}
				else
				{
					return 0;
				}
			}
		}

		public SessionCategory Category { get; set; }
		public string Text { get; set; }

        internal bool IsWeekend
        {
            get
            {
                return Start.DayOfWeek == DayOfWeek.Saturday || Start.DayOfWeek == DayOfWeek.Sunday;
            }
        }

        public bool IsOffHours
        {
            get
            {
                return End.HasValue && (End.Value.Hour > 21 || End.Value.Hour < 6);
            }
        }
    }


	public enum SessionCategory
	{
		New,
		Work,
		Other
	}
}
