using Knightrunner.WorkTrack2013.Contract;
using Knightrunner.WorkTrack2013.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Knightrunner.WorkTrack2013.Service
{
    public class WorkTrack : IWorkTrack
    {
        private Business.WorkTrackContext context;
        private Business.WorkTrack theBusiness;

        public Project[] GetProjects()
        {
            using (var business = GetBusiness())
            {
                return business.GetProjects().ConvertAll(Converters.ProjectToContract).ToArray();
            }
        }

        public Project[] GetChildProjects(string publicId)
        {
            using (var business = GetBusiness())
            {
                return business.GetChildProjects(publicId).ConvertAll(Converters.ProjectToContract).ToArray();
            }
        }

        public void SaveProject(Project project)
        {
            using (var business = GetBusiness())
            {
                business.SaveProject(Converters.ProjectToInterface(project));
            }
        }

        public ActivityType[] GetActivityTypes()
        {
            using (var business = GetBusiness())
            {
                return business.GetActivityTypes().ConvertAll(Converters.ActivityTypeToContract).ToArray();
            }
        }

        public void SaveActivityType(ActivityType activityType)
        {
            using (var business = GetBusiness())
            {
                business.SaveActivityType(Converters.ActivityTypeToInterface(activityType));
            }
        }

        public Reminder[] GetReminders(string userId)
        {
            using (var business = GetBusiness())
            {
                return business.GetReminders(userId).ConvertAll(Converters.ReminderToContract).ToArray();
            }
        }

        public void SaveReminder(Reminder reminder)
        {
            using (var business = GetBusiness())
            {
                business.SaveReminder(Converters.ReminderToInterface(reminder));
            }
        }

        public void DeleteReminder(long id)
        {
            using (var business = GetBusiness())
            {
                business.DeleteReminder(id);
            }
        }

        public JournalEntry[] GetJournalEntries(string userId, DateTime from, DateTime to)
        {
            using (var business = GetBusiness())
            {
                return business.GetJournalEntries(userId, from, to).ConvertAll(Converters.JournalEntryToContract).ToArray();
            }
        }

        public void SaveJournalEntry(JournalEntry journalEntry)
        {
            using (var business = GetBusiness())
            {
                business.SaveJournalEntry(Converters.JouralEntryToInterface(journalEntry));
            }
        }

        public void DeleteJournalEntry(long id)
        {
            using (var business = GetBusiness())
            {
                business.DeleteJournalEntry(id);
            }
        }

        public Activity[] GetActivities(string userId, DateTime from, DateTime to)
        {
            using (var business = GetBusiness())
            {
                return business.GetActivities(userId, from, to).ConvertAll(Converters.ActivityToContract).ToArray();
            }
        }

        public void SaveActivity(Activity activity)
        {
            using (var business = GetBusiness())
            {
                business.SaveActivity(Converters.ActivityToInterface(activity));
            }
        }

        public void DeleteActivity(long id)
        {
            using (var business = GetBusiness())
            {
                business.DeleteActivity(id);
            }
        }


        private Business.WorkTrack GetBusiness()
        {
            if (theBusiness == null)
            {
                theBusiness = new Business.WorkTrack(GetWorkTrackContext());
            }
            return theBusiness;
        }

        private Business.WorkTrackContext GetWorkTrackContext()
        {
            if (context == null)
            {
                var appSettings = System.Configuration.ConfigurationManager.AppSettings;

                string connectionString = appSettings["connectionString"];
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ConfigurationErrorsException("connectionString configuration missing or empty");
                }

                string providerName = appSettings["providerName"];
                if (string.IsNullOrEmpty(providerName))
                {
                    throw new ConfigurationErrorsException("providerName configuration missing or empty");
                }

                context = new Business.WorkTrackContext
                {
                    DataSourceFactory = new DataSourceFactory(connectionString, providerName)
                };
            }
            return context;
        }
    }
}
