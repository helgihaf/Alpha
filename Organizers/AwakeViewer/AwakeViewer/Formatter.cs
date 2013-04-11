using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwakeViewer
{
    internal static class Formatter
    {
        public static string FormatExceptionString(Exception exception)
        {
            StringBuilder sb = new StringBuilder();
            string indent = string.Empty;
            while (exception != null)
            {
                sb.AppendLine(indent + exception.GetType().FullName + " : " + exception.Message);
#if DEBUG
                sb.AppendLine(indent + exception.StackTrace);
#endif
                exception = exception.InnerException;
                indent += "  ";
            }

            return sb.ToString();
        }


        public static string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString("g");
        }

        public static string FormatHours(double hours)
        {
            return string.Format("{0:0.0}", hours);
        }




        public static string FormatDayOfWeek(DateTime dateTime)
        {
            return dateTime.ToString("ddd");
        }
    }
}
