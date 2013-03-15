using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Globalization;

namespace Knightrunner.Library.Core
{
    public sealed class ExceptionSummary
    {
        public ExceptionSummary()
            : this(null, true)
        {
        }

        public ExceptionSummary(Exception ex)
            : this(ex, true)
        {
        }

        public ExceptionSummary(Exception ex, bool includeStackTrace)
        {
            Lines = new List<ExceptionSummaryLine>();

            while (ex != null)
            {
                ExceptionSummaryLine line = new ExceptionSummaryLine();
                line.ExceptionType = ex.GetType();
                line.Message = ex.Message;
                line.Source = ex.Source;
                if (includeStackTrace)
                {
                    line.StackTrace = ex.StackTrace;
                }
                Lines.Add(line);

                ex = ex.InnerException;
            }
        }

        public List<ExceptionSummaryLine> Lines { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Lines.Count; i++)
            {
                sb.AppendLine(i + "> " + Lines[i].ToString());
            }

            return sb.ToString();
        }

        public string ToString(string format)
        {
            if (format == "x")
                return ToXml().ToString();
            else
                return ToString();
        }

        public XElement ToXml()
        {
            XElement root = new XElement("Exception");
            XElement element = root;
            foreach (var line in Lines)
            {
                var lineElement = line.ToXml("InnerException");
                element.Add(lineElement);
                element = lineElement;
            }

            return root;
        }
    }

    public sealed class ExceptionSummaryLine
    {
        public Type ExceptionType { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0}: {1}{2}{3}",
                ExceptionType.FullName,
                Message,
                Environment.NewLine,
                StackTrace
            );
        }


        public XElement ToXml()
        {
            return ToXml("Exception");
        }

        public XElement ToXml(string elementName)
        {
            return new XElement(elementName,
                new XElement("TypeName", ExceptionType.FullName),
                new XElement("Message", Message),
                new XElement("Source", Source),
                new XElement("StackTrace", StackTrace));
        }

    }
}
