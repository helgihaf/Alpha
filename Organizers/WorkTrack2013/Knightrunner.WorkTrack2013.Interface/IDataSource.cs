using System;
using System.Collections.Generic;

namespace Knightrunner.WorkTrack2013.Interface
{
    public interface IDataSource : IDisposable
    {
        List<Project> GetProjects();
        List<Project> GetChildProjects(string publicId);
        Project GetProject(string publicId);
        void SaveProject(Project project);

        List<ActivityType> GetActivityTypes();
        void SaveActivityType(ActivityType activityType);

        List<Reminder> GetReminders(string publicUserId);
        void SaveReminder(Reminder reminder);
        void DeleteReminder(long id);

        List<JournalEntry> GetJournalEntries(string userId, DateTime from, DateTime to);
        void SaveJournalEntry(JournalEntry journalEntry);
        void DeleteJournalEntry(long id);

        List<Activity> GetActivities(string userId, DateTime from, DateTime to);
        void SaveActivity(Activity activity);
        void DeleteActivity(long id);
    }
}
