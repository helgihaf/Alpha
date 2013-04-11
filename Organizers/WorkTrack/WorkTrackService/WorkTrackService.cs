using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Knightrunner.Library.Database;
using System.Reflection;
using Knightrunner.WorkTrack.Model;
using Database = Knightrunner.WorkTrack.Database;
using Knightrunner.WorkTrack.Database;

namespace WorkTrackService
{
    public class WorkTrackService : IWorkTrackService
    {

        /// <summary>
        /// Initialize database connection parameters.
        /// </summary>
        static WorkTrackService()
        {
            ConnectionScope.DefaultSettings.ConnectionString = "Data Source=lap-helgih2;Initial Catalog=WorkTrack;Integrated Security=SSPI";
        }

        #region Bridge

        private class PropertyInfoPair
        {
            public PropertyInfo Source;
            public PropertyInfo Target;
        }

        private IEnumerable<TTarget> ConvertObjects<TTarget, TSource>(IEnumerable<TSource> sources) where TTarget: new()
        {
            var propertyInfos = GetPropertyInfos<TTarget, TSource>();

            foreach (var source in sources)
            {
                yield return ConvertObject<TTarget, TSource>(propertyInfos, source);
            }
        }


        private TTarget ConvertObject<TTarget, TSource>(TSource source) where TTarget : new()
        {
            var propertyInfos = GetPropertyInfos<TTarget, TSource>();
            return ConvertObject<TTarget, TSource>(propertyInfos, source);
        }

        private static IEnumerable<PropertyInfoPair> GetPropertyInfos<TTarget, TSource>() where TTarget : new()
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public;

            var sourcePropertyInfos =
                from propInfo in typeof(TSource).GetProperties(bindingFlags)
                where propInfo.GetCustomAttributes(true).Any(ca => ca is System.Data.Linq.Mapping.ColumnAttribute || ca is System.Runtime.Serialization.DataMemberAttribute)
                select propInfo;

            var propertyInfos =
                from propInfo in typeof(TTarget).GetProperties(bindingFlags)
                join sourcePropInfo in sourcePropertyInfos on propInfo.Name equals sourcePropInfo.Name
                where propInfo.GetCustomAttributes(true).Any(ca => ca is System.Runtime.Serialization.DataMemberAttribute || ca is System.Data.Linq.Mapping.ColumnAttribute)
                select new PropertyInfoPair { Target = propInfo, Source = sourcePropInfo };
            return propertyInfos;
        }


        private TTarget ConvertObject<TTarget, TSource>(IEnumerable<PropertyInfoPair> propertyInfoPairs, TSource source) where TTarget : new()
        {
            TTarget target = new TTarget();
            foreach (var propertyInfoPair in propertyInfoPairs)
            {
                propertyInfoPair.Target.SetValue(target, propertyInfoPair.Source.GetValue(source, null), null);
            }

            return target;
        }

        //private void ApplyPropertyChanges<TContractObject, TDatabaseObject>(Database.WorkTrackDataContext context, TDatabaseObject existing, TContractObject updated) where TDatabaseObject : new()
        //{
        //    try
        //    {
        //        foreach (var propertyInfoPair in GetPropertyInfos<TDatabaseObject, TContractObject>())
        //        {
        //            object existingValue = propertyInfoPair.Target.GetValue(existing, null);
        //            object updatedValue = propertyInfoPair.Source.GetValue(updated, null);
        //            if (!object.Equals(existingValue, updatedValue))
        //            {
        //                propertyInfoPair.Target.SetValue(existing, updatedValue, null);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.GetType().FullName + " " + ex.Message);
        //        throw;
        //    }

        //}

        private void ApplyPropertyChanges<T>(Database.WorkTrackDataContext context, T updated, T existing) where T : new()
        {
            try
            {
                
                foreach (var propertyInfo in GetPropertyInfos(typeof(T)))
                {
                    object existingValue = propertyInfo.GetValue(existing, null);
                    object updatedValue = propertyInfo.GetValue(updated, null);
                    if (!object.Equals(existingValue, updatedValue))
                    {
                        propertyInfo.SetValue(existing, updatedValue, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName + " " + ex.Message);
                throw;
            }

        }

        private IEnumerable<PropertyInfo> GetPropertyInfos(Type type)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(System.Data.Linq.Mapping.ColumnAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    yield return property;
                }
            }
        }

        #endregion

        #region Project

        public Project[] GetProjects()
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                var items = context.Projects.AsEnumerable().ToArray();
                foreach (var item in items)
                {
                    item.Detach();
                }

