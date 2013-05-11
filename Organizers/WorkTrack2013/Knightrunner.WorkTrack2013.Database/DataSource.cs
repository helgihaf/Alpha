using Knightrunner.WorkTrack2013.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.WorkTrack2013.Database
{
    public class DataSource : IDataSource
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

        public List<Contract.Project> GetProjects()
        {
            var projects = database.Fetch<Entities.Project>("").ToDictionary(p => p.Id);
            var result = new List<Contract.Project>(projects.Count);
            foreach (var project in projects.Values)
            {
                var contractProject = Converters.ProjectToContract(project);
                if (project.ParentId.HasValue)
                {
                    contractProject.ParentProjectPublicId = projects[project.ParentId.Value].PublicId;
                }
                result.Add(contractProject);
            }

            return result;
        }

    }
}
