using Knightrunner.WorkTrack2013.Contract;
using Knightrunner.WorkTrack2013.Database;
using Knightrunner.WorkTrack2013.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Knightrunner.WorkTrack2013.Service
{
    public class WorkTrack : IWorkTrack
    {
        public Project[] GetProjects()
        {
            using (var dataSource = CreateDataSource())
            {
                return dataSource.GetProjects().ToArray();
            }
        }

        public Project[] GetChildProjects(string publicId)
        {
            throw new NotImplementedException();
        }

        public void SaveProject(Project project)
        {
            throw new NotImplementedException();
        }

        public ActivityType[] GetActivityTypes()
        {
            throw new NotImplementedException();
        }

        public void SaveActivityType(ActivityType activityType)
        {
            throw new NotImplementedException();
        }

        public Reminder GetReminders(string userId)
        {
            throw new NotImplementedException();
        }

        public void SaveReminder(Reminder reminder)
        {
            throw new NotImplementedException();
        }

        public void DeleteReminder(long id)
        {
            throw new NotImplementedException();
        }

        public JournalEntry[] GetJournalEntries(string userId, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public void SaveJournalEntry(JournalEntry journalEntry)
        {
            throw new NotImplementedException();
        }

        public void DeleteJournalEntry(long id)
        {
            throw new NotImplementedException();
        }

        public Activity[] GetActivities(string userId, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public void SaveActivity(Activity activity)
        {
            throw new NotImplementedException();
        }

        public void DeleteActivity(long id)
        {
            throw new NotImplementedException();
        }

        private IDataSource CreateDataSource()
        {
            var cs = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
            if (string.IsNullOrEmpty(cs))
            {
                throw new ApplicationException("connectionString configuration missing or empty");
            }

            var providerName = System.Configuration.ConfigurationManager.AppSettings["providerName"];
            if (string.IsNullOrEmpty(providerName))
            {
                throw new ApplicationException("providerName configuration missing or empty");
            }

            return new DataSource(cs, providerName);
        }
    }
}
