using System;

namespace Knightrunner.Library.Scheduling
{
    internal enum Sequence
    {
        Undefined,
        First,
        Second,
        Third,
        Fourth,
        Last
    }

    internal class CrontabEntry
    {
        public bool[] SecondFlags { get; set; }
        public bool[] MinuteFlags { get; set; }
        public bool[] HourFlags { get; set; }
        public bool[] MonthFlags { get; set; }
        public bool[] DayOfWeekFlags { get; set; }
        public bool[] DayOfMonthFlags { get; set; }
        public Sequence WeekSequence { get; set; }
        public Sequence MonthSequence { get; set; }
        public bool[] YearFlags { get; set; }

        public object Tag { get; set; }

        public static CrontabEntry Create(ScheduleEntry scheduleEntry)
        {
            CrontabEntry ceb = new CrontabEntry();

            // Initialize the flag arrays
            ceb.SecondFlags = new bool[60];
            ceb.MinuteFlags = new bool[60];
            ceb.HourFlags = new bool[24];
            ceb.MonthFlags = new bool[12];
            ceb.DayOfWeekFlags = new bool[7];
            ceb.DayOfMonthFlags = new bool[31];
            ceb.WeekSequence = Sequence.Undefined;
            ceb.MonthSequence = Sequence.Undefined;
            ceb.YearFlags = new bool[2500];

            ParseToken(scheduleEntry.Seconds, ceb.SecondFlags, false);
            ParseToken(scheduleEntry.Minutes, ceb.MinuteFlags, false);
            ParseToken(scheduleEntry.Hours, ceb.HourFlags, false);
            ParseToken(scheduleEntry.Months, ceb.MonthFlags, true);

            Sequence sequence;

            ParseToken(scheduleEntry.DaysOfWeek, ceb.DayOfWeekFlags, false, out sequence);
            ceb.WeekSequence = sequence;

            ParseToken(scheduleEntry.DaysOfMonth, ceb.DayOfMonthFlags, true, out sequence);
            ceb.MonthSequence = sequence;

            ParseToken(scheduleEntry.Years, ceb.YearFlags, false);

            ceb.Tag = scheduleEntry.Tag;

            return ceb;
        }

        private static void ParseToken(string token, bool[] arrayBool, bool beginInOne)
        {
            Sequence s;
            ParseToken(token, arrayBool, beginInOne, out s);
        }

        private static void ParseToken(string token, bool[] arrayBool, bool beginInOne, out Sequence sequence)
        {
            sequence = Sequence.Undefined;
            int each = 1;
            try 
            {
                // Look for step first
                int index = token.IndexOf("/");
                if(index > 0) 
                {
                    each = int.Parse(token.Substring(index + 1));
                    token = token.Substring(0,index);
                }
            
                if(token.Equals("*")) 
                {
                    for(int i=0; i<arrayBool.Length; i+=each) 
                    {
                        arrayBool[i] = true;
                    }
                    return;
                }

                index = token.IndexOf("L");
                if (index == 0)
                {
                    sequence = Sequence.Last;
                    return;
                }
                else if (index > 0)
                {
                    // xL   where x is 0-6 for day (e.g. 4L = last thursday)
                    sequence = Sequence.Last;
                    int value = int.Parse(token.Substring(0, index));
                    if (beginInOne)
                    {
                        value--;
                    }
                    arrayBool[value] = true;
                    return;
                }

                //Not needed? :
                //index = token.IndexOf("#");
                //if (index >= 0)
                //{
                //    // Week sequence
                //    int sequenceNumber = int.Parse(token[index + 1].ToString());
                //    if (sequenceNumber < 1 || sequenceNumber > 4)
                //    {
                //        throw new ArgumentException("Sequence number must be in the range 1-4");
                //    }
                //    sequence = (Sequence)sequenceNumber;
                //    return;
                //}

                index = token.IndexOf(",");
                if(index > 0) 
                {
                    string[] tokens = token.Split(",".ToCharArray());
                    for(int j = 0; j<tokens.Length; j++)
                    {
                        ParseToken(tokens[j], arrayBool, beginInOne, out sequence);
                    }
                    return;
                }
            
                index = token.IndexOf("-");
                if(index > 0) 
                {
                    int start = int.Parse(token.Substring(0, index));
                    int end = int.Parse(token.Substring(index + 1));

                    if(beginInOne) 
                    {
                        start--;
                        end--;
                    }

                    for(int j=start; j<=end; j+=each)
                        arrayBool[j] = true;
                    return;
                }
            
                int iValue = int.Parse(token);
                if(beginInOne) 
                {
                    iValue--;
                }
                arrayBool[iValue] = true;
                return;
            } 
            catch (Exception ex) 
            {
                throw new ArgumentException("Something was wrong with " + token, "token", ex);
            }
        }
    }
}
