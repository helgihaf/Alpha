using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Knightrunner.WorkTrack2013.Database;

namespace Knightrunner.WorkTrack2013.DatabaseTest
{
    [TestClass]
    public class UnitTest1
    {
        const string cs = "Server=(local);Database=WorkTrack2013;Trusted_Connection=True;";
        const string providerName = "System.Data.SqlClient";

        [TestMethod, TestCategory("DatabaseTest")]
        public void ShouldGetProjectsFromDatabase()
        {
            using (var ds = new DataSource(cs, providerName))
            {
                var projects = ds.GetProjects();
                Assert.IsNotNull(projects);
            }
        }
    }
}
