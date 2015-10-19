using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Knightrunner.WorkTrack2013.Business
{
    [ServiceContract(Namespace=Constants.ServiceNamespace)]
    public interface IWorkTrack
    {
        //
        // Projects
        //

        [OperationContract]
        Project[] GetProjects();

        [OperationContract]
        Project[] GetChildProjects(string publicId);

        [OperationContract]
        void SaveProject(Project project);

        //
        // ActivityTypes
        //

        [OperationContract]
        ActivityType[] GetActivityTypes();

        [OperationContract]
        void SaveActivityType(ActivityType activityType);

        //
        // Reminders
        //

        [OperationContract]
        Reminder GetReminders(string userId);

        [OperationContract]
        void SaveReminder(Reminder reminder);
        
        [OperationContract]
        void DeleteReminder(long id);

        //
        // Journal entries
        //

        [OperationContract]
        JournalEntry[] GetJournalEntries(string userId, DateTime from, DateTime to);

        [OperationContract]
        void SaveJournalEntry(JournalEntry journalEntry);

        [OperationContract]
        void DeleteJournalEntry(long id);

        //
        // Activities
        //

        [OperationContract]
        Activity[] GetActivities(string userId, DateTime from, DateTime to);
        
        [OperationContract]
        void SaveActivity(Activity activity);

        [OperationContract]
        void DeleteActivity(long id);
    }
}