                return items;
            }
        }


        public Project GetProject(Guid id)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                var item = context.Projects.FirstOrDefault(proj => proj.Id == id);
                if (item != null)
                {
                    item.Detach();
                }

                return item;
            }
        }

        
        public Project CreateProject()
        {
            var project = new Project();
            project.Active = true;

            return project;
        }

        public void InsertProject(Project project)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                context.Projects.InsertOnSubmit(project);
                context.SubmitChanges();
            }
        }

        public void UpdateProject(Project updatedProject, Project originalProject)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                context.Projects.Attach(updatedProject);
                context.SubmitChanges();
            }
        }

        public void DeleteProject(Project project)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                context.Projects.Attach(project);
                context.Projects.DeleteOnSubmit(project);
                context.SubmitChanges();
            }
        }

        #endregion


        #region Activity

        public Activity[] GetActivities()
        {
            using (var cs = new ConnectionScope())
            {
                Activity[] items;
                using (Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection))
                {
                    items = context.Activities.AsEnumerable().ToArray();
                }

                foreach (var item in items)
                {
                    item.Detach();
                }

                return items;
            }
        }


        public Activity GetActivity(Guid id)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                var item = context.Activities.FirstOrDefault(activity => activity.Id == id);
                if (item != null)
                {
                    item.Detach();
                }

                return item;
            }
        }


        public Activity CreateActivity()
        {
            var activity = new Activity();
            activity.Active = true;

            return activity;
        }

        public void InsertActivity(Activity activity)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                context.Activities.InsertOnSubmit(activity);
                context.SubmitChanges();
            }
        }

        public void UpdateActivity(Activity updatedActivity, Activity originalActivity)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);

                context.Activities.Attach(originalActivity);
                ApplyPropertyChanges(context, updatedActivity, originalActivity);
                context.SubmitChanges();
            }
        }


        public void DeleteActivity(Activity activity)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                context.Activities.Attach(activity);
                context.Activities.DeleteOnSubmit(activity);

                context.SubmitChanges();
            }
        }

        #endregion

        #region User

        public User[] GetUsers()
        {
            User[] items;
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                items = context.Users.AsEnumerable().ToArray();
            }

            foreach (var item in items)
            {
                item.Detach();
            }

            return items;
        }


        public User GetUser(Guid id)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                var item = context.Users.FirstOrDefault(user => user.Id == id);
                if (item != null)
                {
                    item.Detach();
                }

                return item;
            }
        }


        public User CreateUser()
        {
            var user = new User();
            user.Active = true;

            return user;
        }

        public void InsertUser(User user)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                context.Users.InsertOnSubmit(user);
                context.SubmitChanges();
            }
        }

        public void UpdateUser(User updatedUser, User originalUser)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                context.Users.Attach(updatedUser);
                context.SubmitChanges();
            }
        }

        public void SetUserPassword(Guid userId, byte[] oldPassword, byte[] newPassword)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);

                var user = context.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    throw new ArgumentException("User ID not found");
                }

                var hashedOldPassword = HashPassword(oldPassword);
                if (StringComparer.InvariantCulture.Compare(user.Password, hashedOldPassword) != 0)
                {
                    throw new ArgumentException("Old password wrong");
                }

                if (!ValidatePassword(newPassword))
                {
                    throw new ArgumentException("New password is not valid");
                }
                var hashedNewPassword = HashPassword(newPassword);
                user.Password = hashedNewPassword;

                context.SubmitChanges();
            }
        }

        private string HashPassword(byte[] password)
        {
            if (password == null || password.Length == 0)
            {
                return null;
            }

            var hashAlgorithm = new System.Security.Cryptography.SHA512Managed();
            var hashedPassword = hashAlgorithm.ComputeHash(password);
            return Convert.ToBase64String(hashedPassword);
        }

        private bool ValidatePassword(byte[] newPassword)
        {
            return newPassword != null && newPassword.Length > 8;
        }

        #endregion


        #region WorkEntry

        public WorkEntry[] GetWorkEntries()
        {
            WorkEntry[] items;
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                items = context.WorkEntries.AsEnumerable().ToArray();
            }

            foreach (var item in items)
            {
                item.Detach();
            }

            return items;
        }


        public WorkEntry[] GetWorkEntriesByUser(Guid userId)
        {
            WorkEntry[] items;
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                var workEntries =
                    from workEntry in context.WorkEntries
                    where workEntry.User == userId
                    select workEntry;
                items = workEntries.ToArray();
            }

            foreach (var item in items)
            {
                item.Detach();
            }

            return items;
        }



        public WorkEntry[] GetWorkEntriesByUserDate(Guid userId, DateTime fromDate, DateTime toDate)
        {
            WorkEntry[] items;
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                var workEntries =
                    from workEntry in context.WorkEntries
                    where workEntry.User == userId && workEntry.Start >= fromDate && (workEntry.End == null || workEntry.End < toDate)
                    select workEntry;
                items = workEntries.ToArray();
            }

            foreach (var item in items)
            {
                item.Detach();
            }

            return items;
        }

        
        public WorkEntry GetWorkEntry(Guid id)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                var item = context.WorkEntries.FirstOrDefault(workEntry => workEntry.Id == id);
                if (item != null)
                {
                    item.Detach();
                }

                return item;
            }
        }


        public WorkEntry CreateWorkEntry()
        {
            var workEntry = new WorkEntry();

            return workEntry;
        }

        public void InsertWorkEntry(WorkEntry workEntry)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);
                context.WorkEntries.InsertOnSubmit(workEntry);
                context.SubmitChanges();
            }
        }

        public void UpdateWorkEntry(WorkEntry updatedWorkEntry, WorkEntry originalWorkEntry)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);

                var existing = context.WorkEntries.FirstOrDefault(workEntry => workEntry.Id == originalWorkEntry.Id);
                if (existing == null)
                {
                    throw new ArgumentException("No workEntry found to update");
                }

                context.WorkEntries.Attach(updatedWorkEntry);

                context.SubmitChanges();
            }
        }

        public void DeleteWorkEntry(Guid id)
        {
            using (var cs = new ConnectionScope())
            {
                Database.WorkTrackDataContext context = new Database.WorkTrackDataContext(cs.Connection);

                var existing = context.WorkEntries.FirstOrDefault(workEntry => workEntry.Id == id);
                if (existing != null)
                {
                    context.WorkEntries.DeleteOnSubmit(existing);
                    context.SubmitChanges();
                }
            }
        }

        #endregion

    }

}
