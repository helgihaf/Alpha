using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.WorkTrack2013.Database
{
    public static class Converters
    {
        public static readonly Converter<Entities.Project, Interface.Project> ProjectToInterface =
            delegate(Entities.Project data)
            {
                if (data == null)
                {
                    return null;
                }

                var conv = new Interface.Project();

                conv.PublicId = data.PublicId;
                conv.Text = data.Text;
                conv.Active = data.Active;

                return conv;
            };

        public static readonly Converter<Interface.Project, Entities.Project> ProjectToEntity =
            delegate(Interface.Project data)
            {
                if (data == null)
                {
                    return null;
                }

                var conv = new Entities.Project();
                CopyToEntity(conv, data);
                return conv;
            };

        public static void CopyToEntity(Entities.Project conv, Interface.Project data)
        {
            conv.PublicId = data.PublicId;
            conv.Text = data.Text;
            conv.Active = data.Active;
        }

        public static readonly Converter<Entities.ActivityType, Interface.ActivityType> ActivityTypeToInterface =
            delegate(Entities.ActivityType data)
            {
                if (data == null)
                {
                    return null;
                }

                var conv = new Interface.ActivityType();

                conv.PublicId = data.PublicId;
                conv.Text = data.Text;
                conv.Active = data.Active;

                return conv;
            };

        public static readonly Converter<Interface.ActivityType, Entities.ActivityType> ActivityTypeToEntity =
            delegate(Interface.ActivityType data)
            {
                if (data == null)
                {
                    return null;
                }

                var conv = new Entities.ActivityType();
                CopyToEntity(conv, data);
                return conv;
            };

        public static void CopyToEntity(Entities.ActivityType conv, Interface.ActivityType data)
        {
            conv.PublicId = data.PublicId;
            conv.Text = data.Text;
            conv.Active = data.Active;
        }

        public static readonly Converter<Entities.Reminder, Interface.Reminder> ReminderToInterface =
            delegate(Entities.Reminder data)
            {
                if (data == null)
                {
                    return null;
                }

                var conv = new Interface.Reminder();
                CopyToInterface(conv, data);
                return conv;
            };

        public static void CopyToInterface(Interface.Reminder conv, Entities.Reminder data)
        {
            conv.Text = data.Text;
            conv.Active = data.Active;
            conv.Seconds = data.Seconds;
            conv.Minutes = data.Minutes;
            conv.Hours = data.Hours;
            conv.Months = data.Months;
            conv.DaysOfMonth = data.DaysOfMonth;
            conv.DaysOfWeek = data.DaysOfWeek;
            conv.Years = data.Years;
        }

        public static void CopyToEntity(Entities.Reminder conv, Interface.Reminder data)
        {
            conv.Text = data.Text;
            conv.Active = data.Active;
            conv.Seconds = data.Seconds;
            conv.Minutes = data.Minutes;
            conv.Hours = data.Hours;
            conv.Months = data.Months;
            conv.DaysOfMonth = data.DaysOfMonth;
            conv.DaysOfWeek = data.DaysOfWeek;
            conv.Years = data.Years;
        }


        public static readonly Converter<Entities.JournalEntry, Interface.JournalEntry> JournalEntryToInterface =
            delegate(Entities.JournalEntry data)
            {
                if (data == null)
                {
                    return null;
                }

                var conv = new Interface.JournalEntry();
                CopyToInterface(conv, data);
                return conv;
            };

        private static void CopyToInterface(Interface.JournalEntry conv, Entities.JournalEntry data)
        {
            conv.Id = data.Id;
            conv.DateTime = data.DateTime;
            conv.Type = (Interface.JournalEntryType)data.Type;
            conv.TypeOrigin = (Interface.JournalEntryTypeOrigin)data.TypeOrigin;
            conv.Text = data.Text;
        }

        internal static void CopyToEntity(Entities.JournalEntry conv, Interface.JournalEntry data)
        {
            conv.Id = data.Id;
            conv.DateTime = data.DateTime;
            conv.Type = (int)data.Type;
            conv.TypeOrigin = (int)data.TypeOrigin;
            conv.Text = data.Text;
        }

        public static readonly Converter<Entities.ActivityQueryResult, Interface.Activity> ActivityQueryResultToInterface =
            delegate(Entities.ActivityQueryResult data)
            {
                if (data == null)
                {
                    return null;
                }

                var conv = new Interface.Activity();
                CopyToInterface(conv, data);
                return conv;
            };

        private static void CopyToInterface(Interface.Activity conv, Entities.ActivityQueryResult data)
        {
            conv.Id = data.Id;
            conv.UserId = data.UserId;
            conv.Start = data.Start;
            if (data.DurationSeconds.HasValue)
            {
                conv.Duration = TimeSpan.FromSeconds(data.DurationSeconds.Value);
            }
            conv.ProjectPublicId = data.ProjectPublicId;
            conv.ActivityTypePublicId = data.ActivityTypePublicId;
            conv.Text = data.Text;
        }

        public static void CopyToEntity(Entities.Activity conv, Interface.Activity data)
        {
            conv.Id = data.Id;
            conv.Start = data.Start;
            if (data.Duration.HasValue)
            {
                conv.DurationSeconds = Convert.ToInt32(data.Duration.Value.TotalSeconds);
            }
            conv.Text = data.Text;
        }
    }
}
