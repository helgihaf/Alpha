using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.WorkTrack2013.Database
{
    public class DataSource : Interface.IDataSource
    {
        private PetaPoco.Database database;

        public DataSource(string connectionString, string providerName)
        {
            database = new PetaPoco.Database(connectionString, providerName);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (database != null)
                {
                    database.Dispose();
                    database = null;
                }
            }
        }

        public List<Interface.Project> GetProjects()
        {
            var projects = database.Fetch<Entities.Project>("").ToDictionary(p => p.Id);
            var result = new List<Interface.Project>(projects.Count);
            foreach (var project in projects.Values)
            {
                var interfaceProject = Converters.ProjectToInterface(project);
                if (project.ParentId.HasValue)
                {
                    interfaceProject.ParentProjectPublicId = projects[project.ParentId.Value].PublicId;
                }
                result.Add(interfaceProject);
            }

            return result;
        }

        public List<Interface.Project> GetChildProjects(string publicId)
        {
            var sql = PetaPoco.Sql.Builder
                .Append("SELECT ch.*")
                .Append("FROM " + Entities.Project._Names._Table + " ch")
                .Append("INNER JOIN " + Entities.Project._Names._Table + " par ON ch." + Entities.Project._Names.ParentId + " = par." + Entities.Project._Names.Id)
                .Append("WHERE par." + Entities.Project._Names.PublicId + " = @0", publicId);

            var childProjects = database.Fetch<Entities.Project>(sql);
            var result = new List<Interface.Project>(childProjects.Count);
            foreach (var project in childProjects)
            {
                var interfaceProject = Converters.ProjectToInterface(project);
                interfaceProject.ParentProjectPublicId = publicId;
                result.Add(interfaceProject);
            }

            return result;
        }

        public Interface.Project GetProject(string publicId)
        {
            return Converters.ProjectToInterface(GetProjectByPublicId(publicId));
        }

        public void SaveProject(Interface.Project project)
        {
            long? parentId = null;
            if (!string.IsNullOrEmpty(project.ParentProjectPublicId))
            {
                parentId = GetProjectIdByPublicIdAsserted(project.ParentProjectPublicId);
            }

            Entities.Project entity = GetProjectByPublicId(project.PublicId);
            if (entity == null)
            {
                entity = new Entities.Project();
            }

            Converters.CopyToEntity(entity, project);
            entity.ParentId = parentId;

            database.Save(entity);
        }

        private long GetProjectIdByPublicIdAsserted(string publicId)
        {
            var parentId = GetProjectIdByPublicId(publicId);
            if (parentId == null)
            {
                throw new System.Data.DataException(string.Format("Parent project with public ID {0} not found.", publicId));
            }
            return parentId.Value;
        }

        private long? GetProjectIdByPublicId(string publicId)
        {
            var sql = PetaPoco.Sql.Builder
                .Select(Entities.Project._Names.Id)
                .From(Entities.Project._Names._Table)
                .Where(Entities.Project._Names.PublicId + " = @0", publicId);
            return database.SingleOrDefault<long>(sql);
        }

        private Entities.Project GetProjectByPublicId(string publicId)
        {
            var sql = PetaPoco.Sql.Builder
                .Where(Entities.Project._Names.PublicId + " = @0", publicId);
            return database.SingleOrDefault<Entities.Project>(sql);
        }

        public List<Interface.ActivityType> GetActivityTypes()
        {
            return database.Fetch<Entities.ActivityType>("").ConvertAll(Converters.ActivityTypeToInterface);
        }

        public void SaveActivityType(Interface.ActivityType activityType)
        {
            Entities.ActivityType entity = GetActivityTypeByPublicId(activityType.PublicId);
            if (entity == null)
            {
                entity = new Entities.ActivityType();
            }
            Converters.CopyToEntity(entity, activityType);

            database.Save(entity);
        }

        private Entities.ActivityType GetActivityTypeByPublicId(string publicId)
        {
            var sql = PetaPoco.Sql.Builder
                .Where(Entities.ActivityType._Names.PublicId + " = @0", publicId);
            return database.SingleOrDefault<Entities.ActivityType>(sql);
        }

        private long GetActivityTypeIdByPublicIdAsserted(string publicId)
        {
            var sql = PetaPoco.Sql.Builder
                .Select(Entities.ActivityType._Names.Id)
                .From(Entities.ActivityType._Names._Table)
                .Where(Entities.ActivityType._Names.PublicId + " = @0", publicId);
            var result = database.SingleOrDefault<long>(sql);
            if (result == 0)
            {
                throw new System.Data.DataException(string.Format("Activity type with public ID {0} not found.", publicId));
            }
            return result;
        }

        public List<Interface.Reminder> GetReminders(string publicUserId)
        {
            Entities.User user = GetUserByPublicIdAsserted(publicUserId);

            var sql = PetaPoco.Sql.Builder.Where(Entities.Reminder._Names.UserId + " = @0", user.Id);
            var result = database.Fetch<Entities.Reminder>(sql).ConvertAll(Converters.ReminderToInterface);
            
            foreach (var reminder in result)
            {
                reminder.UserId = user.PublicId;
            }

            return result;
        }

        private Entities.User GetUserByPublicIdAsserted(string publicId)
        {
            var user = GetUserByPublicId(publicId);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new System.Data.DataException(string.Format("User with public ID {0} not found.", publicId));
            }
        }

        private Entities.User GetUserByPublicId(string publicId)
        {
            var sql = PetaPoco.Sql.Builder.Where(Entities.User._Names.PublicId + " = @0", publicId);
            return database.SingleOrDefault<Entities.User>(sql);
        }

        private long GetUserIdByPublicIdAsserted(string publicId)
        {
            var sql = PetaPoco.Sql.Builder
                .Select(Entities.User._Names.Id)
                .From(Entities.User._Names._Table)
                .Where(Entities.User._Names.PublicId + " = @0", publicId);
            var userId = database.SingleOrDefault<long>(sql);
            if (userId != 0)
            {
                return userId;
            }
            else
            {
                throw new System.Data.DataException(string.Format("User with public ID {0} not found.", publicId));
            }
        }

        public void SaveReminder(Interface.Reminder reminder)
        {
            Entities.Reminder entity = GetReminderById(reminder.Id);
            if (entity == null)
            {
                entity = new Entities.Reminder();
            }

            long userId = GetUserIdByPublicIdAsserted(reminder.UserId);

            Converters.CopyToEntity(entity, reminder);
            entity.UserId = userId;
            database.Save(entity);
        }

        private Entities.Reminder GetReminderById(long id)
        {
            var sql = PetaPoco.Sql.Builder.Where(Entities.Reminder._Names.Id + " = @0", id);
            return database.SingleOrDefault<Entities.Reminder>(sql);
        }

        public void DeleteReminder(long id)
        {
            database.Delete<Entities.Reminder>(id);
        }

        public List<Interface.JournalEntry> GetJournalEntries(string userId, DateTime from, DateTime to)
        {
            var entityUser = GetUserByPublicId(userId);
            if (entityUser == null)
            {
                return new List<Interface.JournalEntry>();
            }

            var sql = PetaPoco.Sql.Builder
                .Append("WHERE " + Entities.JournalEntry._Names.UserId + " = @0", entityUser.Id)
                .Append("AND " + Entities.JournalEntry._Names.DateTime + " BETWEEN @0 AND @1", from, to);
            var result = database.Fetch<Entities.JournalEntry>(sql).ConvertAll(Converters.JournalEntryToInterface);
            foreach (var journalEntry in result)
            {
                journalEntry.UserId = entityUser.PublicId;
            }

            return result;
        }

        public void SaveJournalEntry(Interface.JournalEntry journalEntry)
        {
            Entities.JournalEntry entity = GetJournalEntryById(journalEntry.Id);
            if (entity == null)
            {
                entity = new Entities.JournalEntry();
            }

            long userId = GetUserIdByPublicIdAsserted(journalEntry.UserId);

            Converters.CopyToEntity(entity, journalEntry);
            entity.UserId = userId;
            database.Save(entity);
        }

        private Entities.JournalEntry GetJournalEntryById(long id)
        {
            var sql = PetaPoco.Sql.Builder.Where(Entities.JournalEntry._Names.Id + " = @0", id);
            return database.SingleOrDefault<Entities.JournalEntry>(sql);
        }

        public void DeleteJournalEntry(long id)
        {
            database.Delete<Entities.JournalEntry>(id);
        }

        public List<Interface.Activity> GetActivities(string userId, DateTime from, DateTime to)
        {
            var sql = PetaPoco.Sql.Builder
                .Append("SELECT")
                    .Append("act." + Entities.Activity._Names.Id)
                    .Append(", usr." + Entities.User._Names.PublicId + " AS UserId")
                    .Append(", act." + Entities.Activity._Names.Start)
                    .Append(", act." + Entities.Activity._Names.DurationSeconds)
                    .Append(", prj." + Entities.Project._Names.PublicId + " AS ProjectPublicId")
                    .Append(", atp." + Entities.ActivityType._Names.PublicId + " AS ActivityPublicId")
                    .Append(", act." + Entities.Activity._Names.Text)
                .Append("FROM " + Entities.Activity._Names._Table + " AS act")
                    .Append("INNER JOIN " + Entities.User._Names._Table + " AS usr ON act." + Entities.Activity._Names.UserId + " = usr." + Entities.User._Names.Id)
                    .Append("LEFT OUTER JOIN " + Entities.Project._Names._Table + " AS prj ON act." + Entities.Activity._Names.ProjectId + " = prj." + Entities.Project._Names.Id)
                    .Append("LEFT OUTER JOIN " + Entities.ActivityType._Names._Table + " AS atp ON act." + Entities.Activity._Names.ActivityTypeId + " = atp." + Entities.ActivityType._Names.Id)
                .Append("WHERE usr." + Entities.User._Names.PublicId + " = @0", userId)
                    .Append("AND act." + Entities.Activity._Names.Start + " BETWEEN @0 AND @1", from, to);

            return database.Fetch<Entities.ActivityQueryResult>(sql).ConvertAll(Converters.ActivityQueryResultToInterface);
        }

        public void SaveActivity(Interface.Activity activity)
        {
            var entityUserId = GetUserIdByPublicIdAsserted(activity.UserId);
            
            long? entityProjectId = null;
            if (!string.IsNullOrWhiteSpace(activity.ProjectPublicId))
            {
                entityProjectId = GetProjectIdByPublicIdAsserted(activity.ProjectPublicId);
            }

            long? entityActivityTypeId = null;
            if (!string.IsNullOrWhiteSpace(activity.ActivityTypePublicId))
            {
                entityActivityTypeId = GetActivityTypeIdByPublicIdAsserted(activity.ActivityTypePublicId);
            }

            var entity = database.SingleOrDefault<Entities.Activity>(activity.Id);
            if (entity == null)
            {
                entity = new Entities.Activity();
            }

            Converters.CopyToEntity(entity, activity);
            entity.UserId = entityUserId;
            entity.ProjectId = entityProjectId;
            entity.ActivityTypeId = entityActivityTypeId;
            database.Save(entity);
        }

        public void DeleteActivity(long id)
        {
            database.Delete<Entities.Activity>(id);
        }
    }
}
